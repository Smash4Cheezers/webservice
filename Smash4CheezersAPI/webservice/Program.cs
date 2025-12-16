using System.Data;
using DAL;
using DAL.DAO;
using DAL.DAO.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using webservice.Controllers.Interfaces.Services;
using webservice.Helpers;
using webservice.Services;
using webservice.Services.Interfaces.Helpers;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
MySqlServerVersion serverVersion = new MySqlServerVersion(new Version(8, 0, 30));
builder.Services.AddDbContext<S4CDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("SmashForCheezers"), serverVersion));

// DAO
builder.Services.AddScoped<IUsersDao, UsersDao>();
builder.Services.AddScoped<ICharactersDao, CharactersDao>();
builder.Services.AddScoped<ISessionDao, SessionDao>();
builder.Services.AddScoped<ISerieDAO, SerieDAO>();
builder.Services.AddScoped<IChallengeDAO, ChallengeDAO>();

// Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddScoped<ISessionService, SessionService>();
builder.Services.AddScoped<ISerieService, SerieService>();

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
        ValidAudience = "https://localhost:7020/api/",
        IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(s: builder.Configuration["Jwt:Key"] ?? throw new NoNullAllowedException())),
        RequireExpirationTime = true,
        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularPolicy",
        policy => policy.WithOrigins("https://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});


WebApplication app = builder.Build();
app.UseHttpsRedirection();
app.UseCookiePolicy();
app.UseCors("AngularPolicy");

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();