var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterModules();



var app = builder.Build();

app.UseStatusCodePagesWithReExecute("/error/{0}");

app.MapEndpoints();

app.Run("http://localhost:228");
