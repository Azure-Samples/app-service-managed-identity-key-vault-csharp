using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Mikv
{
    /// <summary>
    /// WebHostBuilder Startup
    /// </summary>
    public class Startup
    {
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration">the configuration for WebHost</param>
        public Startup(IConfiguration configuration)
        {
            // keep a local reference
            Configuration = configuration;
        }

        /// <summary>
        /// Service configuration
        /// </summary>
        /// <param name="services">The services in the web host</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // add MVC 2.2
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // configure Swagger
            services.ConfigureSwaggerGen(options =>
            {
                options.IncludeXmlComments(GetXmlCommentsPath());
            });

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(Constants.SwaggerName, new Info { Title = Constants.SwaggerTitle, Version = Constants.SwaggerVersion });
            });
        }

        /// <summary>
        /// Configure the application builder
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // differences based on dev or prod
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(Constants.SwaggerPath, Constants.SwaggerTitle);
                c.RoutePrefix = string.Empty;
            });

            app.UseMvc();
        }

        /// <summary>
        /// Get the path to the XML docs generated at compile time
        /// 
        /// Swagger uses this to annotate the documentation
        /// </summary>
        /// <returns>string - path to xml file</returns>
        private string GetXmlCommentsPath()
        {
            var app = Microsoft.Extensions.PlatformAbstractions.PlatformServices.Default.Application;
            return System.IO.Path.Combine(app.ApplicationBasePath, Constants.XmlCommentsPath);
        }
    }
}
