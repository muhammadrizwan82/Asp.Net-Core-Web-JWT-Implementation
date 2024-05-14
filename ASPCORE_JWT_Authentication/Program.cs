using ASPCORE_JWT_Authentication;
using ASPCORE_JWT_Authentication.Models;
using ASPCORE_JWT_Authentication.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.PrivateKey)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
builder.Services.AddTransient<AuthService>();
builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("tech", p => p.RequireRole("developer"));
});
var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/login", (AuthService service) =>
{ 
    var user = new User(
        1,
        "bruno.bernardes",
        "Bruno Bernardes",
        "bruno@gmail.com",
        "q1w2e3r4t5",
        new string[] { "manager" }
        );

    return service.Create(user);
});

app.MapGet("/test", () => "OK!").RequireAuthorization();
app.MapGet("/test/tech", () => "tech OK!").RequireAuthorization("tech");

app.Run();
