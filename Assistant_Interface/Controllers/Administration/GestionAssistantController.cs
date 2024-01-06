using Assistant_Bdd.Data;
using Assistant_Interface.Data;
using Assistant_Interface.Models.Session;
using Assistant_Interface.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog;
using NLog.Web;
using static Assistant_Interface.Controllers.Utils.Utils;

namespace Assistant_Interface.Controllers.Administration
{
    public class GestionAssistantController : Controller
    {
        private readonly AssistantContext _accessBddContext;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _identityDbContext;
        protected Logger Logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

        public GestionAssistantController(AssistantContext accessBddContext, IConfiguration configuration,
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
                Logger.Info("Chargement de la page permettant de gérer la liste des Assistant disponible.");
                var sessionAccess = HttpContext.Session.GetString("AccessViewModel");
                if (sessionAccess == null)
                {
                    var user = User;
                    if (user.Identity.IsAuthenticated)
                    {
                        var accessViewModel = GetAccessViewModel(user, _identityDbContext);
                        if (accessViewModel == null)
                            return RedirectToAction("Index", "Home");
                        HttpContext.Session.SetString("AccessViewModel",
                            JsonConvert.SerializeObject(accessViewModel));
                    }
                    else
                        return RedirectToAction("Index", "Home");
                }

                var listAssistant = _accessBddContext.Assistant.ToList();
                var viewModel = new GestionAssistantViewModel
                {
                    ListAssistantActif = new List<AssistantViewModel>(),
                    ListAssistantInactif = new List<AssistantViewModel>()
                };

                foreach (var assistant in listAssistant)
                {
                    var item = new AssistantViewModel(assistant);
                    var user = _identityDbContext.Users.FirstOrDefault(x => x.Id == assistant.IdCreateurAssistant);
                    item.NomCreateurAssistant = user != null ? user.UserName : "Inconnu";
                    if (assistant.AssistantActif)
                        viewModel.ListAssistantActif.Add(item);
                    else
                        viewModel.ListAssistantInactif.Add(item);
                }

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
    }
}
