// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Globalization;
using System.Reflection;
using System;
using System.Text;

string? p = Environment.GetEnvironmentVariable("PORT");

if (string.IsNullOrWhiteSpace(p))
{
    p = "8080";
    Environment.SetEnvironmentVariable("PORT", p);
}

// pass the port
args = new string[] { "--urls", $"http://*:{p}/" };

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// handle /version
app.Use(async (context, next) =>
{
    // matches /version
    if (context.Request.Path.Value == "/version")
    {
        // return the version info
        context.Response.ContentType = "text/plain";

        if (Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyInformationalVersionAttribute)) is AssemblyInformationalVersionAttribute v)
        {
            await context.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(v.InformationalVersion)).ConfigureAwait(false);
        }
    }
    else
    {
        // not a match, so call next middleware handler
        await next().ConfigureAwait(false);
    }
});

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
