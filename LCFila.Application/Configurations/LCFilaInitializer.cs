using LCFila.Application.Interfaces.Configuration;
using LCFila.Application.Extensions;
using Microsoft.AspNetCore.Builder;

namespace LCFila.Application.Configurations;

internal class LCFilaInitializer : ILCFilaInitializer
{
    public void Initialize(WebApplication app)
    {
        app.UseLCFila();
    }
}
