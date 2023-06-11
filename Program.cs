using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Rewrite;
using SmartEdu;
using SmartEdu.FileLogger;


var builder = WebApplication.CreateBuilder(args);

// json config
builder.Configuration.AddJsonFile("Config/appsettings.json");

// file logger
builder.Logging.AddFile(builder.Configuration["LoggerFileName"]!);

// database connection string
ApplicationContext.connectionString = builder.Configuration["ConnectionStrings:SmartEduConnection"]!;

// modules services
builder.Services.RegisterModules();

// authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters 
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Authentication:Audience"],
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Authentication:Key"]!)),
            ValidateIssuerSigningKey = true
        };
    });

// authorization
// claims policy
builder.Services.AddAuthorization(options =>
{

});



var app = builder.Build();

// auth
app.UseAuthentication();
app.UseAuthorization();

// connect URL-rewriter
var options = new RewriteOptions()
            .AddRewrite("^$", "documents/Main_page.html", false)
            .AddRewrite("registration", "documents/Tutor_Authorization.html", false);

app.UseRewriter(options);

// modules endpoints
app.MapEndpoints();

// error handling
app.UseStatusCodePagesWithReExecute("/error/{0}");

// static files
app.UseStaticFiles();

app.Run("https://localhost:228");
