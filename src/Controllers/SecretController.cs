// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="SecretController"/> class.
        /// </summary>
        /// <param name="logger">ILogger instance</param>
        public SecretController(ILogger<SecretController> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Returns the secret injected from Key Vault
        /// </summary>
        /// <param name="key">key of secret (env var)</param>
        /// <returns>text/plain</returns>
        /// <remarks>Returns the secret stored in Key Vault</remarks>
        /// <response code="200">returns the secret as text/plain</response>
        [HttpGet("{key}")]
        [Produces("text/plain")]
        public IActionResult GetSecret([FromRoute] string key)
        {
            // log the request
            logger.LogInformation("GetSecret");

            try
            {
                if (!string.IsNullOrWhiteSpace(key))
                {
                    string secret = Environment.GetEnvironmentVariable(key.Trim());

                    if (!string.IsNullOrWhiteSpace(secret))
                    {
                        return Ok(secret);
                    }
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                logger.LogError($"GetSecret Exception: {ex.Message}");

                return new ObjectResult("getsecret exception")
                {
                    StatusCode = (int)HttpStatusCode.ServiceUnavailable,
                };
            }
        }
    }
}
