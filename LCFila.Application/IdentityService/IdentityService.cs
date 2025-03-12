using LCFila.Application.Interfaces.Identity;
using LCFila.Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using LCFila.Infra.External;
using LCFila.Application.Dto;

namespace LCFila.Application.IdentityService;

internal class IdentitysService : IIdentityService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IEmailSender _emailSender;
    //private readonly ISmsSender _smsSender;

    public IdentitysService(UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        IEmailSender emailSender
        )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _emailSender = emailSender;
    }

    public IdentityResult AddLoginAsync(AppUserDto user, ExternalLoginInfo info)
    {
        return _userManager.AddLoginAsync(user.ConvertToAppUser(), info).Result;
    }

    public AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl)
    {
        return _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
    }

    public IdentityResult ConfirmEmailAsync(AppUserDto user, string code)
    {
        return _userManager.ConfirmEmailAsync(user.ConvertToAppUser(), code).Result;
    }

    public IdentityResult CreateAsync(AppUserDto user)
    {
        return _userManager.CreateAsync(user.ConvertToAppUser()).Result;
    }

    public IdentityResult CreateAsync(AppUserDto user, string Password)
    {
        return _userManager.CreateAsync(user.ConvertToAppUser(), Password).Result;
    }

    public SignInResult ExternalLoginSignInAsync(string LoginProvider, string ProviderKey, bool isPersistent = false)
    {
        return _signInManager.ExternalLoginSignInAsync(LoginProvider, ProviderKey, isPersistent).Result;
    }

    public AppUser FindByEmailAsync(string Email)
    {
        return _userManager.FindByEmailAsync(Email).Result!;
    }

    public AppUser FindByIdAsync(string userId)
    {
        return _userManager.FindByIdAsync(userId).Result!;
    }

    public string GenerateTwoFactorTokenAsync(AppUserDto user, string provider)
    {
        return _userManager.GenerateTwoFactorTokenAsync(user.ConvertToAppUser(), provider).Result;
    }

    public string GetEmailAsync(AppUserDto user)
    {
        return _userManager.GetEmailAsync(user.ConvertToAppUser()).Result!;
    }

    public ExternalLoginInfo GetExternalLoginInfoAsync()
    {
        return _signInManager.GetExternalLoginInfoAsync().Result!;
    }

    public AppUser GetTwoFactorAuthenticationUserAsync()
    {
        return _signInManager.GetTwoFactorAuthenticationUserAsync().Result!;
    }

    public AppUser GetUserAsync(ClaimsPrincipal claims)
    {
        return _userManager.GetUserAsync(claims).Result!;
    }

    public IList<string> GetValidTwoFactorProvidersAsync(AppUserDto user)
    {
        return _userManager.GetValidTwoFactorProvidersAsync(user.ConvertToAppUser()).Result;
    }

    public bool IsEmailConfirmedAsync(AppUserDto user)
    {
        return _userManager.IsEmailConfirmedAsync(user.ConvertToAppUser()).Result;
    }

    public SignInResult PasswordSignInAsync(string Email, string Password, bool RememberMe, bool lockoutOnFailure = false)
    {
        return _signInManager.PasswordSignInAsync(Email, Password, RememberMe, lockoutOnFailure).Result;
    }

    public IdentityResult ResetPasswordAsync(AppUserDto user, string Code, string Password)
    {
        return _userManager.ResetPasswordAsync(user.ConvertToAppUser(), Code, Password).Result;
    }

    public void SignInAsync(AppUserDto user, bool isPersistent = false)
    {
        _signInManager.SignInAsync(user.ConvertToAppUser(), isPersistent);
    }

    public void SignOutAsync()
    {
        _signInManager.SignOutAsync();
    }

    public SignInResult TwoFactorAuthenticatorSignInAsync(string Code, bool RememberMe, bool RememberBrowser)
    {
        return _signInManager.TwoFactorAuthenticatorSignInAsync(Code, RememberMe, RememberBrowser).Result;
    }

    public SignInResult TwoFactorRecoveryCodeSignInAsync(string Code)
    {
        return _signInManager.TwoFactorRecoveryCodeSignInAsync(Code).Result;
    }

    public SignInResult TwoFactorSignInAsync(string Provider, string Code, bool RememberMe, bool RememberBrowser)
    {
        return _signInManager.TwoFactorSignInAsync(Provider, Code, RememberMe, RememberBrowser).Result;
    }

    public IdentityResult UpdateExternalAuthenticationTokensAsync(ExternalLoginInfo info)
    {
        return _signInManager.UpdateExternalAuthenticationTokensAsync(info).Result;
    }

    public void SendCode(string provider, string code, AppUserDto user)
    {
        var message = "Your security code is: " + code;
        if (provider == "Email")
        {
            _emailSender.SendEmailAsync(GetEmailAsync(user)!, "Security Code", message);
        }
        else if (provider == "Phone")
        {
            //await _smsSender.SendSmsAsync(await _userManager.GetPhoneNumberAsync(user), message);
        }

    }
}
