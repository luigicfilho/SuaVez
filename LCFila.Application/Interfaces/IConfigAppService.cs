using LCFila.Domain.Models;

namespace LCFila.Application.Interfaces;

public interface IConfigAppService
{
    EmpresaLogin GetConfigEmpresa(string UserName);
}
