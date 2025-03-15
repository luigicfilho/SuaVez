using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using LCFila.Application.Interfaces;
using LCFila.Domain.Models;
using LCFila.Infra.Interfaces;
using LCFila.Infra.External;
using LCFila.Application.Dto;
using LCFila.Application.Mappers;

namespace LCFila.Application.AppServices;

public class AdminSysAppService : IAdminSysAppService
{
    private readonly IEmpresaLoginRepository _empresaRepository;
    private readonly IEmpresaConfiguracaoRepository _empresaConfigRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ILogger<AdminSysAppService> _logger;
    private readonly IEmailSender _emailSender;

    public AdminSysAppService(UserManager<AppUser> userManager,
                              RoleManager<IdentityRole> roleManager,
                              ILogger<AdminSysAppService> logger,
                              IEmailSender emailSender,
                              IEmpresaConfiguracaoRepository empresaConfigRepository,
                              IEmpresaLoginRepository empresaRepository)
    {
        _empresaRepository = empresaRepository;
        _empresaConfigRepository = empresaConfigRepository;
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
        _emailSender = emailSender;
    }

    public async Task ActivateToggleEmpresa(Guid Id, bool toggle)
    {
        var empresa = await _empresaRepository.ObterPorId(Id);
        empresa!.Ativo = toggle;
        await _empresaRepository.Atualizar(empresa);
        await _empresaRepository.SaveChanges();
        return;
    }

    public Task<EmpresaLoginDto> CreateEmpresa(EmpresaLoginDto empresaLogindto, string email, string password)
    {
        var user = new AppUser
        {
            Id = Guid.NewGuid().ToString(),
            UserName = email,
            Email = email,
            EmailConfirmed = true
        };

        List<AppUser> listausers = [user];

        EmpresaConfiguracao empconfig = new EmpresaConfiguracao()
        {
            NomeDaEmpresa = empresaLogindto.NomeEmpresa,
            LinkLogodaEmpresa = "http://",
            CorPrincipalEmpresa = "black",
            CorSegundariaEmpresa = "black",
            FooterEmpresa = "no footer"
        };

        EmpresaLogin empresaLogin = new();

        empresaLogin.IdAdminEmpresa = Guid.Parse(user.Id);
        empresaLogin.UsersEmpresa = listausers;
        empresaLogin.EmpresaFilas = new();
        empresaLogin.EmpresaConfiguracao = empconfig;

        empresaLogindto.IdAdminEmpresa = Guid.Parse(user.Id);
        empresaLogindto.EmpresaFilas = new();

        var result = _userManager.CreateAsync(user, password).Result;
        if (result.Succeeded)
        {
            _empresaRepository.Adicionar(empresaLogin);
            var roles = _userManager.AddToRoleAsync(user, "EmpAdmin").Result;
            _logger.LogInformation($"User {empresaLogin.NomeEmpresa} created a new account with password.");
            
            var code = _userManager.GenerateEmailConfirmationTokenAsync(user).Result;

            var resultemail = _userManager.ConfirmEmailAsync(user, code).Result;

            if (!resultemail.Succeeded)
            {
                _userManager.DeleteAsync(user);
                
                //return resultFactory.Failure(new Error("APP01", "Was not possible to confirm the email."));
                //return Results<EmpresaLoginDto>.Failure(Error.GenericFailure("APP01", "Was not possible to confirm the email."));
            }
        }

        return Task.FromResult(empresaLogindto);
        //return Results<EmpresaLoginDto>.Success(empresaLogindto!);
    }

    public async Task<EmpresaLoginDto> EditEmpresa(EmpresaLoginDto empresaLogin)
    {
        var empLogin = await _empresaRepository.ObterPorId(empresaLogin.Id);


        EmpresaLogin empresaLoginDto = new();
        empresaLoginDto = empresaLogin.ConvertToEmpresaLogin();

        await _empresaRepository.Atualizar(empresaLoginDto!);
        await _empresaRepository.SaveChanges();
        return empresaLogin;
    }

    public async Task<IEnumerable<EmpresaLoginDto>> GetAllEmpresas()
    {
        var empresasLogins = await _empresaRepository.ObterTodos();
        if(empresasLogins is not null)
        {
            return empresasLogins.ConvertToEmpresaLoginDto();
        }
        return new List<EmpresaLoginDto>();
    }

