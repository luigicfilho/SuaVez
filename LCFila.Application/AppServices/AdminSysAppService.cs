using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using LCFila.Application.Helpers;
using LCFila.Application.Interfaces;
using LCFila.Domain.Models;
using LCFila.Infra.Interfaces;
using LCFila.Infra.External;
using LCFila.Application.Dto;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using LCFila.Domain.Enums;

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

    public Results<EmpresaLoginDto> CreateEmpresa(EmpresaLoginDto empresaLogindto, string email, string password)
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
                return Results<EmpresaLoginDto>.Failure(Error.GenericFailure("APP01", "Was not possible to confirm the email."));
            }
        }

        return Results<EmpresaLoginDto>.Success(empresaLogindto!);
    }

    public async Task<EmpresaLoginDto> EditEmpresa(EmpresaLoginDto empresaLogin)
    {
        var empLogin = await _empresaRepository.ObterPorId(empresaLogin.Id);
        //TODO: Update what it's needed
        List<AppUser> appuserlist = [];
        foreach (var appuser in empresaLogin.UsersEmpresa!)
        {
            appuserlist.Add(new()
            {
                Email = appuser.Email!,
                PhoneNumber = appuser.PhoneNumber!,
                Id = appuser.Id,
                UserName = appuser.UserName!
            });
        }

        List<Fila> filalist = [];
        foreach (var filas in empresaLogin.EmpresaFilas!)
        {
            filalist.Add(new()
            {
                Id = filas.Id,
                Nome = filas.Nome,
                DataInicio = filas.DataInicio,
                DataFim = filas.DataFim,
                Tipofila = Enum.Parse<TiposFilas>(filas.Tipofila),
                Status = Enum.Parse<FilaStatus>(filas.Status),
                Ativo = filas.Ativo,
                TempoMedio = filas.TempoMedio
            });
        }

        EmpresaConfiguracao empConfDto = new()
        {
            Id = empresaLogin.EmpresaConfiguracao!.Id,
            NomeDaEmpresa = empresaLogin.EmpresaConfiguracao.NomeDaEmpresa,
            LinkLogodaEmpresa = empresaLogin.EmpresaConfiguracao.LinkLogodaEmpresa,
            CorPrincipalEmpresa = empresaLogin.EmpresaConfiguracao.CorPrincipalEmpresa,
            CorSegundariaEmpresa = empresaLogin.EmpresaConfiguracao.CorSegundariaEmpresa,
            FooterEmpresa = empresaLogin.EmpresaConfiguracao.FooterEmpresa
        };

        EmpresaLogin empresaLoginDto = new()
        {
            NomeEmpresa = empresaLogin!.NomeEmpresa,
            CNPJ = empresaLogin!.CNPJ,
            IdAdminEmpresa = empresaLogin.IdAdminEmpresa,
            UsersEmpresa = appuserlist,
            EmpresaConfiguracao = empConfDto,
            EmpresaFilas = filalist,
            Ativo = empresaLogin.Ativo
        };
        await _empresaRepository.Atualizar(empresaLoginDto!);
        await _empresaRepository.SaveChanges();
        return empresaLogin;
    }

    public async Task<IEnumerable<EmpresaLoginDto>> GetAllEmpresas()
    {
        var empresasLogins = await _empresaRepository.ObterTodos();
        List<AppUserDto> appuserlist = [];

        List<FilaDto> filalist = [];

        List<EmpresaLoginDto> empresaLoginDtos = [];
        foreach (var emps in empresasLogins)
        {
            foreach (var appuser in emps.UsersEmpresa)
            {
                appuserlist.Add(new()
                {
                    Email = appuser.Email!,
                    PhoneNumber = appuser.PhoneNumber!,
                    Id = appuser.Id,
                    UserName = appuser.UserName!
                });
            }
            foreach (var filas in emps.EmpresaFilas)
            {
                filalist.Add(new()
                {
                    Id = filas.Id,
                    Nome = filas.Nome,
                    DataInicio = filas.DataInicio,
                    DataFim = filas.DataFim,
                    Tipofila = filas.Tipofila.ToString(),
                    Status = filas.Status.ToString(),
                    Ativo = filas.Ativo,
                    TempoMedio = filas.TempoMedio
                });
            }
            EmpresaConfiguracaoDto empConfDto = new()
            {
                Id = emps.EmpresaConfiguracao.Id,
                NomeDaEmpresa = emps.EmpresaConfiguracao.NomeDaEmpresa,
                LinkLogodaEmpresa = emps.EmpresaConfiguracao.LinkLogodaEmpresa,
                CorPrincipalEmpresa = emps.EmpresaConfiguracao.CorPrincipalEmpresa,
                CorSegundariaEmpresa = emps.EmpresaConfiguracao.CorSegundariaEmpresa,
                FooterEmpresa = emps.EmpresaConfiguracao.FooterEmpresa
            };
            empresaLoginDtos.Add(new EmpresaLoginDto
            {
                Id = emps.Id,
                NomeEmpresa = emps!.NomeEmpresa,
                CNPJ = emps!.CNPJ,
                IdAdminEmpresa = emps.IdAdminEmpresa,
                UsersEmpresa = appuserlist,
                EmpresaConfiguracao = empConfDto,
                EmpresaFilas = filalist,
                Ativo = emps.Ativo
            });
        }
        return empresaLoginDtos;
    }

    public async Task<EmpresaLoginDto> GetEmpresaDetail(Guid Id)
    {
        var retorno = await _empresaRepository!.ObterPorId(Id);
        List<AppUserDto> appuserlist = [];

        foreach(var appuser in retorno!.UsersEmpresa)
        {
            appuserlist.Add(new()
            {
                Email = appuser.Email!,
                PhoneNumber = appuser.PhoneNumber!,
                Id = appuser.Id,
                UserName = appuser.UserName!
            });
        }

        List<FilaDto> filalist = [];
        foreach (var filas in retorno!.EmpresaFilas)
        {
            filalist.Add(new()
            {
                Id = filas.Id,
                Nome = filas.Nome,
                DataInicio = filas.DataInicio,
                DataFim = filas.DataFim,
                Tipofila = filas.Tipofila.ToString(),
                Status = filas.Status.ToString(),
                Ativo = filas.Ativo,
                TempoMedio = filas.TempoMedio
            });
        }

        EmpresaConfiguracaoDto empConfDto = new()
        {
            Id = retorno.EmpresaConfiguracao.Id,
            NomeDaEmpresa = retorno.EmpresaConfiguracao.NomeDaEmpresa,
            LinkLogodaEmpresa = retorno.EmpresaConfiguracao.LinkLogodaEmpresa,
            CorPrincipalEmpresa = retorno.EmpresaConfiguracao.CorPrincipalEmpresa,
            CorSegundariaEmpresa = retorno.EmpresaConfiguracao.CorSegundariaEmpresa,
            FooterEmpresa = retorno.EmpresaConfiguracao.FooterEmpresa
        };

        EmpresaLoginDto empresaLoginDto = new()
        {
            NomeEmpresa = retorno!.NomeEmpresa,
            CNPJ = retorno!.CNPJ,
            IdAdminEmpresa = retorno.IdAdminEmpresa,
            UsersEmpresa = appuserlist,
            EmpresaConfiguracao = empConfDto,
            EmpresaFilas = filalist,
            Ativo = retorno.Ativo
        };

        return empresaLoginDto;
    }

    public async Task<AppUserDto> GetEmpresaAdmin(string IdAdminEmpresa)
    {
        var retorno = await _userManager.FindByIdAsync(IdAdminEmpresa);

        AppUserDto appUserDto = new()
        {
            Email = retorno!.Email!,
            PhoneNumber = retorno.PhoneNumber!,
            Id = retorno.Id,
            UserName = retorno!.UserName!
        };

        return appUserDto!;
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

        EmpresaConfiguracaoDto empresaConfiguracaoDto = new()
        {
            CorPrincipalEmpresa = empcofig.CorPrincipalEmpresa,
            CorSegundariaEmpresa = empcofig.CorSegundariaEmpresa,
            Id = empcofig.Id,
            NomeDaEmpresa = empcofig.NomeDaEmpresa,
            FooterEmpresa = empcofig.FooterEmpresa,
            LinkLogodaEmpresa = empcofig.LinkLogodaEmpresa
        };

        return empresaConfiguracaoDto;
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
}
