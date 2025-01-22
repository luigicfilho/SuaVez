using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace LCFilaApplication.Models;

[ModelMetadataType(typeof(AppUserMetadata))]
public partial class AppUser
{
}

public class AppUserMetadata
{
    [Display(Name = "Nome de usuário")]
    public virtual string UserName { get; set; }

    [Display(Name = "Telefone")]
    public virtual string PhoneNumber { get; set; }

    [Display(Name = "Email")]
    public virtual string Email { get; set; }
}
