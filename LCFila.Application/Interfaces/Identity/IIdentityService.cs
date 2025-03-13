using LCFila.Application.Dto;
using LCFila.Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace LCFila.Application.Interfaces.Identity;

public interface IIdentityService
{
    #region UserManager
    AppUser GetUserAsync(ClaimsPrincipal claims);

    string GetEmailAsync(AppUserDto user);

    string GenerateTwoFactorTokenAsync(AppUserDto user, string provider);

    IdentityResult ResetPasswordAsync(AppUserDto user, string Code, string Password);

    AppUser FindByEmailAsync(string Email);

    bool IsEmailConfirmedAsync(AppUserDto user);

    IdentityResult ConfirmEmailAsync(AppUserDto user, string code);

    AppUser FindByIdAsync(string userId);

    IdentityResult CreateAsync(AppUserDto user);
    IdentityResult CreateAsync(AppUserDto user, string Password);
    IdentityResult AddLoginAsync(AppUserDto user, ExternalLoginInfo info);
    IList<string> GetValidTwoFactorProvidersAsync(AppUserDto user);

    #endregion

    #region SignInManager
    SignInResult PasswordSignInAsync(string Email, string Password, bool RememberMe, bool lockoutOnFailure = false);
    void SignInAsync(AppUserDto user, bool isPersistent = false);

    void SignOutAsync();
    AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl);
    IEnumerable<AuthenticationScheme> GetExternalAuthenticationSchemesAsync();
    ExternalLoginInfo GetExternalLoginInfoAsync();
    SignInResult ExternalLoginSignInAsync(string LoginProvider, string ProviderKey, bool isPersistent = false);
    IdentityResult UpdateExternalAuthenticationTokensAsync(ExternalLoginInfo info);
    AppUser GetTwoFactorAuthenticationUserAsync();
    SignInResult TwoFactorAuthenticatorSignInAsync(string Code, bool RememberMe, bool RememberBrowser);
    SignInResult TwoFactorSignInAsync(string Provider, string Code, bool RememberMe, bool RememberBrowser);
    SignInResult TwoFactorRecoveryCodeSignInAsync(string Code);

    #endregion

    void SendCode(string provider, string code, AppUserDto user);
}
