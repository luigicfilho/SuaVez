using LCFila.Application.Interfaces;
using LCFila.Domain.Models;
using LCFila.Infra.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace LCFila.Application.AppServices;

public class ConfigAppService : IConfigAppService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IEmpresaLoginRepository _empresaRepository;

    public ConfigAppService(UserManager<AppUser> userManager,
                            IEmpresaLoginRepository empresaRepository)
    {
        _empresaRepository = empresaRepository;
        _userManager = userManager;
    }
    public EmpresaLogin GetConfigEmpresa(string userName)
    {
        var user = _userManager.Users.SingleOrDefault(p => p.UserName == userName);
        if (user == null)
        {
            // User cannot be null
            ArgumentNullException.ThrowIfNull(user);
        }
        var empresa = _empresaRepository.ObterTodos().Result.SingleOrDefault(p => p.IdAdminEmpresa == Guid.Parse(user!.Id));
        return empresa!;
    }
}
