using LCFila.Application.Interfaces.Identity;
using LCFila.Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using LCFila.Application.AppServices;

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


    public IdentityResult AddLoginAsync(AppUser user, ExternalLoginInfo info)
    {
        return _userManager.AddLoginAsync(user, info).Result;
    }

    public AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl)
    {
        return _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
    }

    public IdentityResult ConfirmEmailAsync(AppUser user, string code)
    {
        return _userManager.ConfirmEmailAsync(user, code).Result;
    }

    public IdentityResult CreateAsync(AppUser user)
    {
        return _userManager.CreateAsync(user).Result;
    }

    public IdentityResult CreateAsync(AppUser user, string Password)
    {
        return _userManager.CreateAsync(user, Password).Result;
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

    public string GenerateTwoFactorTokenAsync(AppUser user, string provider)
    {
        return _userManager.GenerateTwoFactorTokenAsync(user, provider).Result;
    }

    public string GetEmailAsync(AppUser user)
    {
        return _userManager.GetEmailAsync(user).Result!;
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

    public IList<string> GetValidTwoFactorProvidersAsync(AppUser user)
    {
        return _userManager.GetValidTwoFactorProvidersAsync(user).Result;
    }

    public bool IsEmailConfirmedAsync(AppUser user)
    {
        return _userManager.IsEmailConfirmedAsync(user).Result;
    }

    public SignInResult PasswordSignInAsync(string Email, string Password, bool RememberMe, bool lockoutOnFailure = false)
    {
        return _signInManager.PasswordSignInAsync(Email, Password, RememberMe, lockoutOnFailure).Result;
    }

    public IdentityResult ResetPasswordAsync(AppUser user, string Code, string Password)
    {
        return _userManager.ResetPasswordAsync(user, Code, Password).Result;
    }

    public void SignInAsync(AppUser user, bool isPersistent = false)
    {
        _signInManager.SignInAsync(user, isPersistent);
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

    public void SendCode(string provider, string code, AppUser user)
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
