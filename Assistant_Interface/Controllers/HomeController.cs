using Assistant_Bdd.Data;
using Assistant_Interface.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Diagnostics;
using Assistant_Interface.Models.Session;
using NLog.Web;
using static Assistant_Interface.Controllers.Utils.Utils;
using Assistant_Interface.Data;
using Newtonsoft.Json;

namespace Assistant_Interface.Controllers
{
    public class HomeController : Controller
    {
        private readonly AssistantContext _accessBddContext;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _identityDbContext;
        protected Logger Logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

        public HomeController(AssistantContext accessBddContext, IConfiguration configuration,
            SignInManager<IdentityUser> signInManager, ApplicationDbContext identityDbContext)
        {
            _accessBddContext = accessBddContext;
            _configuration = configuration;
            _signInManager = signInManager;
            _identityDbContext = identityDbContext;
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
