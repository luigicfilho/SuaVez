using LCFila.ViewModels;
using LCFilaApplication.Interfaces;
using LCFilaApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LCFila.Controllers;

[Authorize(Roles = "SysAdmin,EmpAdmin")]
public class UsuarioController : BaseController
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IEmpresaLoginRepository _empresaRepository;

    public UsuarioController(INotificador notificador,
        SignInManager<AppUser> signInManager,
        UserManager<AppUser> userManager,
        IEmpresaLoginRepository empresaRepository) : base(notificador, userManager, empresaRepository)
    {
        _empresaRepository = empresaRepository;
        _userManager = userManager;
        _signInManager = signInManager;
    }
    // GET: UsuarioController
    public async Task<IActionResult> Index()
    {
        ConfigEmpresa();
        var AllUsers = _userManager.Users;
        var admin = AllUsers.Include(p => p.empresaLogin).FirstOrDefault(p => p.UserName == User.Identity.Name);
        var Empresa = await _empresaRepository.ObterPorId(admin.empresaLogin.Id);
        var usersempresa = Empresa.UsersEmpresa.Where(p => p.Id != Empresa.IdAdminEmpresa.ToString()).ToList(); 

        return View(usersempresa);
    }

    // GET: UsuarioController/Details/5
    public async Task<IActionResult> Details(Guid id)
    {
        ConfigEmpresa();
        var user = await _userManager.FindByIdAsync(id.ToString());
        var role = await _userManager.GetRolesAsync(user);
        ViewBag.Role = "Houve algum erro ao capturar a função do funcionário";
        if (role.Count == 1)
        {
            if(role[0] == "OperatorEmp")
            {
                ViewBag.Role = "Funcionário";
            } else if(role[0] == "EmpAdmin")
            {
                ViewBag.Role = "Administrador";
            }
        }
        return View(user);
    }

    // GET: UsuarioController/Create
    public async Task<IActionResult> Create()
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

    // POST: UsuarioController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UserCreateViewModel Input)
    {
        ConfigEmpresa();
        try
        {
            var user = new AppUser { UserName = Input.Email, Email = Input.Email };

            user.EmailConfirmed = true;
            var result = await _userManager.CreateAsync(user, Input.Password);
            if (result.Succeeded)
            {
                var returnUrl = Url.Content("~/");
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var resultemail = await _userManager.ConfirmEmailAsync(user, code);

                if (!resultemail.Succeeded)
                {
                    await _userManager.DeleteAsync(user);
                    throw new Exception();
                }

                if(Input.RoleId == 1)
                {
                    await _userManager.AddToRoleAsync(user, "EmpAdmin");
                } else if (Input.RoleId == 2)
                {
                    await _userManager.AddToRoleAsync(user, "OperatorEmp");
                }
            }

            if(result.Succeeded)
            {
                var AllUsers = _userManager.Users;
                var admin = AllUsers.FirstOrDefault(p => p.UserName == User.Identity.Name);
                var AllEmpresas = await _empresaRepository.ObterTodos();
                var empresa = AllEmpresas.FirstOrDefault(p => p.IdAdminEmpresa == Guid.Parse(admin.Id));
                empresa.UsersEmpresa.Add(user);
                await _empresaRepository.Atualizar(empresa);
            }

            return RedirectToAction(nameof(Index));
        }
        catch (Exception Ex)
        {
            return View(Input);
        }
    }

    // GET: UsuarioController/Edit/5
    public async Task<IActionResult> Edit(Guid id)
    {
        ConfigEmpresa();
        var user = await _userManager.FindByIdAsync(id.ToString());
        var role = await _userManager.GetRolesAsync(user);
        if (role.Count == 1)
        {
            if (role[0] == "OperatorEmp")
            {
                ViewBag.Role = "Funcionário";
            }
            else if (role[0] == "EmpAdmin")
            {
                ViewBag.Role = "Administrador";
            }
        }
        return View(user);
    }

    // POST: UsuarioController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, AppUserViewModel formUser, IFormCollection collection)
    {
        ConfigEmpresa();
        try
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            user.UserName = formUser.Email;
            user.Email = formUser.Email;
            user.PhoneNumber = formUser.PhoneNumber;

            var rolefromedit = collection["Funcao"][0];
            if(rolefromedit == "1")
            {
                rolefromedit = "Administrador";
            }else if (rolefromedit == "2")
            {
                rolefromedit = "Funcionário";
            }
            var role = await _userManager.GetRolesAsync(user);
            if (role.Count == 1)
            {
                if (role[0] != rolefromedit) { 
                    if (rolefromedit == "Funcionário")
                    {
                        await _userManager.RemoveFromRoleAsync(user, "EmpAdmin");
                        await _userManager.AddToRoleAsync(user, "OperatorEmp");
                    }
                    else if (rolefromedit == "Administrador")
                    {
                        await _userManager.RemoveFromRoleAsync(user, "OperatorEmp");
                        await _userManager.AddToRoleAsync(user, "EmpAdmin");
                    }
                }
            }

            await _userManager.UpdateAsync(user);
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: UsuarioController/Delete/5
    public async Task<IActionResult> Delete(Guid id)
    {
        ConfigEmpresa();
        var user = await _userManager.FindByIdAsync(id.ToString());
        var role = await _userManager.GetRolesAsync(user);
        ViewBag.Role = "Houve algum erro ao capturar a função do funcionário";
        if (role.Count == 1)
        {
            if (role[0] == "OperatorEmp")
            {
                ViewBag.Role = "Funcionário";
            }
            else if (role[0] == "EmpAdmin")
            {
                ViewBag.Role = "Administrador";
            }
        }
        return View(user);
    }

    // POST: UsuarioController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id, AppUser formUser)
    {
        ConfigEmpresa();
        try
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var useroles = await _userManager.GetRolesAsync(user);
            foreach(var item in useroles)
            {
                await _userManager.RemoveFromRoleAsync(user, item);
            }
            await _userManager.DeleteAsync(user);
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}
