using Assistant_Bdd.Data;
using Microsoft.AspNetCore.Mvc;
using NLog;
using NLog.Web;

namespace Assistant_Interface.Controllers.Administration
{
    public class GestionAssistantController : Controller
    {
        private readonly AssistantContext _accessBddContext;
        private readonly IConfiguration _configuration;
        protected Logger Logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

        public GestionAssistantController(AssistantContext accessBddContext, IConfiguration configuration)
        {
            _accessBddContext = accessBddContext;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            try
            {
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
