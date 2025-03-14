using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using LCFila.Application.Interfaces;
using LCFila.Web.Mapping;
using LCFila.Web.Models.Empresa;

namespace LCFila.Web.Controllers.Sistema;

[Authorize(Roles = "SysAdmin")]
public class SysadminController : BaseController
{
    private readonly IAdminSysAppService _adminSysAppService;
    public SysadminController(IAdminSysAppService adminSysAppService,
                              IConfigAppService configAppService) : base(configAppService)
    {
        _adminSysAppService = adminSysAppService;
    }

    public async Task<IActionResult> Index()
    {
        ConfigEmpresa();
        var empresaLista = await _adminSysAppService.GetAllEmpresas();
        var empresaviewmodel = empresaLista.ConvertToEmpresaLoginViewModel();
        return View(empresaviewmodel);
    }

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
    }

    public ActionResult Create()
    {
        ConfigEmpresa();
        return View();
    }

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

            return RedirectToAction(nameof(Index));
        }
        catch (Exception)
        {
            return View(empresaViewModel);
        }
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        ConfigEmpresa();
        var empresa = await _adminSysAppService.GetEmpresaDetail(id);
        var adminempresa = await _adminSysAppService.GetEmpresaAdmin(empresa.IdAdminEmpresa.ToString());

        var empresaviewmodel = empresa.ConvertToEmpresaLoginViewModel();
        empresaviewmodel.Email = adminempresa.Email!;
        var usersQuery = from d in empresa.UsersEmpresa!.Where(p => p.Email != adminempresa.Email).AsEnumerable()
                         orderby d.Email
                         select d;
        empresaviewmodel.ListaUsers = new SelectList(usersQuery, "Id", "Email");
        empresaviewmodel.AdminEmpresa = adminempresa;
        return View(empresaviewmodel);
    }

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

    public async Task<IActionResult> Delete(Guid id)
    {
        ConfigEmpresa();
        var empresa = await _adminSysAppService.GetEmpresaDetail(id);
        var empresaviewmodel = empresa.ConvertToEmpresaLoginViewModel();
        return View(empresaviewmodel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id, EmpresaLoginViewModel empresaViewModel)
    {
        ConfigEmpresa();
        try
        {
            await _adminSysAppService.RemoveEmpresa(id);

            return RedirectToAction(nameof(Index));
        }
        catch (Exception)
        {
            return View(empresaViewModel);
        }
    }
}
