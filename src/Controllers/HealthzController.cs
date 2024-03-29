// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

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
        /// Initializes a new instance of the <see cref="HealthzController"/> class.
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
        /// Returns the string "pass"
        /// </remarks>
        /// <returns>200 OK</returns>
        /// <response code="200">returns the string "pass" as text/plain</response>
        [HttpGet]
        [Produces("text/plain")]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult Healthz()
        {
            logger.LogInformation("Healthz");

            // return 200 OK with payload
            return Ok("pass");
        }
    }
}
