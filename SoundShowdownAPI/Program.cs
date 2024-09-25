using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Logging;
using System.Security.Principal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Set up authentication via Microsoft Identity

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(options =>
    {
        builder.Configuration.Bind("AzureAdB2C", options);
        options.TokenValidationParameters.NameClaimType = "name";

        options.Events = new JwtBearerEvents
        {
            OnChallenge = context =>
            {
                Console.WriteLine(context.AuthenticateFailure?.Message);
                return Task.CompletedTask;
            },

            OnForbidden = context =>
            {
                return Task.CompletedTask;
            },

            OnAuthenticationFailed = context =>
            {
                Console.WriteLine(context.Exception?.Message);
                return Task.CompletedTask;
            }
        };
    },
    options =>
    {
        builder.Configuration.Bind("AzureAdB2C", options);
    });



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

// TEMPORARY - DO NOT CHECK IN
IdentityModelEventSource.ShowPII = true;
IdentityModelEventSource.LogCompleteSecurityArtifact = true;

app.Run();
