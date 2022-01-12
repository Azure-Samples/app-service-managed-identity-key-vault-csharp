// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

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

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
