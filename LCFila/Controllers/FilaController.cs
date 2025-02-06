using LCFila.Controllers.Sistema;
using LCFila.Mapping;
using LCFila.ViewModels;
using LCFilaApplication.Enums;
using LCFilaApplication.Interfaces;
using LCFilaApplication.Mapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> Index(/*Guid? id*/)
    {
        ConfigEmpresa();

        //TODO: Refactor this!!!

        var allusers = _filaAppService.GetAllUsers();
        var filasdousuario = _filaAppService.GetFilaList(User.Identity!.Name!);

        //TODO: Review this conversion, it' a reference to APP
        var teste = filasdousuario.ConvertToListFilaViewModel();
        foreach (var item in teste)
        {
            item.NomeUser = allusers.SingleOrDefault(p => p.Id == item.UserId.ToString()).UserName;
        }
        return View(teste.ToList());
    }

    // GET: FilaController/Details/5
    public async Task<IActionResult> Details(Guid id)
    {
        ConfigEmpresa();

        //TODO: Refactor this
        var (filatoopen,pessoas) = _filaAppService.GetPessoas(id, User.Identity!.Name!);
        var pessoasdafila = pessoas.ConvertToPessoaViewModelList();
        ViewBag.idFila = id;
        ViewBag.statusfila = filatoopen.Status.ToString();
        return View(pessoasdafila);
    }

    public async Task<IActionResult> ReAbrir(Guid id)
    {
        ConfigEmpresa();
        var result = _filaAppService.ReabrirFila(id);
        if(result)
            return RedirectToAction(nameof(Index));

        return BadRequest();
    }

    public async Task<IActionResult> Finalizar(Guid id)
    {
        ConfigEmpresa();

        var result = _filaAppService.FinalizarFila(id);
        if (result)
            return RedirectToAction(nameof(Index));

        return BadRequest();
    }

    [HttpGet]
    public async Task<IActionResult> CreateFila()
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
    public async Task<IActionResult> CreateFila(FilaViewModel filamodel)
    {
        ConfigEmpresa();
        filamodel.DataInicio = DateTime.Now;
        filamodel.Ativo = true;
        //Review ENUM, really need to be a reference to App?
        filamodel.Status = FilaStatus.Aberta;
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
    public async Task<IActionResult> Create(Guid id)
    {
        ConfigEmpresa();
        AdicionarpessoasViewModel pessoasviewmodel = new AdicionarpessoasViewModel();
        pessoasviewmodel.filaId = id;
        return View(pessoasviewmodel);
    }

    public async Task<IActionResult> IniciarFila()
    {
        ConfigEmpresa();
        
        var filaId = _filaAppService.IniciarFila(User.Identity!.Name!);
        return RedirectToAction("Index", filaId);

    }

    // POST: FilaController/Create
    [HttpPost]
    public async Task<IActionResult> Create(AdicionarpessoasViewModel pessoaViewModel)
    {
        ConfigEmpresa();
        if (!ModelState.IsValid) return View(pessoaViewModel);

        pessoaViewModel.pessoa.DataEntradaNaFila = DateTime.Now;
        pessoaViewModel.pessoa.Ativo = true;

        //Review ENUM, really need to be a reference to App?
        pessoaViewModel.pessoa.Status = PessoaStatus.Esperando;

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
    public async Task<IActionResult> Delete(Guid id)
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
    public async Task<IActionResult> Delete(Guid id, IFormCollection collection)
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
