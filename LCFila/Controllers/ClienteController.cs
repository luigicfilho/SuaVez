using LCFila.Controllers.Sistema;
using LCFilaApplication.Enums;
using LCFilaApplication.Interfaces;
using LCFilaApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LCFila.Controllers;

//[Authorize(Roles = "User")]
//[AllowAnonymous]
public class ClienteController : BaseController
{
    private readonly IPessoaRepository _pessoaRepository;
    //private readonly IFilaRepository _filaRepository;
    //private readonly IFilaPessoaRepository _filapessoaRepository;
    private readonly UserManager<AppUser> _userManager;

    public ClienteController(INotificador notificador,
                         IPessoaRepository pessoaRepository,
                         IFilaPessoaRepository filapessoaRepository,
                         IEmpresaLoginRepository empresaRepository,
                         UserManager<AppUser> userManager,
                         IFilaRepository filaRepository) : base(notificador, userManager, empresaRepository)
    {
        _pessoaRepository = pessoaRepository;
        //_filaRepository = filaRepository;
        //_filapessoaRepository = filapessoaRepository;
        _userManager = userManager;
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
        var pegarpessoa = await _pessoaRepository.ObterPorId(id);
        var pessoa = new Pessoa()
        {
            Ativo = pegarpessoa.Ativo,
            Celular = pegarpessoa.Celular,
            Documento = pegarpessoa.Documento,
            Nome = pegarpessoa.Nome,
            Id = pegarpessoa.Id,
            Posicao = pegarpessoa.Posicao,
            Status = pegarpessoa.Status,
            Preferencial = pegarpessoa.Preferencial,
            Fila = pegarpessoa.Fila,
            FilaId = pegarpessoa.FilaId
        };
        pessoa.FilaId = filaid;
        return View(pessoa);
    }


    public async Task<IActionResult> Call(Guid id, Guid filaid)
    {
        ConfigEmpresa();
        var pessoa = await _pessoaRepository.ObterPorId(id);
        var pessoas = await _pessoaRepository.Buscar(p => p.FilaId == filaid && p.Ativo == true && p.Status == PessoaStatus.Esperando);

        foreach (var item in pessoas.OrderBy(p => p.Preferencial))
        {
            if (item.Id == id)
            {
                item.Posicao = 0;
                item.Status = PessoaStatus.Chamado;
            } else
            {
                item.Posicao = item.Posicao - 1;
            }
            await _pessoaRepository.Atualizar(item);
        }
        return RedirectToAction("Details", "Fila", new { id = filaid });
    }

    public async Task<IActionResult> Attend(Guid id, Guid filaid)
    {
        ConfigEmpresa();
        var pessoa = await _pessoaRepository.ObterPorId(id);
        pessoa.Ativo = false;
        pessoa.Status = PessoaStatus.Atendido;
        await _pessoaRepository.Atualizar(pessoa);
        return RedirectToAction("Details","Fila", new { id = filaid });
    }

    public async Task<IActionResult> Skip(Guid id, Guid filaid)
    {
        ConfigEmpresa();
        var pessoa = await _pessoaRepository.ObterPorId(id);
        var pessoas = await _pessoaRepository.Buscar(p => p.FilaId == filaid && p.Ativo == true && (p.Status == PessoaStatus.Esperando || p.Status == PessoaStatus.Chamado));
        var posicaopessoadel = pessoa.Posicao;
        foreach (var item in pessoas.OrderBy(p => p.Preferencial))
        {
            if (item.Id == id)
            {
                if(item.Status == PessoaStatus.Chamado)
                {
                    item.Posicao = 1;
                    item.Status = PessoaStatus.Esperando;
                } else
                {
                    item.Posicao = item.Posicao + 1;
                }
            }
            else
            {
                if (item.Posicao > posicaopessoadel)
                {
                    if(item.Posicao == 1)
                    {
                        item.Posicao = 0;
                        item.Status = PessoaStatus.Chamado;
                    }
                    else
                    {
                         item.Posicao = item.Posicao - 1;
                    }
                }
            }
            await _pessoaRepository.Atualizar(item);
        }
        return RedirectToAction("Details", "Fila", new { id = filaid });
    }

    public async Task<IActionResult> Remove(Guid id, Guid filaid)
    {
        ConfigEmpresa();
        var pessoa = await _pessoaRepository.ObterPorId(id);
        var pessoas = await _pessoaRepository.Buscar(p => p.FilaId == filaid && p.Ativo == true && (p.Status == PessoaStatus.Esperando || p.Status == PessoaStatus.Chamado));
        var posicaopessoadel = pessoa.Posicao;
        foreach (var item in pessoas.OrderBy(p => p.Preferencial))
        {
            if (item.Id == id)
            {
                item.Ativo = false;
                item.Status = PessoaStatus.Removido;
            }
            else
            {
                if(item.Posicao > posicaopessoadel)
                {
                    item.Posicao = item.Posicao - 1;
                }
            }
            await _pessoaRepository.Atualizar(item);
        }
        return RedirectToAction("Details", "Fila", new { id = filaid });
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
