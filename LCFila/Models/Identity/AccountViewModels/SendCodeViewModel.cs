using Microsoft.AspNetCore.Mvc.Rendering;

namespace LCFila.Web.Models.Identity.AccountViewModels;

public class SendCodeViewModel
{
    public string SelectedProvider { get; set; } = string.Empty;

    public ICollection<SelectListItem> Providers { get; set; } = [];

    public string ReturnUrl { get; set; } = string.Empty;

    public bool RememberMe { get; set; }
}
