using LCFila.ViewModels;
using LCFilaApplication.Mapping;
using LCFilaApplication.Models;

namespace LCFila.Mapping;

public static class EmpresaMapping
{
    public static EmpresaLogin ConvertToEmpresaLogin(this EmpresaLoginViewModel empresaloginViewModel)
    {
        EmpresaLogin empresaLogin = new()
        {
            Id = empresaloginViewModel.Id,
            IdAdminEmpresa = empresaloginViewModel.IdAdminEmpresa,
            CNPJ = empresaloginViewModel.CNPJ,
            EmpresaConfiguracao = empresaloginViewModel.EmpresaConfiguracao.ConvertToEmpresaConfiguracao(),
            EmpresaFilas = empresaloginViewModel.EmpresaFilas.ConvertToListFila(),
            NomeEmpresa = empresaloginViewModel.NomeEmpresa,
            UsersEmpresa = empresaloginViewModel.UsersEmpresa,
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
            EmpresaFilas = empresalogin.EmpresaFilas.ConvertToListFilaViewModel(),
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


    public static IEnumerable<EmpresaLoginViewModel> ConvertToEmpresaLoginViewModel(this List<EmpresaLogin> empresalogin)
    {
        List<EmpresaLoginViewModel> viewlist = [];
        foreach(var emp in empresalogin)
        {
            viewlist.Add(new EmpresaLoginViewModel()
            {
                Id = emp.Id,
                IdAdminEmpresa = emp.IdAdminEmpresa,
                CNPJ = emp.CNPJ,
                EmpresaConfiguracao = emp.EmpresaConfiguracao.ConvertToEmpresaConfiguracaoView(),
                EmpresaFilas = emp.EmpresaFilas.ConvertToListFilaViewModel(),
                NomeEmpresa = emp.NomeEmpresa,
                UsersEmpresa = emp.UsersEmpresa,
                Ativo = emp.Ativo
            });
        }
        IEnumerable<EmpresaLoginViewModel> list = viewlist;
        return list;
    }
}
