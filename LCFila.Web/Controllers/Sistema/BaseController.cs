using Microsoft.AspNetCore.Mvc;
using LCFila.Application.Interfaces;

namespace LCFila.Controllers.Sistema;

public abstract class BaseController : Controller
{
    private readonly INotificador _notificador;
    private readonly IConfigAppService _configAppService;

    protected BaseController(INotificador notificador,
                             IConfigAppService configAppService)
    {
        _notificador = notificador;
        _configAppService = configAppService;
    }

    protected void ConfigEmpresa()
    {
        string userName = User.Identity is not null ? User.Identity!.Name! : "";
        var empresa = _configAppService.GetConfigEmpresa(userName);
        if (empresa != null)
        {
            ViewBag.bgcolor = empresa!.EmpresaConfiguracao.CorPrincipalEmpresa;
            ViewBag.btcolor = empresa.EmpresaConfiguracao.CorSegundariaEmpresa;
            ViewBag.logo = empresa.EmpresaConfiguracao.LinkLogodaEmpresa;
            ViewBag.footer = empresa.EmpresaConfiguracao.FooterEmpresa;
        }
    }

    protected bool OperacaoValida()
    {
        return !_notificador.TemNotificacao();
    }
}
