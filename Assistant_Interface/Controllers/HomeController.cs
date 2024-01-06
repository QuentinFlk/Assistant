using Assistant_Bdd.Data;
using Assistant_Interface.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using System.Diagnostics;
using NLog.Web;

namespace Assistant_Interface.Controllers
{
    public class HomeController : Controller
    {
        private readonly AssistantContext _accessBddContext;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        protected Logger Logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

        public HomeController(AssistantContext accessBddContext, IConfiguration configuration,
            SignInManager<IdentityUser> signInManager, Logger? logger = null)
        {
            _accessBddContext = accessBddContext;
            _configuration = configuration;
            _signInManager = signInManager;
            if (logger != null)
                Logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Accueil", "Home");
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                Logger.Error(ex.StackTrace);
                Logger.Error(ex.InnerException);
                return RedirectToAction("Error");
            }
        }

        [AllowAnonymous]
        public IActionResult Accueil()
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
                return RedirectToAction("Index");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
