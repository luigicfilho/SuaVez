using LCFilaApplication.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace LCFila.Application.Interfaces.Identity;

public interface IIdentityService
{
    #region UserManager
    AppUser GetUserAsync(ClaimsPrincipal claims);

    string GetEmailAsync(AppUser user);

    string GenerateTwoFactorTokenAsync(AppUser user, string provider);

    IdentityResult ResetPasswordAsync(AppUser user, string Code, string Password);

    AppUser FindByEmailAsync(string Email);

    bool IsEmailConfirmedAsync(AppUser user);

    IdentityResult ConfirmEmailAsync(AppUser user, string code);

    AppUser FindByIdAsync(string userId);

    IdentityResult CreateAsync(AppUser user);
    IdentityResult CreateAsync(AppUser user, string Password);
    IdentityResult AddLoginAsync(AppUser user, ExternalLoginInfo info);
    IList<string> GetValidTwoFactorProvidersAsync(AppUser user);

    #endregion

    #region SignInManager
    SignInResult PasswordSignInAsync(string Email, string Password, bool RememberMe, bool lockoutOnFailure = false);
    void SignInAsync(AppUser user, bool isPersistent = false);

    void SignOutAsync();
    AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl);

    ExternalLoginInfo GetExternalLoginInfoAsync();
    SignInResult ExternalLoginSignInAsync(string LoginProvider, string ProviderKey, bool isPersistent = false);
    IdentityResult UpdateExternalAuthenticationTokensAsync(ExternalLoginInfo info);
    AppUser GetTwoFactorAuthenticationUserAsync();
    SignInResult TwoFactorAuthenticatorSignInAsync(string Code, bool RememberMe, bool RememberBrowser);
    SignInResult TwoFactorSignInAsync(string Provider, string Code, bool RememberMe, bool RememberBrowser);
    SignInResult TwoFactorRecoveryCodeSignInAsync(string Code);

    #endregion

    void SendCode(string provider, string code, AppUser user);
}
