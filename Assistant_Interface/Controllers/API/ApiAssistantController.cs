using Assistant_Bdd.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using NLog.Web;

namespace Assistant_Interface.Controllers.API
{
    [Authorize]
    [ApiController]
    [Route("apiAssistant/[action]")]
    public class ApiAssistantController : ControllerBase
    {
        private readonly AssistantContext _accessBddContext;
        private readonly IConfiguration _configuration;
        protected Logger Logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

        public ApiAssistantController(AssistantContext accessBddContext, IConfiguration configuration)
        {
            _accessBddContext = accessBddContext;
            _configuration = configuration;
        }

    }
}
