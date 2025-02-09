using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LCFila.Controllers.Sistema;
using LCFila.Mapping;
using LCFila.ViewModels;
using LCFila.Web.Mapping;
using LCFila.Web.Models;
using LCFila.Application.Interfaces;

namespace LCFila.Controllers;

[Authorize(Roles = "SysAdmin,EmpAdmin,OperatorEmp")]
public class FilaController : BaseController
{
    private readonly IFilaAppService _filaAppService;
    public FilaController(INotificador notificador,
                          IFilaAppService filaAppService,
                          IConfigAppService configAppService) : base(notificador, configAppService)
    {
        _filaAppService = filaAppService;
    }

    // GET: FilaController
    public IActionResult Index(/*Guid? id*/)
    {
        ConfigEmpresa();

        //TODO: Refactor this!!!

        var allusers = _filaAppService.GetAllUsers();
        var filasdousuario = _filaAppService.GetFilaList(User.Identity!.Name!);

        //TODO: Review this conversion, it' a reference to APP
        var teste = FilaMapping.ConvertToListFilaViewModel(filasdousuario);
        foreach (var item in teste)
        {
            item.NomeUser = allusers.SingleOrDefault(p => p.Id == item.UserId.ToString())!.UserName!;
        }
        return View(teste.ToList());
    }

    // GET: FilaController/Details/5
    public IActionResult Details(Guid id)
    {
        ConfigEmpresa();

        //TODO: Refactor this
        var (filatoopen,pessoas) = _filaAppService.GetPessoas(id, User.Identity!.Name!);
        var pessoasdafila = pessoas.ConvertToPessoaViewModelList();
        ViewBag.idFila = id;
        ViewBag.statusfila = filatoopen.Status.ToString();
        return View(pessoasdafila);
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
        var (userId, empresaid) = _filaAppService.GetUserIdEmpId(User.Identity!.Name!);

        FilaViewModel filamodel = new FilaViewModel
        {
            UserId = userId,
            EmpresaId = empresaid,
            TempoMedio = "30"
        };
        return View(filamodel);
    }

    [HttpPost]
    public IActionResult CreateFila(FilaViewModel filamodel)
    {
        ConfigEmpresa();
        filamodel.DataInicio = DateTime.Now;
        filamodel.Ativo = true;
        //Review ENUM, really need to be a reference to App?
        filamodel.Status = FilaStatusViewModel.Aberta;
        //TODO: Review this conversion, it' a reference to APP
        var fila = filamodel.ConvertToFila();
        var result = _filaAppService.CriarFila(fila);
        if (result)
            return RedirectToAction(nameof(Index));
        return BadRequest();
    }
    // GET: FilaController/Create
    //[ClaimsAuthorize("Funcionário", "Criar")]
    [HttpGet]
    public IActionResult Create(Guid id)
    {
        ConfigEmpresa();
        AdicionarpessoasViewModel pessoasviewmodel = new AdicionarpessoasViewModel();
        pessoasviewmodel.filaId = id;
        return View(pessoasviewmodel);
    }

    public IActionResult IniciarFila()
    {
        ConfigEmpresa();
        
        var filaId = _filaAppService.IniciarFila(User.Identity!.Name!);
        return RedirectToAction("Index", filaId);

    }

    // POST: FilaController/Create
    [HttpPost]
    public IActionResult Create(AdicionarpessoasViewModel pessoaViewModel)
    {
        ConfigEmpresa();
        if (!ModelState.IsValid) return View(pessoaViewModel);

        pessoaViewModel.pessoa.DataEntradaNaFila = DateTime.Now;
        pessoaViewModel.pessoa.Ativo = true;
        pessoaViewModel.pessoa.FilaId = pessoaViewModel.filaId;
        //Review ENUM, really need to be a reference to App?
        pessoaViewModel.pessoa.Status = PessoaStatusViewModel.Esperando;

        var result = _filaAppService.AdicionarPessoa(pessoaViewModel.pessoa.ConvertToPessoa(), 
                                                     pessoaViewModel.filaId);

        if (result)
        {
            if (!OperacaoValida()) return View(pessoaViewModel);

            return RedirectToAction("Details", new { id = pessoaViewModel.filaId });
        }
        return BadRequest();
    }

    // GET: FilaController/Edit/5
    public ActionResult Edit(int id)
    {
        ConfigEmpresa();
        return View();
    }

    // POST: FilaController/Edit/5
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

    // GET: FilaController/Delete/5
    public IActionResult Delete(Guid id)
    {
        ConfigEmpresa();
        var result = _filaAppService.RemoverFila(id);
        if (result)
            return RedirectToAction(nameof(Index));
        return BadRequest();
    }

    // POST: FilaController/Delete/5
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
            //return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}
