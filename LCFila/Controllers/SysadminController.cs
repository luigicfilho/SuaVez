using LCFila.ViewModels;
using LCFilaApplication.Interfaces;
using LCFilaApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LCFila.Mapping;
using LCFilaApplication.Services;

namespace LCFila.Controllers;

[Authorize(Roles = "SysAdmin")]
public class SysadminController : BaseController
{
    private readonly IEmpresaLoginRepository _empresaRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ILogger<SysadminController> _logger;
    private readonly IEmailSender _emailSender;
    public SysadminController(INotificador notificador,
                              UserManager<AppUser> userManager,
                              RoleManager<IdentityRole> roleManager,
                              ILogger<SysadminController> logger,
                              IEmailSender emailSender,
                              IEmpresaLoginRepository empresaRepository) : base(notificador, userManager, empresaRepository)
    {
        _empresaRepository = empresaRepository;
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
        _emailSender = emailSender;
    }
    // GET: SysadminController
    public async Task<IActionResult> Index()
    {
        var empresaLista = await _empresaRepository.ObterTodos();
        var empresaviewmodel = empresaLista.ConvertToEmpresaLoginViewModel();
        return View(empresaviewmodel);
    }

    // GET: SysadminController/Details/5
    public async Task<IActionResult> Details(Guid id)
    {
        var empresa = await _empresaRepository.ObterPorId(id);
        var adminempresa = await _userManager.FindByIdAsync(empresa.IdAdminEmpresa.ToString());
        var empresaviewmodel = empresa.ConvertToEmpresaLoginViewModel();
        empresaviewmodel.Email = adminempresa.Email;
        return View(empresaviewmodel);
    }

    public async Task<IActionResult> AtivarEmpresa(Guid id)
    {
        var empresa = await _empresaRepository.ObterPorId(id);
        empresa.Ativo = true;
        await _empresaRepository.Atualizar(empresa);
        await _empresaRepository.SaveChanges();
        var empresaviewmodel = empresa.ConvertToEmpresaLoginViewModel();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> DesativarEmpresa(Guid id)
    {
        var empresa = await _empresaRepository.ObterPorId(id);
        empresa.Ativo = false;
        await _empresaRepository.Atualizar(empresa);
        await _empresaRepository.SaveChanges();
        var empresaviewmodel = empresa.ConvertToEmpresaLoginViewModel();
        return RedirectToAction(nameof(Index));
        //return View();
    }

    // GET: SysadminController/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: SysadminController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EmpresaLoginViewModel empresaViewModel)
    {
        try
        {
            if (!ModelState.IsValid) return View(empresaViewModel);

            var user = new AppUser { UserName = empresaViewModel.Email, Email = empresaViewModel.Email };
            user.EmailConfirmed = true;
            var result = await _userManager.CreateAsync(user, empresaViewModel.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation($"User {empresaViewModel.NomeEmpresa} created a new account with password.");
                var returnUrl = Url.Content("~/");
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var resultemail = await _userManager.ConfirmEmailAsync(user, code);

                if (!resultemail.Succeeded)
                {
                    await _userManager.DeleteAsync(user);
                    throw new Exception();
                }

                var roles = await _userManager.AddToRoleAsync(user, "EmpAdmin");

                List<AppUser> listausers = new List<AppUser>();
                listausers.Add(user);
                List<FilaViewModel> EmpresaFilas = new List<FilaViewModel>();
                empresaViewModel.IdAdminEmpresa = Guid.Parse(user.Id);
                empresaViewModel.UsersEmpresa = listausers;
                empresaViewModel.EmpresaFilas = EmpresaFilas;
                EmpresaConfiguracaoViewModel empconfig = new EmpresaConfiguracaoViewModel()
                {
                    NomeDaEmpresa = empresaViewModel.NomeEmpresa,
                    LinkLogodaEmpresa = "http://",
                    CorPrincipalEmpresa = "black",
                    CorSegundariaEmpresa = "black",
                    FooterEmpresa = "no footer"
                };
                empresaViewModel.EmpresaConfiguracao = empconfig;
                var empresa = empresaViewModel.ConvertToEmpresaLogin();
                await _empresaRepository.Adicionar(empresa);

            }

            if (!OperacaoValida()) return View(empresaViewModel);

            return RedirectToAction(nameof(Index));
        }
        catch (Exception Ex)
        {
            return View(empresaViewModel);
        }
    }

    // GET: SysadminController/Edit/5
    public async Task<IActionResult> Edit(Guid id)
    {
        var empresa = await _empresaRepository.ObterPorId(id);
        var adminempresa = await _userManager.FindByIdAsync(empresa.IdAdminEmpresa.ToString());
        var users = _userManager.Users.Include(p => p.empresaLogin).Where(p => p.empresaLogin.Id == empresa.Id).ToList();
        var empresaviewmodel = empresa.ConvertToEmpresaLoginViewModel();
        empresaviewmodel.Email = adminempresa.Email;
        var usersQuery = from d in users.Where(p => p.Email != adminempresa.Email).AsEnumerable()
                               orderby d.Email // Sort by name.
                               select d;
        empresaviewmodel.ListaUsers = new SelectList(usersQuery, "Id", "Email"); 
        empresaviewmodel.AdminEmpresa = adminempresa;
        return View(empresaviewmodel);
    }

    // POST: SysadminController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, EmpresaLoginViewModel empresaViewModel)
    {
        try
        {
            
            var empresa = empresaViewModel.ConvertToEmpresaLogin();
            await _empresaRepository.Atualizar(empresa);
            await _empresaRepository.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View(empresaViewModel);
        }
    }

    // GET: SysadminController/Delete/5
    public async Task<IActionResult> Delete(Guid id)
    {
        var empresa = await _empresaRepository.ObterPorId(id);
        var empresaviewmodel = empresa.ConvertToEmpresaLoginViewModel();
        return View(empresaviewmodel);
    }

    // POST: SysadminController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id, EmpresaLoginViewModel empresaViewModel)
    {
        try
        {
            var empresa1 = empresaViewModel.ConvertToEmpresaLogin();
            var empresa = await _empresaRepository.ObterPorId(empresaViewModel.Id);
            
            var adminempresa = await _userManager.FindByIdAsync(empresa.IdAdminEmpresa.ToString());
            await _userManager.RemoveFromRoleAsync(adminempresa, "EmpAdmin");
            await _userManager.DeleteAsync(adminempresa);
            await _empresaRepository.Remover(empresa.Id);
            await _empresaRepository.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        catch (Exception Ex)
        {
            return View(empresaViewModel);
        }
    }
}
