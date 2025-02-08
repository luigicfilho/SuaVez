using System.ComponentModel.DataAnnotations;

namespace LCFila.Web.Models.Identity.ManageViewModels;

public class VerifyPhoneNumberViewModel
{
    [Required]
    public string Code { get; set; } = string.Empty;

    [Required]
    [Phone]
    [Display(Name = "Phone number")]
    public string PhoneNumber { get; set; } = string.Empty;
}
