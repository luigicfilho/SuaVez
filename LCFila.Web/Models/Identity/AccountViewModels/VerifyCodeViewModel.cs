using System.ComponentModel.DataAnnotations;

namespace LCFila.Web.Models.Identity.AccountViewModels;

public class VerifyCodeViewModel
{
    [Required]
    public string Provider { get; set; } = string.Empty;

    [Required]
    public string Code { get; set; } = string.Empty;

    public string ReturnUrl { get; set; } = string.Empty;

    [Display(Name = "Remember this browser?")]
    public bool RememberBrowser { get; set; }

    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }
}
