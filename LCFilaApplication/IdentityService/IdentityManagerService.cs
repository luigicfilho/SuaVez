using LCFila.Application.Interfaces.Identity;
using LCFilaApplication.Models;
using LCFilaApplication.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace LCFila.Application.IdentityService;

internal class IdentityManagerService : IIdentityManagerService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IEmailSender _emailSender;
    //private readonly ISmsSender _smsSender;

    public IdentityManagerService(UserManager<AppUser> userManager,
    SignInManager<AppUser> signInManager,
    IEmailSender emailSender)
    //ISmsSender smsSender,)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _emailSender = emailSender;
        //_smsSender = smsSender;
    }

    public IdentityResult AddLoginAsync(AppUser user, UserLoginInfo info)
    {
        return _userManager.AddLoginAsync(user, info).Result;
    }

    public IdentityResult AddPasswordAsync(AppUser user, string NewPassword)
    {
        return _userManager.AddPasswordAsync(user, NewPassword).Result;
    }

    public IdentityResult ChangePasswordAsync(AppUser user, string OldPassword, string NewPassword)
    {
        return _userManager.ChangePasswordAsync(user, OldPassword, NewPassword).Result;
    }

    public IdentityResult ChangePhoneNumberAsync(AppUser user, string phoneNumber, string Code)
    {
        return _userManager.ChangePhoneNumberAsync(user, phoneNumber, Code).Result;
    }

    public AuthenticationProperties ConfigureExternalAuthenticationProperties(string? provider, [StringSyntax("Uri")] string? redirectUrl, string? userId = null)
    {
        return _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, userId);
    }

    public string GenerateChangePhoneNumberTokenAsync(AppUser user, string PhoneNumber)
    {
        return _userManager.GenerateChangePhoneNumberTokenAsync(user, PhoneNumber).Result;
    }

    public IEnumerable<string> GenerateNewTwoFactorRecoveryCodesAsync(AppUser user, int val)
    {
        return _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, val).Result;
    }

    public string GetAuthenticatorKeyAsync(AppUser user)
    {
        return _userManager.GetAuthenticatorKeyAsync(user).Result;
    }

    public IEnumerable<AuthenticationScheme> GetExternalAuthenticationSchemesAsync()
    {
        return _signInManager.GetExternalAuthenticationSchemesAsync().Result;
    }

    public ExternalLoginInfo GetExternalLoginInfoAsync(string userId)
    {
        return _signInManager.GetExternalLoginInfoAsync(userId).Result;
    }

    public IList<UserLoginInfo> GetLoginsAsync(AppUser user)
    {
        return _userManager.GetLoginsAsync(user).Result;
    }

    public string GetPhoneNumberAsync(AppUser user)
    {
        return _userManager.GetPhoneNumberAsync(user).Result;
    }

    public bool GetTwoFactorEnabledAsync(AppUser user)
    {
        return _userManager.GetTwoFactorEnabledAsync(user).Result;
    }

    public AppUser GetUserAsync(ClaimsPrincipal principal)
    {
        return _userManager.GetUserAsync(principal).Result ;
    }

    public string GetUserId(ClaimsPrincipal userClaims)
    {
        return _userManager.GetUserId(userClaims);
    }

    public string GetUserIdAsync(AppUser user)
    {
        return _userManager.GetUserIdAsync(user).Result;
    }

    public bool HasPasswordAsync(AppUser user)
    {
        return _userManager.HasPasswordAsync(user).Result;
    }

    public bool IsTwoFactorClientRememberedAsync(AppUser user)
    {
        return _signInManager.IsTwoFactorClientRememberedAsync(user).Result;
    }

    public IdentityResult RemoveLoginAsync(AppUser user, string LoginProvider, string ProviderKey)
    {
        return _userManager.RemoveLoginAsync(user, LoginProvider, ProviderKey).Result;
    }

    public bool ResetAuthenticatorKeyAsync(AppUser user)
    {
        return _userManager.ResetAuthenticatorKeyAsync(user).IsCompletedSuccessfully;
    }

    public IdentityResult SetPhoneNumberAsync(AppUser user, string? phoneNumber)
    {
        return _userManager.SetPhoneNumberAsync(user, phoneNumber).Result;
    }

    public bool SetTwoFactorEnabledAsync(AppUser user, bool val)
    {
        return _userManager.SetTwoFactorEnabledAsync(user, val).IsCompletedSuccessfully;
    }

    public bool SignInAsync(AppUser user, bool isPersistent)
    {
        return _signInManager.SignInAsync(user, isPersistent).IsCompletedSuccessfully;
    }
}
