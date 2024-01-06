using System.Security.Claims;
using Assistant_Interface.Data;
using Assistant_Interface.Models.Session;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using NLog;
using NLog.Web;

namespace Assistant_Interface.Controllers.Utils
{
    public static class Utils
    {
        public static Logger Logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

        public static AccessViewModel GetAccessViewModel(ClaimsPrincipal user, ApplicationDbContext identityDbContext)
        {
            try
            {
                var userExiste =
                    identityDbContext.Users.FirstOrDefault(x => x.UserName.Equals(user.Identity.Name));
                if (userExiste == null)
                    return null;
                var userHaveRole =
                    identityDbContext.UserRoles.FirstOrDefault(x => x.UserId.Equals(userExiste.Id));
                if (userHaveRole != null)
                {
                    var profilUser = int.Parse(userHaveRole.RoleId);
                    var accessViewModel = new AccessViewModel
                    {
                        ProfilUtilisateur = profilUser,
                        EmailConnexion = userExiste.Email,
                        IdConnexion = userExiste.Id,
                        NomConnexion = userExiste.UserName
                    };
                    return accessViewModel;
                }

                return null;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                Logger.Error(ex.StackTrace);
                Logger.Error(ex.InnerException);
                return null;
            }
        }
    }
}
