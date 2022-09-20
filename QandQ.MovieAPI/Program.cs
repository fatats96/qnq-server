using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QandQ.Core.Repositories;
using QandQ.Data.Context;
using QandQ.Data.Repositories;
using QandQ.MovieAPI.Common;
using QandQ.Services;
using Microsoft.AspNetCore.Http.Json;
using JsonOptions = Microsoft.AspNetCore.Http.Json.JsonOptions;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
var authenticationSettings = builder.Configuration.GetSection("Authentication").Get<AuthenticationSettings>();

builder.Services.AddControllers();

builder.Services.AddDbContext<MovieContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), x => x.MigrationsAssembly("QandQ.Data")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//builder.Services.AddHostedService<GetGenresHostedService>();
//builder.Services.AddHostedService<GetTopRatedMoviesHostedService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        corsBuilder => corsBuilder
            .WithOrigins(new string[] { "http://localhost:3000" })
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.Authority = authenticationSettings.Authority;
    options.Audience = authenticationSettings.Audience;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidIssuer = authenticationSettings.Authority,
        ValidateAudience = false,
        ValidAudience = authenticationSettings.Audience,
        ValidateLifetime = true
    };
});

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
app.UseCors();
//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
