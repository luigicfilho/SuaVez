using LCFila.Application.Interfaces.Identity;
using LCFila.Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using LCFila.Infra.External;
using LCFila.Application.Dto;
using LCFila.Application.Mappers;

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

    public IdentityResult AddLoginAsync(AppUserDto user, UserLoginInfo info)
    {
        return _userManager.AddLoginAsync(user.ConvertToAppUser(), info).Result;
    }

    public IdentityResult AddPasswordAsync(AppUserDto user, string NewPassword)
    {
        return _userManager.AddPasswordAsync(user.ConvertToAppUser(), NewPassword).Result;
    }

    public IdentityResult ChangePasswordAsync(AppUserDto user, string OldPassword, string NewPassword)
    {
        return _userManager.ChangePasswordAsync(user.ConvertToAppUser(), OldPassword, NewPassword).Result;
    }

    public IdentityResult ChangePhoneNumberAsync(AppUserDto user, string phoneNumber, string Code)
    {
        return _userManager.ChangePhoneNumberAsync(user.ConvertToAppUser(), phoneNumber, Code).Result;
    }

    public AuthenticationProperties ConfigureExternalAuthenticationProperties(string? provider, [StringSyntax("Uri")] string? redirectUrl, string? userId = null)
    {
        return _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, userId);
    }

    public string GenerateChangePhoneNumberTokenAsync(AppUserDto user, string PhoneNumber)
    {
        return _userManager.GenerateChangePhoneNumberTokenAsync(user.ConvertToAppUser(), PhoneNumber).Result;
    }

    public IEnumerable<string> GenerateNewTwoFactorRecoveryCodesAsync(AppUserDto user, int val)
    {
        return _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user.ConvertToAppUser(), val).Result!;
    }

    public string GetAuthenticatorKeyAsync(AppUserDto user)
    {
        return _userManager.GetAuthenticatorKeyAsync(user.ConvertToAppUser()).Result!;
    }

    public IEnumerable<AuthenticationScheme> GetExternalAuthenticationSchemesAsync()
    {
        return _signInManager.GetExternalAuthenticationSchemesAsync().Result;
    }

    public ExternalLoginInfo GetExternalLoginInfoAsync(string userId)
    {
        return _signInManager.GetExternalLoginInfoAsync(userId).Result!;
    }

    public IList<UserLoginInfo> GetLoginsAsync(AppUserDto user)
    {
        return _userManager.GetLoginsAsync(user.ConvertToAppUser()).Result;
    }

    public string GetPhoneNumberAsync(AppUserDto user)
    {
        return _userManager.GetPhoneNumberAsync(user.ConvertToAppUser()).Result!;
    }

    public bool GetTwoFactorEnabledAsync(AppUserDto user)
    {
        return _userManager.GetTwoFactorEnabledAsync(user.ConvertToAppUser()).Result;
    }

    public AppUserDto GetUserAsync(ClaimsPrincipal principal)
    {
        var user = _userManager.GetUserAsync(principal).Result!;
        return user.ConvertToAppUserDto();
    }

    public string GetUserId(ClaimsPrincipal userClaims)
    {
        return _userManager.GetUserId(userClaims)!;
    }

    public string GetUserIdAsync(AppUserDto user)
    {
        return _userManager.GetUserIdAsync(user.ConvertToAppUser()).Result;
    }

    public bool HasPasswordAsync(AppUserDto user)
    {
        return _userManager.HasPasswordAsync(user.ConvertToAppUser()).Result;
    }

    public bool IsTwoFactorClientRememberedAsync(AppUserDto user)
    {
        return _signInManager.IsTwoFactorClientRememberedAsync(user.ConvertToAppUser()).Result;
    }

    public IdentityResult RemoveLoginAsync(AppUserDto user, string LoginProvider, string ProviderKey)
    {
        return _userManager.RemoveLoginAsync(user.ConvertToAppUser(), LoginProvider, ProviderKey).Result;
    }

    public bool ResetAuthenticatorKeyAsync(AppUserDto user)
    {
        return _userManager.ResetAuthenticatorKeyAsync(user.ConvertToAppUser()).IsCompletedSuccessfully;
    }

    public IdentityResult SetPhoneNumberAsync(AppUserDto user, string? phoneNumber)
    {
        return _userManager.SetPhoneNumberAsync(user.ConvertToAppUser(), phoneNumber).Result;
    }

    public bool SetTwoFactorEnabledAsync(AppUserDto user, bool val)
    {
        return _userManager.SetTwoFactorEnabledAsync(user.ConvertToAppUser(), val).IsCompletedSuccessfully;
    }

    public bool SignInAsync(AppUserDto user, bool isPersistent)
    {
        return _signInManager.SignInAsync(user.ConvertToAppUser(), isPersistent).IsCompletedSuccessfully;
    }
}
