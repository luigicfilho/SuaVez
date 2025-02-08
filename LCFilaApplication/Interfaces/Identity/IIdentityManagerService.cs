using LCFilaApplication.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace LCFila.Application.Interfaces.Identity;

public interface IIdentityManagerService
{
    bool HasPasswordAsync(AppUser user);
    string GetPhoneNumberAsync(AppUser user);
    bool GetTwoFactorEnabledAsync(AppUser user);
    IList<UserLoginInfo> GetLoginsAsync(AppUser user);
    bool IsTwoFactorClientRememberedAsync(AppUser user);
    string GetAuthenticatorKeyAsync(AppUser user);
    IdentityResult RemoveLoginAsync(AppUser user, string LoginProvider, string ProviderKey);
    bool SignInAsync(AppUser user, bool isPersistent);
    string GenerateChangePhoneNumberTokenAsync(AppUser user, string PhoneNumber);
    bool ResetAuthenticatorKeyAsync(AppUser user);
    IEnumerable<string> GenerateNewTwoFactorRecoveryCodesAsync(AppUser user, int val);
    bool SetTwoFactorEnabledAsync(AppUser user, bool val);
    IdentityResult ChangePhoneNumberAsync(AppUser user, string phoneNumber, string Code);
    IdentityResult SetPhoneNumberAsync(AppUser user, string? phoneNumber);
    IdentityResult ChangePasswordAsync(AppUser user, string OldPassword, string NewPassword);
    IdentityResult AddPasswordAsync(AppUser user, string NewPassword);
    IEnumerable<AuthenticationScheme> GetExternalAuthenticationSchemesAsync();
    AuthenticationProperties ConfigureExternalAuthenticationProperties(string? provider, [StringSyntax(StringSyntaxAttribute.Uri)] string? redirectUrl, string? userId = null);
    ExternalLoginInfo GetExternalLoginInfoAsync(string userId);
    IdentityResult AddLoginAsync(AppUser user, UserLoginInfo info);
    AppUser GetUserAsync(ClaimsPrincipal principal);
    string GetUserId(ClaimsPrincipal userClaims);
    string GetUserIdAsync(AppUser user);
}
