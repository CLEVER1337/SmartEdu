using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Rewrite;
using SmartEdu;
using SmartEdu.FileLogger;
using SmartEdu.Modules.SessionModule.Adapters;
using SmartEdu.Modules.SessionModule.Core;

var builder = WebApplication.CreateBuilder(args);

// json config
builder.Configuration.AddJsonFile("Config/appsettings.json");

// file logger
builder.Logging.AddFile(builder.Configuration["LoggerFileName"]!);

// database connection string
ApplicationContext.connectionString = builder.Configuration["ConnectionStrings:SmartEduConnection"]!;

// authentication
var tokenOptions = new AuthenticationTokenOptions{issuer = builder.Configuration["Authentication:Issuer"],
                                                  audience = builder.Configuration["Authentication:Audience"],
                                                  key = builder.Configuration["Authentication:Key"] };

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters 
        {
            ValidateIssuer = true,
            ValidIssuer = tokenOptions.issuer,
            ValidateAudience = true,
            ValidAudience = tokenOptions.audience,
            ValidateLifetime = true,
            IssuerSigningKey = tokenOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true
        };
    });

// authorization
// claims policy
builder.Services.AddAuthorization(options =>
{
    
});

// redis cache
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["RedisCache:Configuration"];
    options.InstanceName = builder.Configuration["RedisCache:InstanceName"];
    options.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions 
    { 
        AbortOnConnectFail = false 
    };
});

// modules services
builder.Services.RegisterModules();



var app = builder.Build();

// connect URL-rewriter
var options = new RewriteOptions()
            .AddRewrite("user/choose", "documents/ChooseUser.html", false)
            .AddRewrite("tutor/registration", "documents/TutorRegistration.html", false)
            .AddRewrite("student/registration", "documents/StudentRegistration.html", false)
            .AddRewrite("tutor/authorization", "documents/TutorAuthorization.html", false)
            .AddRewrite("student/authorization", "documents/StudentAuthorization.html", false);
app.UseRewriter(options);

// static files + default file
var staticFilesOptions = new FileServerOptions();
staticFilesOptions.DefaultFilesOptions.DefaultFileNames.Clear();
staticFilesOptions.DefaultFilesOptions.DefaultFileNames.Add("/documents/MainPage.html");
app.UseFileServer(staticFilesOptions);

// auth
app.UseAuthentication();
app.UseAuthorization();

// tokens checking before use
app.UseTokenValidationChecking();

// set user's session service's options
SessionService.tokenOptions = tokenOptions;

// error handling
app.UseStatusCodePagesWithReExecute("/error/{0}");

// modules endpoints
app.MapEndpoints();

app.Run();
