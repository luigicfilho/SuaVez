using LCFila.Application.Dto;
using LCFila.Web.Models.Empresa;
using LCFila.Web.Models.User;

namespace LCFila.Web.Mapping;

public static class EmpresaMapping
{
    public static EmpresaConfiguracaoDto ConvertToEmpresaConfiguracaoDto(this EmpresaConfiguracaoViewModel empresaConfiguracaoViewModel)
    {
        EmpresaConfiguracaoDto empresaConfig = new()
        {
            Id = empresaConfiguracaoViewModel.Id,
            CorPrincipalEmpresa = empresaConfiguracaoViewModel.CorPrincipalEmpresa,
            NomeDaEmpresa = empresaConfiguracaoViewModel.NomeDaEmpresa,
            CorSegundariaEmpresa = empresaConfiguracaoViewModel.CorSegundariaEmpresa,
            FooterEmpresa = empresaConfiguracaoViewModel.FooterEmpresa,
            LinkLogodaEmpresa = empresaConfiguracaoViewModel.LinkLogodaEmpresa
        };

        return empresaConfig;
    }

    public static EmpresaConfiguracaoViewModel ConvertToEmpresaConfiguracaoDto(this EmpresaConfiguracaoDto empresaConfiguracaoViewModel)
    {
        EmpresaConfiguracaoViewModel empresaConfig = new()
        {
            Id = empresaConfiguracaoViewModel.Id,
            CorPrincipalEmpresa = empresaConfiguracaoViewModel.CorPrincipalEmpresa,
            NomeDaEmpresa = empresaConfiguracaoViewModel.NomeDaEmpresa,
            CorSegundariaEmpresa = empresaConfiguracaoViewModel.CorSegundariaEmpresa,
            FooterEmpresa = empresaConfiguracaoViewModel.FooterEmpresa,
            LinkLogodaEmpresa = empresaConfiguracaoViewModel.LinkLogodaEmpresa
        };

        return empresaConfig;
    }

    public static EmpresaLoginDto ConvertToEmpresaLogin(this EmpresaLoginViewModel empresaloginViewModel)
    {
        EmpresaConfiguracaoDto empconfig = new()
        {
            CorSegundariaEmpresa = "",
            CorPrincipalEmpresa = "",
            FooterEmpresa = "",
            LinkLogodaEmpresa = "http://",
            NomeDaEmpresa = empresaloginViewModel.NomeEmpresa
        };
        if (empresaloginViewModel.EmpresaConfiguracao is not null)
        {
            empconfig = empresaloginViewModel.EmpresaConfiguracao.ConvertToEmpresaConfiguracaoDto();
        }

        EmpresaLoginDto empresaLogin = new()
        {
            Id = empresaloginViewModel.Id,
            IdAdminEmpresa = empresaloginViewModel.IdAdminEmpresa,
            CNPJ = empresaloginViewModel.CNPJ,
            EmpresaConfiguracao = empconfig,
            EmpresaFilas = empresaloginViewModel.EmpresaFilas!.Any() ? empresaloginViewModel.EmpresaFilas!.ConvertToListFila() : [],
            NomeEmpresa = empresaloginViewModel.NomeEmpresa,
            UsersEmpresa = empresaloginViewModel.UsersEmpresa!.ConvertToDto(),
            Ativo = empresaloginViewModel.Ativo
        };

        return empresaLogin;
    }

    public static EmpresaLoginViewModel ConvertToEmpresaLoginViewModel(this EmpresaLoginDto empresalogin)
    {
        EmpresaLoginViewModel empresaLoginview = new()
        {
            Id = empresalogin.Id,
            IdAdminEmpresa = empresalogin.IdAdminEmpresa,
            CNPJ = empresalogin.CNPJ,
            EmpresaConfiguracao = empresalogin.EmpresaConfiguracao!.ConvertToEmpresaConfiguracaoDto(),
            EmpresaFilas = empresalogin.EmpresaFilas!.ConvertToListFilaViewModel(),
            NomeEmpresa = empresalogin.NomeEmpresa,
            UsersEmpresa = empresalogin.UsersEmpresa!.ConvertToViewModel(),
            Ativo = empresalogin.Ativo
        };

        return empresaLoginview;
    }

    public static IEnumerable<EmpresaLoginViewModel> ConvertToEmpresaLoginViewModel(this IEnumerable<EmpresaLoginDto>? empresalogin)
    {
        List<EmpresaLoginViewModel> viewlist = [];
        if(empresalogin is not null)
        {
            foreach (var emp in empresalogin)
            {
                if (!(emp.NomeEmpresa == "System"))
                {
                    viewlist.Add(new EmpresaLoginViewModel()
                    {
                        Id = emp.Id,
                        IdAdminEmpresa = emp.IdAdminEmpresa,
                        CNPJ = emp.CNPJ,
                        EmpresaConfiguracao = emp.EmpresaConfiguracao!.ConvertToEmpresaConfiguracaoDto(),
                        EmpresaFilas = emp.EmpresaFilas!.ConvertToListFilaViewModel(),
                        NomeEmpresa = emp.NomeEmpresa,
                        UsersEmpresa = emp.UsersEmpresa!.ConvertToViewModel(),
                        Ativo = emp.Ativo
                    });
                }

            }
        }
        
        IEnumerable<EmpresaLoginViewModel> list = viewlist;
        return list;
    }

    public static List<AppUserViewModel> ConvertToViewModel(this List<AppUserDto> appUserDtos)
    {
        List<AppUserViewModel> appUserViewModels = [];
        foreach(var appuser in appUserDtos)
        {
            appUserViewModels.Add(new AppUserViewModel
            {
                Id = Guid.Parse(appuser.Id),
                Email = appuser.Email,
                PhoneNumber = appuser.PhoneNumber
            });
        }
        return appUserViewModels;
    }

    public static List<AppUserDto> ConvertToDto(this List<AppUserViewModel> appUserViewModels)
    {
        List<AppUserDto> appUserDtoList = [];
        foreach (var appuser in appUserViewModels)
        {
            appUserDtoList.Add(new AppUserDto
            {
                Id = appuser.Id.ToString(),
                Email = appuser.Email,
                PhoneNumber = appuser.PhoneNumber
            });
        }
        return appUserDtoList;
    }

}