    public async Task<EmpresaLoginDto> GetEmpresaDetail(Guid Id)
    {
        var retorno = await _empresaRepository!.ObterPorId(Id);

        if (retorno is not null)
        {
            return retorno.ConvertToDto();
        }
        return new EmpresaLoginDto();
    }

    public async Task<AppUserDto> GetEmpresaAdmin(string IdAdminEmpresa)
    {
        var retorno = await _userManager.FindByIdAsync(IdAdminEmpresa);

        if (retorno is not null)
        {
            return retorno.ConvertToAppUserDto();
        }
        return new AppUserDto();
    }
    public async Task RemoveEmpresa(Guid Id)
    {
        var empresa = await _empresaRepository.ObterPorId(Id);

        var adminempresa = await _userManager.FindByIdAsync(empresa!.IdAdminEmpresa.ToString());
        await _userManager.RemoveFromRoleAsync(adminempresa!, "EmpAdmin");
        await _userManager.DeleteAsync(adminempresa!);
        await _empresaRepository.Remover(empresa.Id);
        await _empresaRepository.SaveChanges();
        
    }

    public async Task<EmpresaConfiguracaoDto> GetEmpresaConfiguracao(string userName)
    {
        IQueryable<AppUser> AllUsers = _userManager.Users;
        AppUser admin = AllUsers.FirstOrDefault(p => p.UserName == userName)!;
        List<EmpresaLogin> AllEmpresas = await _empresaRepository.ObterTodos();
        EmpresaLogin empresa = AllEmpresas.FirstOrDefault(p => p.IdAdminEmpresa == Guid.Parse(admin.Id))!;
        IEnumerable<EmpresaConfiguracao> config = await _empresaConfigRepository.Buscar(p => p.NomeDaEmpresa == empresa.NomeEmpresa);
        EmpresaConfiguracao empcofig = config.FirstOrDefault()!;

        return empcofig.ConvertToDto();
    }

    public async Task<EmpresaConfiguracaoDto> UpdateEmpresaConfiguracao(Guid Id, EmpresaConfiguracaoDto empresaConfiguracao, string filePath)
    {
        List<EmpresaLogin> AllEmpresas = await _empresaRepository.ObterTodos();
        EmpresaLogin empresa = AllEmpresas.FirstOrDefault(p => p.EmpresaConfiguracao.Id == Id)!;
        //empresa.NomeEmpresa = empconfig.NomeDaEmpresa;
        EmpresaConfiguracao empconfigs = new EmpresaConfiguracao()
        {
            Id = empresaConfiguracao.Id,
            NomeDaEmpresa = empresaConfiguracao.NomeDaEmpresa,
            LinkLogodaEmpresa = string.IsNullOrEmpty(empresaConfiguracao.LinkLogodaEmpresa) ? "http://" : empresaConfiguracao.LinkLogodaEmpresa,
            CorPrincipalEmpresa = empresaConfiguracao.CorPrincipalEmpresa,
            CorSegundariaEmpresa = empresaConfiguracao.CorSegundariaEmpresa,
            FooterEmpresa = empresaConfiguracao.FooterEmpresa
        };
        if (filePath is null)
        {
            empresaConfiguracao.LinkLogodaEmpresa = empresa.EmpresaConfiguracao.LinkLogodaEmpresa;
        }
        empresa.EmpresaConfiguracao = empconfigs;
        await _empresaRepository.Atualizar(empresa);
        await _empresaRepository.SaveChanges();

        return empresaConfiguracao;
    }

    public bool IsEmpresaAtiva(string Email)
    {
        //TODO:
        // do a better way to exclude sysadmin for this check
        if (Email.Equals("admin@suavez.com.br", StringComparison.InvariantCultureIgnoreCase))
        {
            return true;
        }
        var AllEmpresas = _empresaRepository.ObterTodos().Result;
        var empresa = AllEmpresas.FirstOrDefault(s => s.UsersEmpresa.Any(p => p.Email == Email));
        return empresa!.Ativo;
    }
}
