using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//TODO: This should not be, change to viewmodel, or remove because this file it's not used
using LCFilaApplication.Models;
using LCFilaApplication.Interfaces;

namespace LCFila.Controllers;

public class FilaPessoaController : Controller
{
    private readonly IFilaAppService _filaAppService;

    public FilaPessoaController(IFilaAppService filaAppService)
    {
        _filaAppService = filaAppService;
    }

    // GET: FilaPessoa
    public async Task<IActionResult> Index()
    {
        return View(_filaAppService.GetFilaList(User.Identity!.Name!));
    }

    // GET: FilaPessoa/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var (_ ,filaPessoa) = _filaAppService.GetPessoas(id.Value, User.Identity!.Name!);
        if (filaPessoa == null)
        {
            return NotFound();
        }

        return View(filaPessoa);
    }

    // GET: FilaPessoa/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: FilaPessoa/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id")] FilaPessoa filaPessoa)
    {
        if (ModelState.IsValid)
        {
            filaPessoa.Id = Guid.NewGuid();
            Pessoa pessoa = new()
            {
                Id = Guid.NewGuid()
            };
            _filaAppService.AdicionarPessoa(pessoa, filaPessoa.Id);
            return RedirectToAction(nameof(Index));
        }
        return View(filaPessoa);
    }

    // GET: FilaPessoa/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var filaPessoa = _filaAppService.GetFilaList(User.Identity!.Name!);
        if (filaPessoa == null)
        {
            return NotFound();
        }
        return View(filaPessoa);
    }

    // POST: FilaPessoa/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Id")] FilaPessoa filaPessoa)
    {
        if (id != filaPessoa.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                Fila fila = filaPessoa.FiladePessoas;
                _filaAppService.CriarFila(fila);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilaPessoaExists(filaPessoa.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(filaPessoa);
    }

    // GET: FilaPessoa/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var result = _filaAppService.RemoverFila(id.Value);

        if (!result)
        {
            return NotFound();
        }

        return View();
    }

    // POST: FilaPessoa/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var result = _filaAppService.RemoverFila(id);
        return RedirectToAction(nameof(Index));
    }

    private bool FilaPessoaExists(Guid id)
    {
        return _filaAppService.GetFilaList(User.Identity!.Name!).Any(e => e.Id == id);
    }
}
