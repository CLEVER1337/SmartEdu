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

// modules services
builder.Services.RegisterModules();



var app = builder.Build();

// auth
app.UseAuthentication();
app.UseAuthorization();

// static files + default file(index.html)
var staticFilesOptions = new FileServerOptions();
staticFilesOptions.DefaultFilesOptions.DefaultFileNames.Clear();
staticFilesOptions.DefaultFilesOptions.DefaultFileNames.Add("Main_page.html");
app.UseFileServer(staticFilesOptions);

// connect URL-rewriter
var options = new RewriteOptions();
            //.AddRewrite("registration", "documents/Tutor_registration.html", false);

app.UseRewriter(options);

// error handling
app.UseStatusCodePagesWithReExecute("/error/{0}");

// modules endpoints
app.MapEndpoints();

app.Run("https://localhost:228");
