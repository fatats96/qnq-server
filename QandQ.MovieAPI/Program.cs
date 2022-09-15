using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using QandQ.Core.Repositories;
using QandQ.Data.Context;
using QandQ.Data.Repositories;
using QandQ.MovieAPI.Common;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
var authenticationSettings = builder.Configuration.GetSection("Authentication").Get<AuthenticationSettings>();

builder.Services.AddControllers();

builder.Services.AddDbContext<MovieContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), x=> x.MigrationsAssembly("QandQ.Data")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(options =>
//{
//    options.Authority = authenticationSettings.Authority;
//    options.Audience = authenticationSettings.Audience;
//});

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

app.Run();
