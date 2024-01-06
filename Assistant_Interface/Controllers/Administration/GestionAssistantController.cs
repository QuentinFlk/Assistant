using Assistant_Bdd.Data;
using Assistant_Interface.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NLog;
using NLog.Web;

namespace Assistant_Interface.Controllers.Administration
{
    public class GestionAssistantController : Controller
    {
        private readonly AssistantContext _accessBddContext;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<IdentityUser> _signInManager;
        protected Logger Logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

        public GestionAssistantController(AssistantContext accessBddContext, IConfiguration configuration,
        SignInManager<IdentityUser> signInManager)
        {
            _accessBddContext = accessBddContext;
            _configuration = configuration;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            try
            {
                Logger.Info("Chargement de la page permettant de gérer la liste des Assistant disponible.");
                var listAssistant = _accessBddContext.Assistant.ToList();
                var viewModel = new GestionAssistantViewModel
                {
                    ListAssistantActif = new List<AssistantViewModel>(),
                    ListAssistantInactif = new List<AssistantViewModel>()
                };

                foreach (var assistant in listAssistant)
                {
                    var item = new AssistantViewModel(assistant);
                    var user = _signInManager.UserManager.FindByIdAsync(assistant.IdCreateurAssistant).Result;
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
