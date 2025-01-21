using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LCFilaApplication.Context;
using LCFilaApplication.Models;

namespace LCFila.Controllers;

public class FilaPessoaController : Controller
{
    private readonly FilaDbContext _context;

    public FilaPessoaController(FilaDbContext context)
    {
        _context = context;
    }

    // GET: FilaPessoa
    public async Task<IActionResult> Index()
    {
        return View(await _context.FilaPessoas.ToListAsync());
    }

    // GET: FilaPessoa/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var filaPessoa = await _context.FilaPessoas
            .FirstOrDefaultAsync(m => m.Id == id);
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
            _context.Add(filaPessoa);
            await _context.SaveChangesAsync();
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

        var filaPessoa = await _context.FilaPessoas.FindAsync(id);
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
                _context.Update(filaPessoa);
                await _context.SaveChangesAsync();
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

        var filaPessoa = await _context.FilaPessoas
            .FirstOrDefaultAsync(m => m.Id == id);
        if (filaPessoa == null)
        {
            return NotFound();
        }

        return View(filaPessoa);
    }

    // POST: FilaPessoa/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var filaPessoa = await _context.FilaPessoas.FindAsync(id);
        _context.FilaPessoas.Remove(filaPessoa);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool FilaPessoaExists(Guid id)
    {
        return _context.FilaPessoas.Any(e => e.Id == id);
    }
}
