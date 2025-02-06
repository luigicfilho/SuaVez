using LCFila.Controllers.Sistema;
using LCFila.Mapping;
using LCFilaApplication.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LCFila.Controllers;

//[Authorize(Roles = "User")]
//[AllowAnonymous]
public class ClienteController : BaseController
{
    private readonly IPessoaAppService _pessoaAppService;

    public ClienteController(INotificador notificador,
                         IPessoaAppService pessoaAppService,
                         IConfigAppService configAppService) : base(notificador, configAppService)
    {
        _pessoaAppService = pessoaAppService;
    }
    [AllowAnonymous]
    // GET: ClienteController
    public ActionResult Index()
    {
        ConfigEmpresa();
        return View();
    }
    [AllowAnonymous]
    // GET: ClienteController/Details/5
    public async Task<IActionResult> Details(Guid id, Guid filaid)
    {
        ConfigEmpresa();
        var pegarpessoa = _pessoaAppService.GetDetails(id, filaid);
        var pessoa = pegarpessoa.ConvertToPessoaViewModel();
        pessoa.FilaId = filaid;
        return View(pessoa);
    }


    public async Task<IActionResult> Call(Guid id, Guid filaid)
    {
        ConfigEmpresa();
        var result = _pessoaAppService.Chamar(id, filaid);
        if (result)
        {
            return RedirectToAction("Details", "Fila", new { id = filaid });
        }
        return RedirectToAction("Error", new { id = filaid });
    }

    public async Task<IActionResult> Attend(Guid id, Guid filaid)
    {
        ConfigEmpresa();
        var result = _pessoaAppService.Atender(id, filaid);
        if (result)
        {
            return RedirectToAction("Details", "Fila", new { id = filaid });
        }
        return RedirectToAction("Error", new { id = filaid });
    }

    public async Task<IActionResult> Skip(Guid id, Guid filaid)
    {
        ConfigEmpresa();
        var result = _pessoaAppService.Pular(id, filaid);
        if (result)
        {
            return RedirectToAction("Details", "Fila", new { id = filaid });
        }
        return RedirectToAction("Error", new { id = filaid });
    }

    public async Task<IActionResult> Remove(Guid id, Guid filaid)
    {
        ConfigEmpresa();
        var result = _pessoaAppService.Remover(id, filaid);
        if (result)
        {
            return RedirectToAction("Details", "Fila", new { id = filaid });
        }
        return RedirectToAction("Error", new { id = filaid });
    }

    // GET: ClienteController/Create
    public ActionResult Create()
    {
        ConfigEmpresa();
        return View();
    }

    // POST: ClienteController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(IFormCollection collection)
    {
        ConfigEmpresa();
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: ClienteController/Edit/5
    public ActionResult Edit(int id)
    {
        ConfigEmpresa();
        return View();
    }

    // POST: ClienteController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, IFormCollection collection)
    {
        ConfigEmpresa();
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: ClienteController/Delete/5
    public ActionResult Delete(int id)
    {
        ConfigEmpresa();
        return View();
    }

    // POST: ClienteController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, IFormCollection collection)
    {
        ConfigEmpresa();
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}
