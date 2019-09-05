using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace Mikv
{
    public class App
    {
        /// <summary>
        /// Main entry point
        /// 
        /// Configure and run the kestrel server
        /// </summary>
        /// <param name="args">command line args</param>
        public static void Main(string[] args)
        {
            try
            {
                // get the Key Vault URL
                string kvUrl = GetKeyVaultUrl(args);

                // kvUrl is required
                if (string.IsNullOrEmpty(kvUrl))
                {
                    Console.WriteLine("Key Vault name missing");

                    Environment.Exit(-1);
                }

                // build the host
                var host = BuildHost(kvUrl);

                // get the logger service
                var logger = host.Services.GetRequiredService<ILogger<App>>();

                // get the configuration service
                var config = host.Services.GetService<IConfiguration>();

                // log a not using app insights warning
                if (string.IsNullOrEmpty(config[Constants.AppInsightsKey]))
                {
                    logger.LogWarning("App Insights Key not set");
                }

                logger.LogInformation("Web Server Started");

                // run the server
                host.Run();

                // exiting
                logger.LogInformation("Web Server Stopped");
            }

            catch (Exception ex)
            {
                // end app on error
                Console.WriteLine("Error in Main()\r\n{0}", ex);

                Environment.Exit(-1);
            }
        }

        /// <summary>
        /// Builds the config for the web server
        /// 
        /// Uses Key Vault via MSI in production
        /// 
        /// Uses dotnet secrets in development
        /// </summary>
        /// <param name="kvUrl">URL of the Key Vault to use</param>
        /// <returns>Root Configuration</returns>
        static IConfigurationRoot BuildConfig(string kvUrl)
        {
            // standard config builder
            var cfgBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json", optional: true);

            // use Azure Key Vault via MSI
            cfgBuilder.AddAzureKeyVault(kvUrl);

            // build the config
            return cfgBuilder.Build();
        }

        /// <summary>
        /// Build the web host
        /// </summary>
        /// <param name="kvUrl">URL of the Key Vault</param>
        /// <returns>Web Host ready to run</returns>
        static IWebHost BuildHost(string kvUrl)
        {
            // configure the web host builder
            IWebHostBuilder builder = WebHost.CreateDefaultBuilder()
                .UseKestrel()
                .UseStartup<Startup>();

            // build the config
            // need to build this here so it's available to dependendent services
            IConfigurationRoot config = BuildConfig(kvUrl);
            builder.UseConfiguration(config);

            // setup the listen port
            UseUrls(builder, config);

            // add App Insights if key set
            string appInsightsKey = config.GetValue<string>(Constants.AppInsightsKey);

            if (!string.IsNullOrEmpty(appInsightsKey))
            {
                builder.UseApplicationInsights(appInsightsKey);
            }

            // build the host
            return builder.Build();
        }

        /// <summary>
        /// Set the listen URL and port
        /// </summary>
        /// <param name="builder">Web Host Builder</param>
        /// <param name="config">Configuration Root</param>
        static void UseUrls(IWebHostBuilder builder, IConfigurationRoot config)
        {
            // get the port or use default
            int port = config.GetValue<int>(Constants.Port);

            if (port == 0)
            {
                port = 4120;
            }

            // listen on the port
            builder.UseUrls(string.Format("http://*:{0}/", port));
        }

        /// <summary>
        /// Get the Key Vault URL from the environment variable or command line
        /// </summary>
        /// <param name="args">command line args</param>
        /// <returns>URL to Key Vault</returns>
        static string GetKeyVaultUrl(string[] args)
        {
            // get the key vault name from the environment variable
            string kvName = Environment.GetEnvironmentVariable(Constants.KeyVaultName);

            // command line arg overrides environment variable
            if (args.Length > 0 && ! args[0].StartsWith("-"))
            {
                kvName = args[0].Trim();
            }

            // build the URL
            if (!string.IsNullOrEmpty(kvName) && ! kvName.StartsWith("https://"))
            {
                return "https://" + kvName + ".vault.azure.net/";
            }

            return string.Empty;
        }
    }
}
