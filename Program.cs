using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using SmartEdu;
using SmartEdu.FileLogger;
using SmartEdu.Modules.UserModule.Core;
using SmartEdu.Modules.HashingModule.Adapters;
using SmartEdu.Modules.HashingModule.Ports;
using System.Security.Cryptography;
using System.Text;

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

app.Map("/hello", (IHashService hashService) =>
{
    using(var context = new ApplicationContext(builder.Configuration["ConnectionStrings:SmartEduConnection"]!))
    {
        hashService.HashFunction("gavno");

        var user = new Tutor("Oleg", hashService.Salt, hashService.Hash);

        context.Users.Add(user);

        context.SaveChanges();

        user.UserData.UserId = user.Id;

        context.SaveChanges();
    }
});


app.Map("/main", (HttpContext httpContext, IHashService hashService) =>
{
    using (var context = new ApplicationContext(builder.Configuration["ConnectionStrings:SmartEduConnection"]!))
    {
        var user = context.Users.Include(u => u.UserData).FirstOrDefault();

        hashService.HashFunction("gavno", user?.UserData?.Salt!);

        httpContext.Response.WriteAsync(user.UserData.HashedPassword + "\n" + hashService.Hash);
    }
});

app.Run("https://localhost:228");
