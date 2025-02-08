using System.ComponentModel.DataAnnotations;

namespace LCFila.Web.Models.Identity.AccountViewModels;

public class UseRecoveryCodeViewModel
{
    [Required]
    public string Code { get; set; } = string.Empty;

    public string ReturnUrl { get; set; } = string.Empty;
}
