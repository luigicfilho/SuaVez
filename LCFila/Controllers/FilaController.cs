using LCFila.Controllers.Sistema;
using LCFila.Mapping;
using LCFila.ViewModels;
using LCFilaApplication.Enums;
using LCFilaApplication.Interfaces;
using LCFilaApplication.Mapping;
using LCFilaApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LCFila.Controllers;

[Authorize(Roles = "SysAdmin,EmpAdmin,OperatorEmp")]
public class FilaController : BaseController
{
    private readonly IPessoaRepository _pessoaRepository;
    private readonly IFilaRepository _filaRepository;
    private readonly IFilaPessoaRepository _filapessoaRepository;
    private readonly IEmpresaLoginRepository _empresaRepository;
    private readonly UserManager<AppUser> _userManager;
    public FilaController(INotificador notificador,
                          UserManager<AppUser> userManager,
                          IPessoaRepository pessoaRepository,
                          IFilaPessoaRepository filapessoaRepository,
                          IFilaRepository filaRepository,
                          IEmpresaLoginRepository empresaRepository) : base(notificador, userManager, empresaRepository)
    {
        _pessoaRepository = pessoaRepository;
        _filaRepository = filaRepository;
        _filapessoaRepository = filapessoaRepository;
        _userManager = userManager;
        _empresaRepository = empresaRepository;
    }

    // GET: FilaController
    public async Task<IActionResult> Index(/*Guid? id*/)
    {
        ConfigEmpresa();
        var user = await _userManager.Users.SingleOrDefaultAsync(p => p.UserName == User.Identity.Name);
        var allusers = _userManager.Users.ToList();
        var empresalogin = _empresaRepository.Buscar(s => s.IdAdminEmpresa == Guid.Parse(user.Id));
        var Empresaid = empresalogin.Id;
        var pegarfila = await _filaRepository.ObterTodos();
        List<Fila> filasdousuario = new List<Fila>();
        //if (User.IsInRole("EmpAdmin"))
        //{
        //    filasdousuario = pegarfila.Where(p => p.EmpresaId == Empresaid).ToList();
        //} else
        //{
            filasdousuario = pegarfila.Where(p => p.UserId == Guid.Parse(user.Id)).ToList();
        //}
        
        var pessoas = await _pessoaRepository.ObterTodos();

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
        var user = await _userManager.Users.SingleOrDefaultAsync(p => p.UserName == User.Identity.Name);
        var empresalogin = _empresaRepository.Buscar(s => s.IdAdminEmpresa == Guid.Parse(user.Id));
        var Empresaid = empresalogin.Id;
        var filatoopen = await _filaRepository.ObterPorId(id);
        var pessoas = await _pessoaRepository.Buscar(p => p.FilaId == id);
        var pessoasdafila = pessoas.ConvertToPessoaViewModelList();
        ViewBag.idFila = id;
        ViewBag.statusfila = filatoopen.Status.ToString();
        return View(pessoasdafila);
    }

    public async Task<IActionResult> ReAbrir(Guid id)
    {
        ConfigEmpresa();
        var filatoopen = await _filaRepository.ObterPorId(id);

        filatoopen.Status = FilaStatus.Aberta;
        await _filaRepository.Atualizar(filatoopen);
        await _filaRepository.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Finalizar(Guid id)
    {
        ConfigEmpresa();
        var filatoopen = await _filaRepository.ObterPorId(id);

        filatoopen.Status = FilaStatus.Finalizada;
        await _filaRepository.Atualizar(filatoopen);
        await _filaRepository.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> CreateFila()
    {
        ConfigEmpresa();
        var user = await _userManager.Users.SingleOrDefaultAsync(p => p.UserName == User.Identity.Name);
        var empresalogin = _empresaRepository.Buscar(s => s.IdAdminEmpresa == Guid.Parse(user.Id)).Result.FirstOrDefault();
        var Empresaid = empresalogin.Id;

        FilaViewModel filamodel = new FilaViewModel
        {
            UserId = Guid.Parse(user.Id),
            EmpresaId = Empresaid,
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
        filamodel.Status = FilaStatus.Aberta;
        var fila = filamodel.ConvertToFila();
        await _filaRepository.Adicionar(fila);
        return RedirectToAction("Index");
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
        var user = await _userManager.Users.SingleOrDefaultAsync(p => p.UserName == User.Identity.Name);
        var empresalogin = _empresaRepository.Buscar(s => s.IdAdminEmpresa == Guid.Parse(user.Id)).Result.FirstOrDefault();
        var Empresaid = empresalogin.Id;
        FilaViewModel novafila = new FilaViewModel();
        novafila.DataInicio = DateTime.Now;
        novafila.TempoMedio = "30";
        novafila.EmpresaId = Empresaid;
        novafila.UserId = Guid.Parse(user.Id);
        var fila = novafila.ConvertToFila(); 
        await _filaRepository.Adicionar(fila);
        return RedirectToAction("Index", fila.Id);

    }

    // POST: FilaController/Create
    [HttpPost]
    public async Task<IActionResult> Create(AdicionarpessoasViewModel pessoaViewModel)
    {
        ConfigEmpresa();
        if (!ModelState.IsValid) return View(pessoaViewModel);

        pessoaViewModel.pessoa.DataEntradaNaFila = DateTime.Now;
        pessoaViewModel.pessoa.Ativo = true;
        pessoaViewModel.pessoa.Status = PessoaStatus.Esperando;
        var pessoas = await _pessoaRepository.ObterTodos();
        var pessoasdafila = pessoas.Where(p => p.FilaId == pessoaViewModel.filaId && p.Ativo == true && p.Status == PessoaStatus.Esperando).ToList();

        var pessoa = pessoaViewModel.pessoa.ConvertToPessoa();

        if (pessoaViewModel.pessoa.Preferencial)
        {
            pessoa.Posicao = 1;
            foreach (var item in pessoas.Where(p => p.FilaId == pessoaViewModel.filaId && p.Ativo == true && p.Status == PessoaStatus.Esperando).OrderBy(p => p.Preferencial))
            {
                if (item.Ativo) { 
                    if (item.Preferencial)
                    {
                        pessoa.Posicao = item.Posicao + 1;
                    } else
                    {
                        item.Posicao = item.Posicao + 1;
                    }
                    await _pessoaRepository.Atualizar(item);
                }
            }
            
        }
        else
        {
            pessoa.Posicao = pessoasdafila.Count + 1;
        }
        pessoa.FilaId = pessoaViewModel.filaId;

        await _pessoaRepository.Adicionar(pessoa);


        if (!OperacaoValida()) return View(pessoaViewModel);

        return RedirectToAction("Details", new { id = pessoaViewModel.filaId });
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
        var filatoopen = await _filaRepository.ObterPorId(id);

        filatoopen.Ativo = false;
        await _filaRepository.Atualizar(filatoopen);
        await _filaRepository.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    // POST: FilaController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id, IFormCollection collection)
    {
        ConfigEmpresa();
        try
        {
            var filatoopen = await _filaRepository.ObterPorId(id);

            filatoopen.Ativo = false;
            await _filaRepository.Atualizar(filatoopen);
            await _filaRepository.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}
