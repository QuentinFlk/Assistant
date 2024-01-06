using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NLog;
using NLog.Web;
using Assistant_Interface.Data;
using Assistant_Interface.Models.Session;
using Newtonsoft.Json;

namespace Assistant_Interface.Areas.Identity.Pages.Account;

[AllowAnonymous]
public class LoginModel : PageModel
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ApplicationDbContext _identityDbContext;
    protected Logger LoggerLogin = LogManager.Setup().LoadConfigurationFromAppSettings().GetLogger("Login");

    public LoginModel(SignInManager<IdentityUser> signInManager, ApplicationDbContext identityDbContext)
    {
        _signInManager = signInManager;
        _identityDbContext = identityDbContext;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public IList<AuthenticationScheme> ExternalLogins { get; set; }

    public string ReturnUrl { get; set; }

    [TempData]
    public string ErrorMessage { get; set; }

    public class InputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public async Task OnGetAsync(string returnUrl = null)
    {
        try
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }
        catch (Exception ex)
        {
            LoggerLogin.Error(ex.Message);
            LoggerLogin.Error(ex.StackTrace);
            LoggerLogin.Error(ex.Source);
            ReturnUrl = Url.Content("~/");
        }
    }

    public async Task<IActionResult> OnPostAsync(string returnUrl = null)
    {
        try
        {
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe,
                    lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    LoggerLogin.Info("L'utilisateur ayant pour email {0} a correctement était identifié.",
                        Input.Email);
                    var idUser = _identityDbContext.Users.FirstOrDefault(x => x.UserName.Equals(Input.Email))?.Id;
                    if (idUser != null)
                    {
                        var userHaveRole =
                            _identityDbContext.UserRoles.FirstOrDefault(x => x.UserId.Equals(idUser));
                        if (userHaveRole != null)
                        {
                            var profilUser = int.Parse(userHaveRole.RoleId);
                            var accessViewModel = new AccessViewModel
                            {
                                ProfilUtilisateur = profilUser,
                                EmailConnexion = Input.Email
                            };

                            HttpContext.Session.SetString("AccessViewModel",
                                JsonConvert.SerializeObject(accessViewModel));
                            //TODO rediriger vers un page en fonction des droits de l'utilisateur
                            return RedirectToAction("Accueil", "Home");
                        }

                        await _signInManager.SignOutAsync();

                        ErrorMessage =
                            "Votre compte n'a pas de droit d'accès à la plateforme. Merci de contacter le support.";
                        return Page();
                    }
                }

                if (result.IsLockedOut)
                {
                    LoggerLogin.Info("Le compte de l'utilisateur ayant pour email {0} est bloqué.",
                        Input.Email);
                    return RedirectToPage("./Lockout");
                }

                ErrorMessage =
                    "L'email et/ou le mot de passe sont incorrects. Merci de ressaisir vos identifiants. Si le problème persiste merci de contacter le support.";
                return Page();
            }

            return Page();
        }
        catch (Exception ex)
        {
            LoggerLogin.Error(ex.Message);
            LoggerLogin.Error(ex.StackTrace);
            LoggerLogin.Error(ex.Source);
            return Page();
        }
    }
}
