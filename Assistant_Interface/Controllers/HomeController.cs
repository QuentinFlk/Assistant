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
using Assistant_Interface.Models.ViewModels;
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
                }

                var viewModel = new AccueilViewModel();
                var listAssistant = _accessBddContext.Assistant.Where(x => !x.IsAssistantGlobal).ToList();
                viewModel.SetAssistantDisponible(listAssistant);
                var sessionClient = HttpContext.Session.GetString("SesseionClient");
                //if (sessionClient != null)
                //{
                //    var echangeSessionViewModel = JsonConvert.DeserializeObject<EchangeSessionViewModel>(sessionClient);
                //    viewModel.ChoixAssistant = echangeSessionViewModel.OpenAiAssistantId;
                //    viewModel.EchangeClientAssistant = echangeSessionViewModel.EchangeClientAssistant;
                //    viewModel.ListEchangeClientAssistant = echangeSessionViewModel.ListEchangeClientAssistant;
                //}

                viewModel.EchangeClientAssistant = "";
                viewModel.ListEchangeClientAssistant = new List<string>
                {
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec cursus nec odio eget iaculis. Donec ligula dui, dignissim eget turpis vitae, " +
                    "bibendum auctor diam. Nullam posuere odio mauris, pellentesque faucibus lacus laoreet sed. Nulla felis dui, consequat ullamcorper congue at, " +
                    "fermentum eu neque. Ut cursus commodo neque, sit amet fermentum metus pellentesque vitae. Donec consectetur urna ac facilisis iaculis. Ut vitae purus at neque auctor interdum.",
                    "Donec in finibus erat. Nullam et consectetur magna. Curabitur ut mauris ex. Nullam in lectus et purus tempus ultricies. Quisque gravida augue " +
                    "in est fringilla, sed tincidunt quam interdum. Morbi pulvinar ligula non purus aliquam, non molestie tortor egestas. Sed vel imperdiet odio. " +
                    "Mauris a neque purus. Donec efficitur diam sit amet dui egestas, quis luctus justo suscipit.",
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras eu euismod nisi. Duis imperdiet metus et enim bibendum consequat. Proin scelerisque " +
                    "eget risus nec dictum. Mauris suscipit, velit sit amet ultricies eleifend, sem libero tempus leo, ut iaculis enim arcu ac odio. Curabitur ornare et " +
                    "justo id viverra. Ut rhoncus ante nisl, n",
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec cursus nec odio eget iaculis. Donec ligula dui, dignissim eget turpis vitae, " +
                    "bibendum auctor diam. Nullam posuere odio mauris, pellentesque faucibus lacus laoreet sed. Nulla felis dui, consequat ullamcorper congue at, " +
                    "fermentum eu neque. Ut cursus commodo neque, sit amet fermentum metus pellentesque vitae. Donec consectetur urna ac facilisis iaculis. Ut vitae purus at neque auctor interdum.",
                    "Donec in finibus erat. Nullam et consectetur magna. Curabitur ut mauris ex. Nullam in lectus et purus tempus ultricies. Quisque gravida augue " +
                    "in est fringilla, sed tincidunt quam interdum. Morbi pulvinar ligula non purus aliquam, non molestie tortor egestas. Sed vel imperdiet odio. " +
                    "Mauris a neque purus. Donec efficitur diam sit amet dui egestas, quis luctus justo suscipit.",
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras eu euismod nisi. Duis imperdiet metus et enim bibendum consequat. Proin scelerisque " +
                    "eget risus nec dictum. Mauris suscipit, velit sit amet ultricies eleifend, sem libero tempus leo, ut iaculis enim arcu ac odio. Curabitur ornare et " +
                    "justo id viverra. Ut rhoncus ante nisl, n",
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec cursus nec odio eget iaculis. Donec ligula dui, dignissim eget turpis vitae, " +
                    "bibendum auctor diam. Nullam posuere odio mauris, pellentesque faucibus lacus laoreet sed. Nulla felis dui, consequat ullamcorper congue at, " +
                    "fermentum eu neque. Ut cursus commodo neque, sit amet fermentum metus pellentesque vitae. Donec consectetur urna ac facilisis iaculis. Ut vitae purus at neque auctor interdum.",
                    "Donec in finibus erat. Nullam et consectetur magna. Curabitur ut mauris ex. Nullam in lectus et purus tempus ultricies. Quisque gravida augue " +
                    "in est fringilla, sed tincidunt quam interdum. Morbi pulvinar ligula non purus aliquam, non molestie tortor egestas. Sed vel imperdiet odio. " +
                    "Mauris a neque purus. Donec efficitur diam sit amet dui egestas, quis luctus justo suscipit.",
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras eu euismod nisi. Duis imperdiet metus et enim bibendum consequat. Proin scelerisque " +
                    "eget risus nec dictum. Mauris suscipit, velit sit amet ultricies eleifend, sem libero tempus leo, ut iaculis enim arcu ac odio. Curabitur ornare et " +
                    "justo id viverra. Ut rhoncus ante nisl, n"
                };

                return View(viewModel);
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
