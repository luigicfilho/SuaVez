using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using LCFila.ViewModels;
using LCFila.Mapping;
using LCFilaApplication.Interfaces;

namespace LCFila.Controllers.Sistema;

[Authorize(Roles = "SysAdmin")]
public class SysadminController : BaseController
{
    private readonly IAdminSysAppService _adminSysAppService;
    public SysadminController(INotificador notificador,
                              IAdminSysAppService adminSysAppService,
                              IConfigAppService configAppService) : base(notificador, configAppService)
    {
        _adminSysAppService = adminSysAppService;
    }
    // GET: SysadminController
    public async Task<IActionResult> Index()
    {
        ConfigEmpresa();
        var empresaLista = await _adminSysAppService.GetAllEmpresas();
        var empresaviewmodel = empresaLista.ConvertToEmpresaLoginViewModel();
        return View(empresaviewmodel);
    }

    // GET: SysadminController/Details/5
    public async Task<IActionResult> Details(Guid id)
    {
        ConfigEmpresa();
        var empresa = await _adminSysAppService.GetEmpresaDetail(id);
        var empresaviewmodel = empresa.ConvertToEmpresaLoginViewModel();
        var adminempresa = await _adminSysAppService.GetEmpresaAdmin(empresaviewmodel.IdAdminEmpresa.ToString());
        empresaviewmodel.Email = adminempresa.Email!;
        return View(empresaviewmodel);
    }

    public async Task<IActionResult> AtivarEmpresa(Guid id)
    {
        await _adminSysAppService.ActivateToggleEmpresa(id, true);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> DesativarEmpresa(Guid id)
    {
        await _adminSysAppService.ActivateToggleEmpresa(id, false);
        return RedirectToAction(nameof(Index));
        //return View();
    }

    // GET: SysadminController/Create
    public ActionResult Create()
    {
        ConfigEmpresa();
        return View();
    }

    // POST: SysadminController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(EmpresaLoginViewModel empresaViewModel)
    {
        ConfigEmpresa();
        try
        {
            if (!ModelState.IsValid) return View(empresaViewModel);

            var empresa = empresaViewModel.ConvertToEmpresaLogin();
            var returnUrl = Url.Content("~/");
            var result = _adminSysAppService.CreateEmpresa(empresa,
                                                                 empresaViewModel.Email,
                                                                 empresaViewModel.Password);
            result.Match(
                _ => Console.WriteLine("Operation was successful."),
                error => Console.WriteLine($"Operation failed with error: {error.Message}"));
            //result.Match();

            object resultados;
            if (result.IsSuccess)
            {
                var s = result.Value;
                var e = result.Error;
                var retorno = result.Match(
                            onSuccess: value => resultados = value,
                            onFailure: error => resultados = error);
            }

            //TODO: REVIEW THIS, it's the only thing that makes reference to MVC
            //var resultado =  Results.Extensions.MapResult(result);

            if (!OperacaoValida()) return View(empresaViewModel);

            return RedirectToAction(nameof(Index));
        }
        catch (Exception)
        {
            return View(empresaViewModel);
        }
    }

    // GET: SysadminController/Edit/5
    public async Task<IActionResult> Edit(Guid id)
    {
        ConfigEmpresa();
        var empresa = await _adminSysAppService.GetEmpresaDetail(id);
        var adminempresa = await _adminSysAppService.GetEmpresaAdmin(empresa.IdAdminEmpresa.ToString());


        // var users = _userManager.Users.Include(p => p.EmpresaLogin).Where(p => p.EmpresaLogin.Id == empresa.Id).ToList();
        var empresaviewmodel = empresa.ConvertToEmpresaLoginViewModel();
        empresaviewmodel.Email = adminempresa.Email!;
        var usersQuery = from d in empresa.UsersEmpresa.Where(p => p.Email != adminempresa.Email).AsEnumerable()
                         orderby d.Email // Sort by name.
                         select d;
        empresaviewmodel.ListaUsers = new SelectList(usersQuery, "Id", "Email");
        empresaviewmodel.AdminEmpresa = adminempresa;
        return View(empresaviewmodel);
    }

    // POST: SysadminController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Guid id, EmpresaLoginViewModel empresaViewModel)
    {
        ConfigEmpresa();
        try
        {
            var empresa = empresaViewModel.ConvertToEmpresaLogin();
            var result = _adminSysAppService.EditEmpresa(empresa);
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
        ConfigEmpresa();
        var empresa = await _adminSysAppService.GetEmpresaDetail(id);
        var empresaviewmodel = empresa.ConvertToEmpresaLoginViewModel();
        return View(empresaviewmodel);
    }

    // POST: SysadminController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id, EmpresaLoginViewModel empresaViewModel)
    {
        ConfigEmpresa();
        try
        {
            _ = await _adminSysAppService.RemoveEmpresa(id);

            return RedirectToAction(nameof(Index));
        }
        catch (Exception)
        {
            return View(empresaViewModel);
        }
    }
}
