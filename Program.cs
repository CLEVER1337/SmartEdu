using SmartEdu.Modules.ErrorHandling.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterModules();



var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapEndpoints();

app.Run();
