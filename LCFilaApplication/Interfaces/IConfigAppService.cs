using LCFilaApplication.Models;

namespace LCFilaApplication.Interfaces;

public interface IConfigAppService
{
    EmpresaLogin GetConfigEmpresa(string UserName);
}
