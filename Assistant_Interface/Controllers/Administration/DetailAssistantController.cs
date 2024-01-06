using Assistant_Bdd.Data;
using Assistant_Interface.Data;
using Assistant_Interface.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog;
using NLog.Web;
using static Assistant_Interface.Controllers.Utils.Utils;

namespace Assistant_Interface.Controllers.Administration
{
    public class DetailAssistantController : Controller
    {
        private readonly AssistantContext _accessBddContext;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _identityDbContext;
        protected Logger Logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

        public DetailAssistantController(AssistantContext accessBddContext, IConfiguration configuration,
            ApplicationDbContext identityDbContext)
        {
            _accessBddContext = accessBddContext;
            _configuration = configuration;
            _identityDbContext = identityDbContext;
        }

        public IActionResult Index(string idAssistant)
        {
            try
            {
                Logger.Info("Chargement de la page permettant de gérer la liste des Assistant disponible.");
                
                return View();
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
