using LCFila.Application.Dto;
using LCFila.Application.Helpers;

namespace LCFila.Application.Interfaces;

public interface IAdminSysAppService
{
    Task<IEnumerable<EmpresaLoginDto>> GetAllEmpresas();
    Task<EmpresaLoginDto> GetEmpresaDetail(Guid Id);
    Task ActivateToggleEmpresa(Guid Id, bool toggle);
    Task<AppUserDto> GetEmpresaAdmin(string IdAdminEmpresa);
    Results<EmpresaLoginDto> CreateEmpresa(EmpresaLoginDto empresaLogin, string email, string password);
    Task<EmpresaLoginDto> EditEmpresa(EmpresaLoginDto empresaLogin);
    Task RemoveEmpresa(Guid Id);
    Task<EmpresaConfiguracaoDto> GetEmpresaConfiguracao(string userName);
    Task<EmpresaConfiguracaoDto> UpdateEmpresaConfiguracao(Guid Id, EmpresaConfiguracaoDto empresaConfiguracao, string filePath);
}
