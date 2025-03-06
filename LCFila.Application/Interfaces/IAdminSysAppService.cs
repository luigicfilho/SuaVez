using LCFila.Application.Dto;
using LCFila.Application.Helpers;
using LCFila.Domain.Models;

namespace LCFila.Application.Interfaces;

public interface IAdminSysAppService
{
    Task<IEnumerable<EmpresaLogin>> GetAllEmpresas();
    Task<EmpresaLogin> GetEmpresaDetail(Guid Id);
    Task ActivateToggleEmpresa(Guid Id, bool toggle);
    Task<AppUser> GetEmpresaAdmin(string IdAdminEmpresa);
    Results<EmpresaLogin> CreateEmpresa(EmpresaLogin empresaLogin, string email, string password);
    Task<EmpresaLogin> EditEmpresa(EmpresaLogin empresaLogin);
    Task<EmpresaLogin> RemoveEmpresa(Guid Id);
    Task<EmpresaConfiguracaoDto> GetEmpresaConfiguracao(string userName);
    Task<EmpresaConfiguracaoDto> UpdateEmpresaConfiguracao(Guid Id, EmpresaConfiguracaoDto empresaConfiguracao, string filePath);
}
