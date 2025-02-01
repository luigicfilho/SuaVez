using LCFilaApplication.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureLCFila(builder.Configuration);

var app = builder.Build();

app.UseLCFila();

app.Run();