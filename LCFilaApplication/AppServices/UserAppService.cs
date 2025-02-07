using LCFilaApplication.Interfaces;
using LCFilaApplication.Models;
using LCFilaInfra.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace LCFilaApplication.AppServices;

internal class UserAppService : IUserAppService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IEmpresaLoginRepository _empresaRepository;
    public UserAppService(UserManager<AppUser> userManager,
                          SignInManager<AppUser> signInManager,
                          IEmpresaLoginRepository empresaRepository)
    {
        _empresaRepository = empresaRepository;
        _userManager = userManager;
        _signInManager = signInManager;
    }
    public bool AtualizarUser(Guid id, AppUser formUser, string Role)
    {
        try
        {
            var user = _userManager.FindByIdAsync(id.ToString()).Result;
            user!.UserName = formUser.Email;
            user.Email = formUser.Email;
            user.PhoneNumber = formUser.PhoneNumber;

            var rolefromedit = Role;
            if (rolefromedit == "1")
            {
                rolefromedit = "Administrador";
            }
            else if (rolefromedit == "2")
            {
                rolefromedit = "Funcionário";
            }
            var role = _userManager.GetRolesAsync(user).Result;
            if (role.Count == 1)
            {
                if (role[0] != rolefromedit)
                {
                    if (rolefromedit == "Funcionário")
                    {
                        _userManager.RemoveFromRoleAsync(user, "EmpAdmin");
                        _userManager.AddToRoleAsync(user, "OperatorEmp");
                    }
                    else if (rolefromedit == "Administrador")
                    {
                        _userManager.RemoveFromRoleAsync(user, "OperatorEmp");
                        _userManager.AddToRoleAsync(user, "EmpAdmin");
                    }
                }
            }

            _userManager.UpdateAsync(user);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool CreateNewUser(string UserEmail, string Password, int Role)
    {
        try
        {
            var user = new AppUser { UserName = UserEmail, Email = UserEmail };

            user.EmailConfirmed = true;
            var result = _userManager.CreateAsync(user, Password).Result;
            if (result.Succeeded)
            {

                var code = _userManager.GenerateEmailConfirmationTokenAsync(user).Result;

                var resultemail = _userManager.ConfirmEmailAsync(user, code).Result;

                if (!resultemail.Succeeded)
                {
                    _userManager.DeleteAsync(user);
                    throw new Exception();
                }

                if (Role == 1)
                {
                    _userManager.AddToRoleAsync(user, "EmpAdmin");
                }
                else if (Role == 2)
                {
                    _userManager.AddToRoleAsync(user, "OperatorEmp");
                }
            }

            if (result.Succeeded)
            {
                var AllUsers = _userManager.Users;
                var admin = AllUsers.FirstOrDefault(p => p.UserName == UserEmail);
                var AllEmpresas = _empresaRepository.ObterTodos().Result;
                var empresa = AllEmpresas.FirstOrDefault(p => p.IdAdminEmpresa == Guid.Parse(admin!.Id));
                empresa!.UsersEmpresa.Add(user);
                _empresaRepository.Atualizar(empresa);
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }

    public List<AppUser> GetListUsers(string UserName)
    {
        //TODO: Fix logic, need to search the company by adminId not empresaId in line 37

        //User.Identity.Name
        var AllUsers = _userManager.Users;
        //var user = User.Identity.Name;
        //var admin = AllUsers.Include(p => p.EmpresaLogin).FirstOrDefault(p => p.UserName == User.Identity.Name);
        var admin = AllUsers.FirstOrDefault(a => a.UserName == UserName);
        var Empresa = _empresaRepository.ObterPorId(Guid.Parse(admin!.Id)).Result;
        var usersempresa = Empresa!.UsersEmpresa.Where(p => p.Id != Empresa.IdAdminEmpresa.ToString()).ToList();
        return usersempresa;
    }

    public (string, AppUser) GetUserAndRole(Guid id)
    {
        var user = _userManager.FindByIdAsync(id.ToString()).Result;
        var role = _userManager.GetRolesAsync(user!).Result;
        return (role[0],user)!;
    }

    public bool Logout()
    {
        var result = _signInManager.SignOutAsync();
        return result.IsCompletedSuccessfully;
    }

    public bool RemoverUser(Guid id, AppUser formUser)
    {
        try
        {
            var user = _userManager.FindByIdAsync(id.ToString()).Result;
            var useroles = _userManager.GetRolesAsync(user!).Result;
            foreach (var item in useroles)
            {
                _userManager.RemoveFromRoleAsync(user!, item);
            }
            _userManager.DeleteAsync(user!);
            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }
}
