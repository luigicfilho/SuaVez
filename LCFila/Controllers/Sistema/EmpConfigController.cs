using LCFila.ViewModels;
using LCFilaApplication.Interfaces;
using LCFilaApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LCFila.Controllers.Sistema;


[Authorize(Roles = "EmpAdmin,SysAdmin")]
public class EmpConfigController : BaseController
{
    private readonly IEmpresaLoginRepository _empresaRepository;
    private readonly IEmpresaConfiguracaoRepository _empresaConfigRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    const string UploadDirectory = "wwwroot/upload_arq";
    public EmpConfigController(INotificador notificador,
                              UserManager<AppUser> userManager,
                              RoleManager<IdentityRole> roleManager,
                              IEmpresaLoginRepository empresaRepository,
                              IEmpresaConfiguracaoRepository empresaConfigRepository) : base(notificador, userManager, empresaRepository)
    {
        _empresaRepository = empresaRepository;
        _userManager = userManager;
        _roleManager = roleManager;
        _empresaConfigRepository = empresaConfigRepository;
    }
    // GET: EmpConfigController
    public async Task<IActionResult> Index()
    {
        ConfigEmpresa();
        IQueryable<AppUser> AllUsers = _userManager.Users;
        AppUser admin = AllUsers.FirstOrDefault(p => p.UserName == User.Identity.Name);
        List<EmpresaLogin> AllEmpresas = await _empresaRepository.ObterTodos();
        EmpresaLogin empresa = AllEmpresas.FirstOrDefault(p => p.IdAdminEmpresa == Guid.Parse(admin.Id));
        IEnumerable<EmpresaConfiguracao> config = await _empresaConfigRepository.Buscar(p => p.NomeDaEmpresa == empresa.NomeEmpresa);
        EmpresaConfiguracao empcofig = config.FirstOrDefault();
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
            List<EmpresaLogin> AllEmpresas = await _empresaRepository.ObterTodos();
            EmpresaLogin empresa = AllEmpresas.FirstOrDefault(p => p.EmpresaConfiguracao.Id == id);
            empresa.NomeEmpresa = empconfig.NomeDaEmpresa;
            EmpresaConfiguracao empconfigs = new EmpresaConfiguracao()
            {
                Id = empconfig.Id,
                NomeDaEmpresa = empconfig.NomeDaEmpresa,
                LinkLogodaEmpresa = empconfig.LinkLogodaEmpresa,
                CorPrincipalEmpresa = empconfig.CorPrincipalEmpresa,
                CorSegundariaEmpresa = empconfig.CorSegundariaEmpresa,
                FooterEmpresa = empconfig.FooterEmpresa
            };
            if (empconfig.file == null)
            {
                empconfigs.LinkLogodaEmpresa = empresa.EmpresaConfiguracao.LinkLogodaEmpresa;
            }
            empresa.EmpresaConfiguracao = empconfigs;
            await _empresaRepository.Atualizar(empresa);
            await _empresaRepository.SaveChanges();

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
