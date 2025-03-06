using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LCFila.Application.Interfaces;
using LCFila.Web.Models;
using LCFila.Web.Mapping;

namespace LCFila.Controllers.Sistema;


[Authorize(Roles = "EmpAdmin,SysAdmin")]
public class EmpConfigController : BaseController
{
    private readonly IAdminSysAppService _adminSysAppService;

    const string UploadDirectory = "wwwroot/upload_arq";
    public EmpConfigController(IAdminSysAppService adminSysAppService,
                               IConfigAppService configAppService) : base(configAppService)
    {
        _adminSysAppService = adminSysAppService;       
    }

    public async Task<IActionResult> Index()
    {
        ConfigEmpresa();
        
        var userName = User.Identity!.Name;
        var empcofig = await _adminSysAppService.GetEmpresaConfiguracao(userName!);
        
        return View(empcofig.ConvertToEmpresaConfiguracaoDto());
    }


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
            var empcofig = await _adminSysAppService.UpdateEmpresaConfiguracao(id, empconfig.ConvertToEmpresaConfiguracaoDto(), empconfig.LinkLogodaEmpresa);

            return RedirectToAction(nameof(Index));
        }
        catch (Exception)
        {
            return View("Index", id);
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

        Directory.CreateDirectory(Path.GetDirectoryName(absolutefullFilePath)!);
        using (var fileStream = new FileStream(absolutefullFilePath, FileMode.CreateNew, FileAccess.Write))
        {
            files.CopyTo(fileStream);
        }

        empconfig.LinkLogodaEmpresa = Url.Content("~/" + relativefilePath1.Replace("wwwroot/", "").Replace("\\", "/"));

        return Ok(files);
    }
}
