using System.ComponentModel.DataAnnotations;

namespace LCFila.Web.Models.Identity.ManageViewModels;

public class DisplayRecoveryCodesViewModel
{
    [Required]
    public IEnumerable<string> Codes { get; set; } = [];

}
