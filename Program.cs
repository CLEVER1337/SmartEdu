using Microsoft.AspNetCore.Rewrite;
using SmartEdu.FileLogger;



var builder = WebApplication.CreateBuilder(args);

// json config
builder.Configuration.AddJsonFile("Config/appsettings.json");

// file logger
builder.Logging.AddFile(builder.Configuration["Logging:LoggerFileName"]!);

// modules services
builder.Services.RegisterModules();



var app = builder.Build();

// connect URL-rewriter
var options = new RewriteOptions()
            .AddRewrite("^$", "documents/Main_page.html", false);

app.UseRewriter(options);

// modules endpoints
app.MapEndpoints();

// error handling
app.UseStatusCodePagesWithReExecute("/error/{0}");

// static files
app.UseStaticFiles();

app.Run("https://localhost:228");
