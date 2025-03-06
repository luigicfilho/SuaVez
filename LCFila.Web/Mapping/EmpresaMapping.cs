using LCFila.ViewModels;
using LCFila.Web.Mapping;

//TODO: how to do it in another way to remove theses references
//Don't need all this information in this layer, just the informations
//that need to be passed down
using LCFila.Domain.Models;

namespace LCFila.Mapping;

public static class EmpresaMapping
{
    public static EmpresaLogin ConvertToEmpresaLogin(this EmpresaLoginViewModel empresaloginViewModel)
    {
        EmpresaConfiguracao empconfig = new()
        {
            CorSegundariaEmpresa = "",
            CorPrincipalEmpresa = "",
            FooterEmpresa = "",
            LinkLogodaEmpresa = "http://",
            NomeDaEmpresa = empresaloginViewModel.NomeEmpresa
        };
        if (empresaloginViewModel.EmpresaConfiguracao is not null)
        {
            empconfig = empresaloginViewModel.EmpresaConfiguracao.ConvertToEmpresaConfiguracao();
        }

        EmpresaLogin empresaLogin = new()
        {
            Id = empresaloginViewModel.Id,
            IdAdminEmpresa = empresaloginViewModel.IdAdminEmpresa,
            CNPJ = empresaloginViewModel.CNPJ,
            EmpresaConfiguracao = empconfig,
            EmpresaFilas = empresaloginViewModel.EmpresaFilas!.Any() ? empresaloginViewModel.EmpresaFilas!.ConvertToListFilaVM() : [],
            NomeEmpresa = empresaloginViewModel.NomeEmpresa,
            UsersEmpresa = empresaloginViewModel.UsersEmpresa!,
            Ativo = empresaloginViewModel.Ativo
        };

        return empresaLogin;
    }

    public static EmpresaLoginViewModel ConvertToEmpresaLoginViewModel(this EmpresaLogin empresalogin)
    {
        EmpresaLoginViewModel empresaLoginview = new()
        {
            Id = empresalogin.Id,
            IdAdminEmpresa = empresalogin.IdAdminEmpresa,
            CNPJ = empresalogin.CNPJ,
            EmpresaConfiguracao = empresalogin.EmpresaConfiguracao.ConvertToEmpresaConfiguracaoView(),
            EmpresaFilas = empresalogin.EmpresaFilas.ConvertoToFilaViewModel(),
            NomeEmpresa = empresalogin.NomeEmpresa,
            UsersEmpresa = empresalogin.UsersEmpresa,
            Ativo = empresalogin.Ativo
        };

        return empresaLoginview;
    }

    public static EmpresaConfiguracao ConvertToEmpresaConfiguracao(this EmpresaConfiguracaoViewModel empresaConfiguracaoViewModel)
    {
        EmpresaConfiguracao empresaConfig = new()
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

    public static EmpresaConfiguracaoViewModel ConvertToEmpresaConfiguracaoView(this EmpresaConfiguracao empresaConfiguracao)
    {
        EmpresaConfiguracaoViewModel empresaConfig = new()
        {
            Id = empresaConfiguracao.Id,
            CorPrincipalEmpresa = empresaConfiguracao.CorPrincipalEmpresa,
            NomeDaEmpresa = empresaConfiguracao.NomeDaEmpresa,
            CorSegundariaEmpresa = empresaConfiguracao.CorSegundariaEmpresa,
            FooterEmpresa = empresaConfiguracao.FooterEmpresa,
            LinkLogodaEmpresa = empresaConfiguracao.LinkLogodaEmpresa
        };

        return empresaConfig;
    }


    public static IEnumerable<EmpresaLoginViewModel> ConvertToEmpresaLoginViewModel(this IEnumerable<EmpresaLogin>? empresalogin)
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
                        EmpresaConfiguracao = emp.EmpresaConfiguracao.ConvertToEmpresaConfiguracaoView(),
                        EmpresaFilas = emp.EmpresaFilas.ConvertoToFilaViewModel(),
                        NomeEmpresa = emp.NomeEmpresa,
                        UsersEmpresa = emp.UsersEmpresa,
                        Ativo = emp.Ativo
                    });
                }

            }
        }
        
        IEnumerable<EmpresaLoginViewModel> list = viewlist;
        return list;
    }

    public static List<FilaViewModel> ConvertoToFilaViewModel(this List<Fila> list)
    {
        List<FilaViewModel> listview = [];
        foreach(var fila in list)
        {
            listview.Add(fila.ConvertToFilaViewModelVM());
        }
        return listview;
    }
}
