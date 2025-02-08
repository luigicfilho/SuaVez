using System.ComponentModel.DataAnnotations;

namespace LCFila.Web.Models.Identity.ManageViewModels;

public class AddPhoneNumberViewModel
{
    [Required]
    [Phone]
    [Display(Name = "Phone number")]
    public string PhoneNumber { get; set; } = string.Empty;
}
