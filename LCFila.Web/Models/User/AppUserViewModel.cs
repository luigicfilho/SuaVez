using LCFila.Web.Models.Empresa;
using System.Text.Json.Serialization;

namespace LCFila.Web.Models.User;

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

