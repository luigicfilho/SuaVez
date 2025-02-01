using LCFilaApplication.Interfaces;
using LCFilaApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LCFila.Controllers.Sistema;

public abstract class BaseController : Controller
{
    private readonly INotificador _notificador;
    private readonly UserManager<AppUser> _userManager;
    private readonly IEmpresaLoginRepository _empresaRepository;
    protected BaseController(INotificador notificador,
                             UserManager<AppUser> userManager,
                             IEmpresaLoginRepository empresaRepository)
    {
        _notificador = notificador;
        _empresaRepository = empresaRepository;
        _userManager = userManager;
    }

    protected void ConfigEmpresa()
    {
        var userlog = User.Identity!.Name;
        var user = _userManager.Users.SingleOrDefault(p => p.UserName == User.Identity.Name);
        if (user != null)
        {
            //var Empresaid = user.EmpresaLogin.Id;
            var empresa = _empresaRepository.ObterTodos().Result.SingleOrDefault(p => p.IdAdminEmpresa == Guid.Parse(user.Id));
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
