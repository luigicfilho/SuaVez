using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LCFila.Controllers.Sistema;
using LCFila.Mapping;
using LCFila.Application.Interfaces;

namespace LCFila.Web.Controllers.Public;

public class ClienteController : BaseController
{
    private readonly IPessoaAppService _pessoaAppService;

    public ClienteController(INotificador notificador,
                             IPessoaAppService pessoaAppService,
                             IConfigAppService configAppService)
                           : base(notificador, configAppService)
    {
        _pessoaAppService = pessoaAppService;
    }

    [AllowAnonymous]
    public ActionResult Index()
    {
        ConfigEmpresa();
        return View();
    }

    [AllowAnonymous]
    public IActionResult Details(Guid id, Guid filaid)
    {
        ConfigEmpresa();
        var pegarpessoa = _pessoaAppService.GetDetails(id, filaid);
        var pessoa = pegarpessoa.ConvertToPessoaViewModel();
        pessoa.FilaId = filaid;
        return View(pessoa);
    }

    public IActionResult Call(Guid id, Guid filaid)
    {
        ConfigEmpresa();
        var result = _pessoaAppService.Chamar(id, filaid);
        if (result)
        {
            return RedirectToAction("Details", "Fila", new { id = filaid });
        }
        return RedirectToAction("Error", new { id = filaid });
    }

    public ActionResult Attend(Guid id, Guid filaid)
    {
        ConfigEmpresa();
        var result = _pessoaAppService.Atender(id, filaid);
        if (result)
        {
            return RedirectToAction("Details", "Fila", new { id = filaid });
        }
        return RedirectToAction("Error", new { id = filaid });
    }

    public IActionResult Skip(Guid id, Guid filaid)
    {
        ConfigEmpresa();
        var result = _pessoaAppService.Pular(id, filaid);
        if (result)
        {
            return RedirectToAction("Details", "Fila", new { id = filaid });
        }
        return RedirectToAction("Error", new { id = filaid });
    }

    public IActionResult Remove(Guid id, Guid filaid)
    {
        ConfigEmpresa();
        var result = _pessoaAppService.Remover(id, filaid);
        if (result)
        {
            return RedirectToAction("Details", "Fila", new { id = filaid });
        }
        return RedirectToAction("Error", new { id = filaid });
    }
}
