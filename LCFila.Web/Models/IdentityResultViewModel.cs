using Microsoft.AspNetCore.Identity;

namespace LCFila.Web.Models;

public class IdentityResultViewModel
{
    public IdentityResultViewModel(string code, string description)
    {
        Code = code;
        Description = description;
    }
    public string Code { get; set; } = default!;

    public string Description { get; set; } = default!;
}

public static class IdentityMapping
{

    public static List<IdentityResultViewModel> ConvertToIdentityResult(this IdentityResult identityResult)
    {
        List<IdentityResultViewModel> listerror = [];
        foreach (var error in identityResult.Errors)
        {
            listerror.Add(new IdentityResultViewModel(error.Code, error.Description));
        }
        return listerror;
    }
} 
