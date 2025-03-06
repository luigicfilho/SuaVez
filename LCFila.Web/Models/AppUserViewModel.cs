//TODO: remove this reference in someway
using LCFila.Domain.Models;
using System.Text.Json.Serialization;

namespace LCFila.Web.Models;

public class AppUserViewModel
{
    public DateTimeOffset? LockoutEnd { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    //[ProtectedPersonalData]
    //[PersonalData]
    public string PhoneNumber { get; set; } = string.Empty;

    public string ConcurrencyStamp { get; set; } = string.Empty;
    public string SecurityStamp { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public bool EmailConfirmed { get; set; }

    public string NormalizedEmail { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;

    public Guid Id { get; set; }
    [JsonIgnore]
    public EmpresaLoginViewModel empresaLogin { get; set; } = new();

};

//TODO: Review This, send idenity stuff to app
public static class PessoaMapping
{
    public static AppUserViewModel ConvertToViewModel(this AppUserViewModel appUserViewModel, AppUser user)
    {
        AppUserViewModel UserViewModel = new()
        {
            UserName = user.Email!,
            Email = user.Email!,
            PhoneNumber = user.PhoneNumber!
        };
        return UserViewModel;
    }
    public static AppUser ConvertToAppUser(this AppUserViewModel appUserViewModel)
    {
        return new AppUser()
        {
            UserName = appUserViewModel.Email,
            Email = appUserViewModel.Email,
            PhoneNumber = appUserViewModel.PhoneNumber
        };
    }
}

