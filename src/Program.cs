// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Mikv
{
    public class Program
    {
        public static int Main(string[] args)
        {
            DisplayAsciiArt();

            // check PORT env var
            string p = Environment.GetEnvironmentVariable("PORT");

            if (string.IsNullOrWhiteSpace(p))
            {
                p = "8080";
                Environment.SetEnvironmentVariable("PORT", p);
            }

            // pass --urls to builder
            if (args == null)
            {
                args = new string[] { "--urls", $"http://*:{p}/" };
            }
            else
            {
                // append if not passed
                if (!args.Contains("--urls"))
                {
                    args = args.Append("--urls").Append($"http://*:{p}/").ToArray();
                }
            }

            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            WebApplication app = builder.Build();

            // handle default doc
            app.Use(async (context, next) =>
            {
                string path = context.Request.Path.HasValue ? context.Request.Path.Value.ToLowerInvariant() : string.Empty;

                // matches / /index.* /default.*
                if (path == "/" || path.StartsWith("/index.") || path.StartsWith("/default."))
                {
                    // return the message
                    context.Response.ContentType = "text/plain";
                    await context.Response.Body.WriteAsync(Encoding.UTF8.GetBytes("Under construction ...")).ConfigureAwait(false);
                }
                else
                {
                    // not a match, so call next handler
                    await next().ConfigureAwait(false);
                }
            });

            app.MapControllers();

            Console.WriteLine($"Listening on port {p} ...");

            app.Run();

            return 0;
        }

        private static void DisplayAsciiArt()
        {
            const string file = "./files/ascii-art.txt";

            try
            {
                if (File.Exists(file))
                {
                    string txt = File.ReadAllText(file);

                    if (!string.IsNullOrWhiteSpace(txt))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine(txt);
                        Console.ResetColor();
                    }
                }
            }
            catch
            {
                // ignore any errors
            }
        }
    }
}
