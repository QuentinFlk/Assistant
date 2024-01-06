using System.ComponentModel.DataAnnotations;
using Assistant_Interface.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NLog;
using NLog.Web;

namespace Assistant_Interface.Areas.Identity.Pages.Account
{
    //[AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly ApplicationDbContext _identityDbContext;
        protected Logger LoggerRegister = LogManager.Setup().LoadConfigurationFromAppSettings().GetLogger("Register");

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            IUserStore<IdentityUser> userStore,
            SignInManager<IdentityUser> signInManager,
            ApplicationDbContext identityDbContext)
        {
            _userManager = userManager;
            _userStore = userStore;
            _signInManager = signInManager;
            _identityDbContext = identityDbContext;
        }
        
        [BindProperty]
        public InputModel Input { get; set; }
        
        public string ReturnUrl { get; set; }
        
        public IList<AuthenticationScheme> ExternalLogins { get; set; }
        
        public class InputModel
        {
            [Required]
            [Display(Name = "Votre nom")]
            public string Nom { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }
            
            [Required]
            //[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }
            
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            try
            {
                ReturnUrl = returnUrl;
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            }
            catch (Exception ex)
            {
                LoggerRegister.Error(ex.Message);
                LoggerRegister.Error(ex.StackTrace);
                LoggerRegister.Error(ex.Source);
            }
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            try
            {
                returnUrl ??= Url.Content("~/");
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
                if (ModelState.IsValid)
                {
                    var user = CreateUser();
                    user.Email = Input.Email;
                    user.EmailConfirmed = true;
                    await _userStore.SetUserNameAsync(user, Input.Nom, CancellationToken.None);

                    var result = await _userManager.CreateAsync(user, Input.Password);
                    if (result.Succeeded)
                    {
                        LoggerRegister.Info(
                            "La création du compte utilisateur ayant pour emial {0}, c'est correctement passé.",
                            Input.Email);

                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

                return Page();
            }
            catch (Exception ex)
            {
                LoggerRegister.Error(ex.Message);
                LoggerRegister.Error(ex.StackTrace);
                LoggerRegister.Error(ex.Source);
                return Page();
            }
        }

        private IdentityUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<IdentityUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<IdentityUser> GetEmailStore()
        {
            try
            {
                if (!_userManager.SupportsUserEmail)
                {
                    throw new NotSupportedException("The default UI requires a user store with email support.");
                }
                return (IUserEmailStore<IdentityUser>)_userStore;
            }
            catch (Exception ex)
            {
                LoggerRegister.Error(ex.Message);
                LoggerRegister.Error(ex.StackTrace);
                LoggerRegister.Error(ex.Source);
                return null;
            }
        }
    }
}
