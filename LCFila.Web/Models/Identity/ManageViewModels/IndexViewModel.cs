using Microsoft.AspNetCore.Identity;

namespace LCFila.Web.Models.Identity.ManageViewModels;

public class IndexViewModel
{
    public bool HasPassword { get; set; }

    public IList<UserLoginInfo> Logins { get; set; } = [];

    public string PhoneNumber { get; set; } = string.Empty;

    public bool TwoFactor { get; set; }

    public bool BrowserRemembered { get; set; }

    public string AuthenticatorKey { get; set; } = string.Empty;
}
