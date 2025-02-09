using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using LCFila.Controllers.Sistema;
using LCFila.ViewModels;
using LCFila.Application.Interfaces;

namespace LCFila.Web.Controllers.Sistema;

[Authorize(Roles = "SysAdmin,EmpAdmin")]
public class UsuarioController : BaseController
{
    private readonly IUserAppService _userAppService;

    public UsuarioController(INotificador notificador,
                             IUserAppService userAppService,
                             IConfigAppService configAppService)
                           : base(notificador, configAppService)
    {
        _userAppService = userAppService;
    }

    public IActionResult Index()
    {
        ConfigEmpresa();
        var listUsers = _userAppService.GetListUsers(User.Identity!.Name!);
        return View(listUsers);
    }

    public IActionResult Details(Guid id)
    {
        ConfigEmpresa();
        var (role, user) = _userAppService.GetUserAndRole(id);
        ViewBag.Role = "Houve algum erro ao capturar a função do funcionário";
        if (!string.IsNullOrWhiteSpace(role))
        {
            if (role == "OperatorEmp")
            {
                ViewBag.Role = "Funcionário";
            }
            else if (role == "EmpAdmin")
            {
                ViewBag.Role = "Administrador";
            }
        }
        return View(user);
    }

    public IActionResult Create()
    {
        ConfigEmpresa();
        var userviewmodel = new UserCreateViewModel();
        userviewmodel.Roles = new List<SelectListItem>
        {
             new SelectListItem { Value = "1", Text ="Administrador"},
             new SelectListItem { Value = "2", Text = "Funcionário" },
        };

        return View(userviewmodel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(UserCreateViewModel Input)
    {
        ConfigEmpresa();
        try
        {
            var result = _userAppService.CreateNewUser(Input.Email, Input.Password, Input.RoleId);

            if (result)
            {
                var returnUrl = Url.Content("~/");

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(Input);
            }

        }
        catch (Exception)
        {
            return View(Input);
        }
    }

    public IActionResult Edit(Guid id)
    {
        ConfigEmpresa();
        var (role, user) = _userAppService.GetUserAndRole(id);
        if (!string.IsNullOrWhiteSpace(role))
        {
            if (role == "OperatorEmp")
            {
                ViewBag.Role = "Funcionário";
            }
            else if (role == "EmpAdmin")
            {
                ViewBag.Role = "Administrador";
            }
        }
        return View(user);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Guid id, AppUserViewModel formUser, IFormCollection collection)
    {
        ConfigEmpresa();
        try
        {
            var result = _userAppService.AtualizarUser(id, formUser.ConvertToAppUser(), collection["Funcao"][0]!);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest();
        }
        catch
        {
            return View();
        }
    }

    [HttpGet]
    public IActionResult Delete(Guid id)
    {
        ConfigEmpresa();
        ViewBag.Role = "Houve algum erro ao capturar a função do funcionário";
        var (role, user) = _userAppService.GetUserAndRole(id);
        if (!string.IsNullOrWhiteSpace(role))
        {
            if (role == "OperatorEmp")
            {
                ViewBag.Role = "Funcionário";
            }
            else if (role == "EmpAdmin")
            {
                ViewBag.Role = "Administrador";
            }
        }
        return View(user);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(Guid id, string? returnurl = null)
    {
        ConfigEmpresa();
        try
        {
            var result = _userAppService.RemoverUser(id);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest();
        }
        catch
        {
            return View();
        }
    }
}
