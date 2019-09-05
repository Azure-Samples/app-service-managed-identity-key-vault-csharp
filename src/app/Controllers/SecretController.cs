using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Mikv.Controllers
{
    /// <summary>
    /// Controller that returns the secret from Key Vault
    /// </summary>
    [Route("api/[controller]")]
    public class SecretController : Controller
    {
        private readonly ILogger logger;
        private readonly IConfiguration config;

        /// <summary>
        ///  Constructor
        /// </summary>
        /// <param name="logger">ILogger instance</param>
        /// <param name="config">IConfiguration instance</param>
        public SecretController(ILogger<SecretController> logger, IConfiguration config)
        {
            this.logger = logger;
            this.config = config;
        }

        /// <summary>
        /// </summary>
        /// <remarks>Returns the secret stored in Key Vault</remarks>
        /// <response code="200">returns the secret as text/plain</response>
        [HttpGet]
        [Produces("application/json")]
        public IActionResult GetSecret()
        {
            // log the request
            logger.LogInformation("GetSecret");

            try
            {
                string secret = config[Constants.KeyVaultSecretName];

                return Ok(secret);
            }
            catch (System.Exception ex)
            {
                logger.LogError("GetSecret Exception: {0}", ex);

                return new ObjectResult("getsecret exception")
                {
                    StatusCode = Constants.ServerError
                };
            }
        }
    }
}
