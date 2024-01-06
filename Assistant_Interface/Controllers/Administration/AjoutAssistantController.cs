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
using static Assistant_Interface.Controllers.Utils.Utils;

namespace Assistant_Interface.Controllers.Administration
{
    public class AjoutAssistantController : Controller
    {
        private readonly AssistantContext _accessBddContext;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _identityDbContext;
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

        [HttpPost]
        public JsonResult AjoutAssistant([FromBody] AjoutAssistantViewModel dataAjoutAssistant)
        {
            var transaction = _accessBddContext.Database.BeginTransaction();
            try
            {
                Logger.Info("Appel de la méthode pour ajouter l'assistant ayant pour nom {0}",
                    dataAjoutAssistant.NomAssistant);
                var apiKey = _configuration.GetSection("OpenAi:api-key").Value;
                var openaiApi = new OpenAIClient(apiKey);
                var request = new CreateAssistantRequest(dataAjoutAssistant.ChoixModel, dataAjoutAssistant.NomAssistant,
                    dataAjoutAssistant.DescriptionAssistant, dataAjoutAssistant.InstructionAssistant);
                var assistantOpenai = openaiApi.AssistantsEndpoint.CreateAssistantAsync(request).Result;
                var assistant = new Assistant
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

                _accessBddContext.Assistant.Add(assistant);
                _accessBddContext.SaveChanges();
                transaction.Commit();
                transaction.Dispose();

                var respFormat =
                    new JsonResult(
                            "L'assistant a correctement été créé, vous allez être redirigé vers la page de configuration._" +
                            assistant.IdAssistant)
                        {StatusCode = StatusCodes.Status200OK};
                return respFormat;
            }
            catch (Exception ex)
            {
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
    }
}
