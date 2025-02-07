//TODO: remove this reference in someway
using LCFilaApplication.Models;
using System.Text.Json.Serialization;

namespace LCFila.ViewModels;

public class AppUserViewModel
{
    public DateTimeOffset? LockoutEnd { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    //[ProtectedPersonalData]
    //[PersonalData]
    public string PhoneNumber { get; set; }

    public string ConcurrencyStamp { get; set; }
    public string SecurityStamp { get; set; }

    public string PasswordHash { get; set; }

    public bool EmailConfirmed { get; set; }

    public string NormalizedEmail { get; set; }

    public string Email { get; set; }

    public string UserName { get; set; }

    public Guid Id { get; set; }
    [JsonIgnore]
    public EmpresaLoginViewModel empresaLogin { get; set; }

};

//TODO: Review This, send idenity stuff to app
public static class PessoaMapping
{
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

