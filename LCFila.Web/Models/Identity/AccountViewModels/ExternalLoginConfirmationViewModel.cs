using System.ComponentModel.DataAnnotations;

namespace LCFila.Web.Models.Identity.AccountViewModels;

public class ExternalLoginConfirmationViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}
