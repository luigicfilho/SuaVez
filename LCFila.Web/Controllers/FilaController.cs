using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LCFila.Controllers.Sistema;
using LCFila.Web.Mapping;
using LCFila.Application.Interfaces;
using LCFila.Web.Models;

namespace LCFila.Controllers;

[Authorize(Roles = "SysAdmin,EmpAdmin,OperatorEmp")]
public class FilaController : BaseController
{
    private readonly IFilaAppService _filaAppService;
    public FilaController(IFilaAppService filaAppService,
                          IConfigAppService configAppService) : base(configAppService)
    {
        _filaAppService = filaAppService;
    }

    public IActionResult Index()
    {
        ConfigEmpresa();

        var filasdousuario = _filaAppService.GetFilaList(User.Identity!.Name!);

        var listaFilas = FilaMapping.ConvertToListFilaViewModel(filasdousuario);

        return View(listaFilas);
    }

    public IActionResult Details(Guid id)
    {
        ConfigEmpresa();

        var filaDetails = _filaAppService.GetPessoas(id, User.Identity!.Name!);
        var filaDetailsViewModel = filaDetails.ConvertToFilaViewModelListVM();
        return View(filaDetailsViewModel);
    }

    public IActionResult ReAbrir(Guid id)
    {
        ConfigEmpresa();
        var result = _filaAppService.ReabrirFila(id);
        if(result)
            return RedirectToAction(nameof(Index));

        return BadRequest();
    }

    public IActionResult Finalizar(Guid id)
    {
        ConfigEmpresa();

        var result = _filaAppService.FinalizarFila(id);
        if (result)
            return RedirectToAction(nameof(Index));

        return BadRequest();
    }

    [HttpGet]
    public IActionResult CreateFila()
    {
        ConfigEmpresa();
        var createFilaDto = _filaAppService.GetUserIdEmpId(User.Identity!.Name!);

        var createFilaViewModel = createFilaDto.ConvertToViewModel();

        return View(createFilaViewModel);
    }

    [HttpPost]
    public IActionResult CreateFila(CreateFilaViewModel filamodel)
    {
        ConfigEmpresa();

        var result = _filaAppService.CriarFila(filamodel.ConvertToFilaVM(), 
                                               filamodel.EmpresaId, 
                                               filamodel.UserId);
        
        if (result)
            return RedirectToAction(nameof(Index));
        
        return BadRequest();
    }

    [HttpGet]
    public IActionResult Create(Guid id)
    {
        ConfigEmpresa();

        return View(new AdicionarpessoasViewModel() { filaId = id });
    }

    [HttpPost]
    public IActionResult Create(AdicionarpessoasViewModel pessoaViewModel)
    {
        ConfigEmpresa();
        if (!ModelState.IsValid) return View(pessoaViewModel);

        var result = _filaAppService.AdicionarPessoa(pessoaViewModel.pessoa.ConvertToPessoaDto(), 
                                                     pessoaViewModel.filaId);

        if (result)
        {
            return RedirectToAction("Details", new { id = pessoaViewModel.filaId });
        }
        return BadRequest();
    }

    public IActionResult IniciarFila()
    {
        ConfigEmpresa();

        var filaId = _filaAppService.IniciarFila(User.Identity!.Name!);
        return RedirectToAction("Index", filaId);

    }
    public ActionResult Edit(int id)
    {
        ConfigEmpresa();
        return View();
    }

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

    public IActionResult Delete(Guid id)
    {
        ConfigEmpresa();
        var result = _filaAppService.RemoverFila(id);
        if (result)
            return RedirectToAction(nameof(Index));
        return BadRequest();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(Guid id, IFormCollection collection)
    {
        ConfigEmpresa();
        try
        {
            var result = _filaAppService.RemoverFila(id);
            if (result)
                return RedirectToAction(nameof(Index));

            return BadRequest();
        }
        catch
        {
            return View();
        }
    }
}
