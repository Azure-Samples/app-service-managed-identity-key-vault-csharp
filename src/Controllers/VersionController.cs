// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Mikv.Controllers
{
    /// <summary>
    /// Handle the /healthz request
    /// </summary>
    [Route("[controller]")]
    public class VersionController : Controller
    {
        private readonly ILogger logger;
        private string version = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="VersionController"/> class.
        /// </summary>
        /// <param name="logger">log instance</param>
        public VersionController(ILogger<VersionController> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// gets the app version
        /// </summary>
        /// <remarks>
        /// Returns the version string
        /// </remarks>
        /// <returns>200 OK</returns>
        /// <response code="200">returns the version as text/plain</response>
        [HttpGet]
        [Produces("text/plain")]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult GetVersion()
        {
            logger.LogInformation("Version");

            if (string.IsNullOrWhiteSpace(version))
            {
                if (Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyInformationalVersionAttribute)) is AssemblyInformationalVersionAttribute v)
                {
                    version = v.InformationalVersion;
                }
            }

            // return 200 OK with payload
            return Ok(version);
        }
    }
}
