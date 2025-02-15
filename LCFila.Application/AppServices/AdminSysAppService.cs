using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using LCFila.Application.Helpers;
using LCFila.Application.Interfaces;
using LCFila.Domain.Models;
using LCFila.Infra.Interfaces;

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

    public Results<EmpresaLogin> CreateEmpresa(EmpresaLogin empresaLogin, string email, string password)
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
            NomeDaEmpresa = empresaLogin.NomeEmpresa,
            LinkLogodaEmpresa = "http://",
            CorPrincipalEmpresa = "black",
            CorSegundariaEmpresa = "black",
            FooterEmpresa = "no footer"
        };

        //EmpresaLogin empresaLogin = new();

        empresaLogin.IdAdminEmpresa = Guid.Parse(user.Id);
        empresaLogin.UsersEmpresa = listausers;
        empresaLogin.EmpresaFilas = new();
        empresaLogin.EmpresaConfiguracao = empconfig;

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
                return Results<EmpresaLogin>.Failure(Error.GenericFailure("APP01", "Was not possible to confirm the email."));
            }
        }

        return Results<EmpresaLogin>.Success(empresaLogin!);
    }

    public async Task<EmpresaLogin> EditEmpresa(EmpresaLogin empresaLogin)
    {
        await _empresaRepository.Atualizar(empresaLogin);
        await _empresaRepository.SaveChanges();
        return empresaLogin;
    }

    public async Task<IEnumerable<EmpresaLogin>> GetAllEmpresas()
    {
        return await _empresaRepository.ObterTodos();
    }

    public async Task<EmpresaLogin> GetEmpresaDetail(Guid Id)
    {
        var retorno = await _empresaRepository!.ObterPorId(Id);
        return retorno!;
    }

    public async Task<AppUser> GetEmpresaAdmin(string IdAdminEmpresa)
    {
        var retorno = await _userManager.FindByIdAsync(IdAdminEmpresa);
        return retorno!;
    }
    public async Task<EmpresaLogin> RemoveEmpresa(Guid Id)
    {
        var empresa = await _empresaRepository.ObterPorId(Id);

        var adminempresa = await _userManager.FindByIdAsync(empresa!.IdAdminEmpresa.ToString());
        await _userManager.RemoveFromRoleAsync(adminempresa!, "EmpAdmin");
        await _userManager.DeleteAsync(adminempresa!);
        await _empresaRepository.Remover(empresa.Id);
        await _empresaRepository.SaveChanges();
        return empresa;
    }

    public async Task<EmpresaConfiguracao> GetEmpresaConfiguracao(string userName)
    {
        IQueryable<AppUser> AllUsers = _userManager.Users;
        AppUser admin = AllUsers.FirstOrDefault(p => p.UserName == userName)!;
        List<EmpresaLogin> AllEmpresas = await _empresaRepository.ObterTodos();
        EmpresaLogin empresa = AllEmpresas.FirstOrDefault(p => p.IdAdminEmpresa == Guid.Parse(admin.Id))!;
        IEnumerable<EmpresaConfiguracao> config = await _empresaConfigRepository.Buscar(p => p.NomeDaEmpresa == empresa.NomeEmpresa);
        EmpresaConfiguracao empcofig = config.FirstOrDefault()!;
        return empcofig;
    }

    public async Task<EmpresaConfiguracao> UpdateEmpresaConfiguracao(Guid Id, EmpresaConfiguracao empresaConfiguracao, string filePath)
    {
        List<EmpresaLogin> AllEmpresas = await _empresaRepository.ObterTodos();
        EmpresaLogin empresa = AllEmpresas.FirstOrDefault(p => p.EmpresaConfiguracao.Id == Id)!;
        //empresa.NomeEmpresa = empconfig.NomeDaEmpresa;
        //EmpresaConfiguracao empconfigs = new EmpresaConfiguracao()
        //{
        //    Id = empconfig.Id,
        //    NomeDaEmpresa = empconfig.NomeDaEmpresa,
        //    LinkLogodaEmpresa = empconfig.LinkLogodaEmpresa,
        //    CorPrincipalEmpresa = empconfig.CorPrincipalEmpresa,
        //    CorSegundariaEmpresa = empconfig.CorSegundariaEmpresa,
        //    FooterEmpresa = empconfig.FooterEmpresa
        //};
        if (filePath is null)
        {
            empresaConfiguracao.LinkLogodaEmpresa = empresa.EmpresaConfiguracao.LinkLogodaEmpresa;
        }
        empresa.EmpresaConfiguracao = empresaConfiguracao;
        await _empresaRepository.Atualizar(empresa);
        await _empresaRepository.SaveChanges();

        return empresaConfiguracao;
    }
}
