using LCFila.Web.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureApplication();

var app = builder.Build();

app.InitializeApplication();

app.Run();