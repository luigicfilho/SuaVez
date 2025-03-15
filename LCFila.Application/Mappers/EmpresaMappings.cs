using LCFila.Application.Dto;
using LCFila.Domain.Models;

namespace LCFila.Application.Mappers;

public static class EmpresaMappings
{
    public static EmpresaLogin ConvertToEmpresaLogin(this EmpresaLoginDto empresaLoginDto)
    {
        EmpresaLogin empresaLogin = new()
        {
            NomeEmpresa = empresaLoginDto.NomeEmpresa,
            CNPJ = empresaLoginDto.CNPJ,
            IdAdminEmpresa = empresaLoginDto.IdAdminEmpresa,
            UsersEmpresa = empresaLoginDto.UsersEmpresa is not null ? empresaLoginDto.UsersEmpresa.ConvertToAppUsers() : new(),
            EmpresaConfiguracao = empresaLoginDto.EmpresaConfiguracao is not null ? empresaLoginDto.EmpresaConfiguracao.ConvertToEmpresaConfiuracao() : new(),
            EmpresaFilas = empresaLoginDto.EmpresaFilas is not null ? empresaLoginDto.EmpresaFilas.ConvertToFilas() : new(),
            Ativo = empresaLoginDto.Ativo
        };
        return empresaLogin;
    }

    public static List<EmpresaLogin> ConvertToEmpresaLogin(this List<EmpresaLoginDto> empresaLoginDtos)
    {
        List<EmpresaLogin> empresaLogins = [];
        foreach(var empresa in empresaLoginDtos)
        {
            empresaLogins.Add(empresa.ConvertToEmpresaLogin());
        }
        return empresaLogins;
    }

    public static List<EmpresaLoginDto> ConvertToEmpresaLoginDto(this List<EmpresaLogin> empresaLogins)
    {
        List<EmpresaLoginDto> empresaLoginsDto = [];
        foreach (var empresa in empresaLogins)
        {
            empresaLoginsDto.Add(empresa.ConvertToDto());
        }
        return empresaLoginsDto;
    }

    public static EmpresaLoginDto ConvertToDto(this EmpresaLogin empresaLogin)
    {
        EmpresaLoginDto empresaLoginDto = new()
        {
            NomeEmpresa = empresaLogin.NomeEmpresa,
            CNPJ = empresaLogin.CNPJ,
            IdAdminEmpresa = empresaLogin.IdAdminEmpresa,
            UsersEmpresa = empresaLogin.UsersEmpresa is not null ? empresaLogin.UsersEmpresa.ConvertToAppUsersDto() : new(),
            EmpresaConfiguracao = empresaLogin.EmpresaConfiguracao is not null ? empresaLogin.EmpresaConfiguracao.ConvertToDto() : new(),
            EmpresaFilas = empresaLogin.EmpresaFilas is not null ? empresaLogin.EmpresaFilas.ConvertToFilasDto() : new(),
            Ativo = empresaLogin.Ativo
        };
        return empresaLoginDto;
    }

    public static EmpresaConfiguracao ConvertToEmpresaConfiuracao(this EmpresaConfiguracaoDto empresaConfiguracaoDto)
    {
        EmpresaConfiguracao empConf = new()
        {
            Id = empresaConfiguracaoDto.Id,
            NomeDaEmpresa = empresaConfiguracaoDto.NomeDaEmpresa,
            LinkLogodaEmpresa = empresaConfiguracaoDto.LinkLogodaEmpresa,
            CorPrincipalEmpresa = empresaConfiguracaoDto.CorPrincipalEmpresa,
            CorSegundariaEmpresa = empresaConfiguracaoDto.CorSegundariaEmpresa,
            FooterEmpresa = empresaConfiguracaoDto.FooterEmpresa
        };
        return empConf;
    }

    public static EmpresaConfiguracaoDto ConvertToDto(this EmpresaConfiguracao empresaConfiguracao)
    {
        EmpresaConfiguracaoDto empConf = new()
        {
            Id = empresaConfiguracao.Id,
            NomeDaEmpresa = empresaConfiguracao.NomeDaEmpresa,
            LinkLogodaEmpresa = empresaConfiguracao.LinkLogodaEmpresa,
            CorPrincipalEmpresa = empresaConfiguracao.CorPrincipalEmpresa,
            CorSegundariaEmpresa = empresaConfiguracao.CorSegundariaEmpresa,
            FooterEmpresa = empresaConfiguracao.FooterEmpresa
        };
        return empConf;
    }
}
