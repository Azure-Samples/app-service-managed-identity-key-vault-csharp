using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Mikv.Controllers
{
    /// <summary>
    /// Handle the /healthz request
    /// </summary>
    [Route("[controller]")]
    public class HealthzController : Controller
    {
        private readonly ILogger logger;

        /// <summary>
        ///  Constructor
        /// </summary>
        /// <param name="logger">log instance</param>
        public HealthzController(ILogger<HealthzController> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// The health check end point
        /// </summary>
        /// <remarks>
        /// Returns the string "Healthy"
        /// </remarks>
        /// <returns>200 OK</returns>
        /// <response code="200">returns the string "Healthy" as text/plain</response>
        [HttpGet]
        [Produces("text/plain")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(void), 400)]
        public IActionResult Healthz()
        {
            logger.LogInformation("Healthz");

            // return 200 OK with payload
            return Ok("Healthy");
        }
    }
}
