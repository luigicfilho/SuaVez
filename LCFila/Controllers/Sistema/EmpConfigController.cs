using LCFila.Mapping;
using LCFila.ViewModels;
using LCFilaApplication.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LCFila.Controllers.Sistema;


[Authorize(Roles = "EmpAdmin,SysAdmin")]
public class EmpConfigController : BaseController
{
    private readonly IAdminSysAppService _adminSysAppService;

    const string UploadDirectory = "wwwroot/upload_arq";
    public EmpConfigController(INotificador notificador,
                               IAdminSysAppService adminSysAppService,
                               IConfigAppService configAppService) : base(notificador, configAppService)
    {
        _adminSysAppService = adminSysAppService;       
    }
    // GET: EmpConfigController
    public async Task<IActionResult> Index()
    {
        ConfigEmpresa();
        var userName = User.Identity.Name;
        var empcofig = await _adminSysAppService.GetEmpresaConfiguracao(userName);
        EmpresaConfiguracaoViewModel emconfigviewmodel = new EmpresaConfiguracaoViewModel()
        {
            Id = empcofig.Id,
            NomeDaEmpresa = empcofig.NomeDaEmpresa,
            LinkLogodaEmpresa = empcofig.LinkLogodaEmpresa,
            CorPrincipalEmpresa = empcofig.CorPrincipalEmpresa,
            CorSegundariaEmpresa = empcofig.CorSegundariaEmpresa,
            FooterEmpresa = empcofig.FooterEmpresa
        };
        return View(emconfigviewmodel);
    }

    // GET: EmpConfigController/Details/5
    public async Task<IActionResult> Details(Guid id)
    {
        ConfigEmpresa();
        return View();
    }

    // GET: EmpConfigController/Create
    public async Task<IActionResult> Create()
    {
        ConfigEmpresa();
        return View();
    }

    // POST: EmpConfigController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(IFormCollection collection)
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

    // GET: EmpConfigController/Edit/5
    public async Task<IActionResult> Edit(Guid id)
    {
        ConfigEmpresa();
        return View();
    }

    // POST: EmpConfigController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, EmpresaConfiguracaoViewModel empconfig)
    {
        ConfigEmpresa();
        try
        {
            if (empconfig.file != null)
            {
                uploadFile(empconfig.file, empconfig);
            }
            var empcofig = await _adminSysAppService.UpdateEmpresaConfiguracao(id, empconfig.ConvertToEmpresaConfiguracao(), empconfig.LinkLogodaEmpresa);

            return RedirectToAction(nameof(Index));
        }
        catch (Exception Ex)
        {
            return View(id);
        }
    }

    // GET: EmpConfigController/Delete/5
    public async Task<IActionResult> Delete(Guid id)
    {
        ConfigEmpresa();
        return View();
    }

    // POST: EmpConfigController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id, IFormCollection collection)
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

    public IActionResult uploadFile(IFormFile files, EmpresaConfiguracaoViewModel empconfig)
    {
        var resultFileName = Path.ChangeExtension(Path.GetRandomFileName(), Path.GetExtension(files.FileName));

        var relativefilePath = Path.Combine(empconfig.Id.ToString(), resultFileName);
        var relativefilePath1 = Path.Combine(UploadDirectory, relativefilePath);

        var absolutefullFilePath = Path.Combine(Environment.CurrentDirectory, relativefilePath1);

        while (System.IO.File.Exists(absolutefullFilePath))
        {
            resultFileName = Path.ChangeExtension(Path.GetRandomFileName(), Path.GetExtension(files.FileName));
            relativefilePath = Path.Combine(UploadDirectory, resultFileName);
            absolutefullFilePath = Path.Combine(Environment.CurrentDirectory, relativefilePath);
        }

        Directory.CreateDirectory(Path.GetDirectoryName(absolutefullFilePath));
        using (var fileStream = new FileStream(absolutefullFilePath, FileMode.CreateNew, FileAccess.Write))
        {
            files.CopyTo(fileStream);
        }

        empconfig.LinkLogodaEmpresa = Url.Content("~/" + relativefilePath1.Replace("wwwroot/", "").Replace("\\", "/"));

        return Ok(files);

    }
}
