using Microsoft.AspNetCore.Builder;

namespace LCFila.Application.Interfaces.Configuration;

public interface ILCFilaInitializer
{
    void Initialize(WebApplication app);
}
