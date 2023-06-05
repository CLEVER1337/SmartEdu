using SmartEdu;



var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterModules();

builder.Configuration.AddJsonFile("Config/appsettings.json");

builder.Logging.AddFile(builder.Configuration["Logging:LoggerFileName"]);



var app = builder.Build();

app.UseStatusCodePagesWithReExecute("/error/{0}");

app.MapEndpoints();



app.Map("/", () => 
{
    
});

app.Run("http://localhost:228");
