using Microsoft.AspNetCore.Mvc.Rendering;

namespace LCFila.Web.Models.Identity.ManageViewModels;

public class ConfigureTwoFactorViewModel
{
    public string SelectedProvider { get; set; } = string.Empty;

    public ICollection<SelectListItem> Providers { get; set; } = [];
}
