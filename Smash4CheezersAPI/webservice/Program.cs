using System.Data;
using DAL;
using DAL.DAO;
using DAL.DAO.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using webservice.Controllers.Interfaces.Helpers;
using webservice.Controllers.Interfaces.Services;
using webservice.Helpers;
using webservice.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
var serverVersion = new MySqlServerVersion(new Version(8, 0, 30));
builder.Services.AddDbContext<S4CDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("SmashForCheezers"), serverVersion));

// DAO
builder.Services.AddScoped<IUsersDAO, UsersDAO>();
builder.Services.AddScoped<ICharactersDAO, CharactersDAO>();

// Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICharacterService, CharacterService>();

// Helpers
builder.Services.AddScoped<ITokenHelper, TokenHelper>();

// Utils
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "myApp",
        ValidAudience = "http://localhost:5183/api/",
        IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(s: builder.Configuration["Jwt:Key"] ?? throw new NoNullAllowedException())),
        RequireExpirationTime = true,
        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();

app.Run();