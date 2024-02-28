using Assistant_Bdd.Data;
using Assistant_Bdd.Models;
using Assistant_Interface.Data;
using Assistant_Interface.Models.Session;
using Assistant_Interface.Models.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog;
using NLog.Web;
using OpenAI;
using OpenAI.Assistants;
using OpenAI.Threads;
using System.Threading;
using static Assistant_Interface.Controllers.Utils.Utils;
using Message = OpenAI.Threads.Message;

namespace Assistant_Interface.Controllers.Administration
{
    public class AjoutAssistantController : Controller
    {
        private readonly AssistantContext _accessBddContext;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _identityDbContext;
        private string _threadId = "";
        protected Logger Logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

        public AjoutAssistantController(AssistantContext accessBddContext, IConfiguration configuration,
            ApplicationDbContext identityDbContext)
        {
            _accessBddContext = accessBddContext;
            _configuration = configuration;
            _identityDbContext = identityDbContext;
        }

        public IActionResult Index()
        {
            try
            {
                Logger.Info("Chargement de la page permettant d'ajouter un nouvel Assistant.");
                AccessViewModel accessViewModel = null;
                var sessionAccess = HttpContext.Session.GetString("AccessViewModel");
                if (sessionAccess == null)
                {
                    var user = User;
                    if (user.Identity.IsAuthenticated)
                    {
                        accessViewModel = GetAccessViewModel(user, _identityDbContext);
                        if (accessViewModel == null)
                            return RedirectToAction("Index", "Home");
                        HttpContext.Session.SetString("AccessViewModel",
                            JsonConvert.SerializeObject(accessViewModel));
                    }
                    else
                        return RedirectToAction("Index", "Home");
                }
                else
                    accessViewModel = JsonConvert.DeserializeObject<AccessViewModel>(sessionAccess);
                var viewModel = new AjoutAssistantViewModel
                {
                    IdCreateurAssistant = accessViewModel.IdConnexion,
                };

                var apiKey = _configuration.GetSection("OpenAi:api-key").Value;
                if (string.IsNullOrEmpty(apiKey))
                {
                    Logger.Error("Aucune clé d'api n'est présente dans le appsettings.");
                    return RedirectToAction("Accueil", "Home");
                }

                var openaiApi = new OpenAIClient(apiKey);
                var listModelDisponible = openaiApi.ModelsEndpoint.GetModelsAsync().Result.ToList();
                var listModelGpt = listModelDisponible.Where(x => x.Id.StartsWith("gpt-") && !x.Id.Contains("vision")).ToList();
                viewModel.SetModelDisponible(listModelGpt);
                openaiApi.Dispose();
                return View(viewModel);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                Logger.Error(ex.StackTrace);
                Logger.Error(ex.InnerException);
                return RedirectToAction("Accueil", "Home");
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetTagForNewAssistant(string prompt)
        {
            var apiKey = _configuration.GetSection("OpenAi:api-key").Value;
            var auth = new OpenAIAuthentication(apiKey);
            var api = new OpenAIClient(auth);
            try
            {
                if (string.IsNullOrEmpty(prompt))
                    return new JsonResult("Merci de saisir un prompt.")
                    { StatusCode = StatusCodes.Status400BadRequest };
                Logger.Info("Appel de la méthode qui va appeler tag manager pour générer les tags de base.");
                var tagManager = _configuration.GetSection("OpenAi:tag-manager").Value;
                var assistant = await api.AssistantsEndpoint.RetrieveAssistantAsync(tagManager);
                var listTag = new List<string>();

                if (!string.IsNullOrEmpty(_threadId))
                {
                    var messageList = await api.ThreadsEndpoint.ListMessagesAsync(_threadId);
                    foreach (var itemMessage in messageList.Items)
                    {
                        listTag.Add(itemMessage.PrintContent());
                    }
                }
                else
                {
                    var thread = await api.ThreadsEndpoint.CreateThreadAsync();
                    _threadId = thread.Id;
                    await thread.CreateMessageAsync(prompt);
                    var run = await thread.CreateRunAsync(assistant);
                    Thread.Sleep(500);
                    var runId = run.Id;
                    Thread.Sleep(500);
                    var listRunStep = await run.ListRunStepsAsync();

                    foreach (var item in listRunStep.Items)
                    {
                        var runStep = await run.RetrieveRunStepAsync(item.Id);
                        runStep = await runStep.UpdateAsync();
                        while (runStep.Status != RunStatus.Completed)
                        {
                            runStep = await runStep.UpdateAsync();
                        }

                        var messageList = await api.ThreadsEndpoint.ListMessagesAsync(_threadId);
                        foreach (var itemMessage in messageList.Items)
                        {
                            if (itemMessage.PrintContent().Equals(prompt))
                                continue;
                            listTag.Add(itemMessage.PrintContent());
                        }
                    }
                }

                api.Dispose();

                return new JsonResult(listTag)
                { StatusCode = StatusCodes.Status200OK };
            }
            catch (Exception ex)
            {
                api.Dispose();
                Logger.Error(ex.Message);
                Logger.Error(ex.InnerException);
                Logger.Error(ex.StackTrace);
                return new JsonResult(ex.Message)
                { StatusCode = StatusCodes.Status400BadRequest };
            }
        }

        [HttpPost]
        public JsonResult AjoutAssistant([FromBody] AjoutAssistantViewModel dataAjoutAssistant)
        {
            var transaction = _accessBddContext.Database.BeginTransaction();
            var apiKey = _configuration.GetSection("OpenAi:api-key").Value;
            var api = new OpenAIClient(apiKey);
            try
            {
                Logger.Info("Appel de la méthode pour ajouter l'assistant ayant pour nom {0}",
                    dataAjoutAssistant.NomAssistant);
                var request = new CreateAssistantRequest(dataAjoutAssistant.ChoixModel, dataAjoutAssistant.NomAssistant,
                    dataAjoutAssistant.DescriptionAssistant, dataAjoutAssistant.InstructionAssistant);
                var assistantOpenai = api.AssistantsEndpoint.CreateAssistantAsync(request).Result;
                var newAssistant = new Assistant
                {
                    OpenAiAssisantId = assistantOpenai.Id,
                    NomAssistant = dataAjoutAssistant.NomAssistant,
                    InstructionAssistant = dataAjoutAssistant.InstructionAssistant,
                    DescriptionAssistant = dataAjoutAssistant.DescriptionAssistant,
                    AssistantActif = true,
                    CreationAssistance = DateTime.Now,
                    UpdateAssistance = DateTime.Now,
                    IdCreateurAssistant = dataAjoutAssistant.IdCreateurAssistant
                };

                _accessBddContext.Assistant.Add(newAssistant);
                _accessBddContext.SaveChanges();

                if (dataAjoutAssistant.LisTagAssistant.Any())
                {
                    foreach (var tag in dataAjoutAssistant.LisTagAssistant)
                    {
                        var tagAssistant = new TagAssistant
                        {
                            IdAssistant = newAssistant.IdAssistant,
                        };
                        var tagExiste = _accessBddContext.Tag.FirstOrDefault(x => x.LibelleTag.Equals(tag));
                        if (tagExiste == null)
                        {
                            var newTag = new Tag
                            {
                                LibelleTag = tag
                            };
                            _accessBddContext.Tag.Add(newTag);
                            _accessBddContext.SaveChanges();

                            tagAssistant.IdTag = newTag.IdTag;
                        }
                        else
                            tagAssistant.IdTag = tagExiste.IdTag;
                        _accessBddContext.TagAssistant.Add(tagAssistant);
                    }

                    _accessBddContext.SaveChanges();
                }

                transaction.Commit();
                transaction.Dispose();

                api.Dispose();
                return new JsonResult(
                            "L'assistant a correctement été créé, vous allez être redirigé vers la page de configuration._" +
                            newAssistant.IdAssistant)
                { StatusCode = StatusCodes.Status200OK };
            }
            catch (Exception ex)
            {
                api.Dispose();
                transaction.Rollback();
                transaction.Dispose();
                Logger.Error(ex.Message);
                Logger.Error(ex.InnerException);
                Logger.Error(ex.StackTrace);
                var respFormat = new JsonResult(ex.Message)
                { StatusCode = StatusCodes.Status400BadRequest };
                return respFormat;
            }
        }

        private class TagOpenAi
        {
            [JsonProperty("tags")]
            public List<string> Tags { get; set; }
        }
    }
}