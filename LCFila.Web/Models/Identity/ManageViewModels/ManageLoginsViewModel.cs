using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace LCFila.Web.Models.Identity.ManageViewModels;

public class ManageLoginsViewModel
{
    public IList<UserLoginInfo> CurrentLogins { get; set; } = [];

    public IList<AuthenticationScheme> OtherLogins { get; set; } = [];
}
