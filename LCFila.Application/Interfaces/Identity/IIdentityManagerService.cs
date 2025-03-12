using LCFila.Application.Dto;
using LCFila.Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace LCFila.Application.Interfaces.Identity;

public interface IIdentityManagerService
{
    bool HasPasswordAsync(AppUserDto user);
    string GetPhoneNumberAsync(AppUserDto user);
    bool GetTwoFactorEnabledAsync(AppUserDto user);
    IList<UserLoginInfo> GetLoginsAsync(AppUserDto user);
    bool IsTwoFactorClientRememberedAsync(AppUserDto user);
    string GetAuthenticatorKeyAsync(AppUserDto user);
    IdentityResult RemoveLoginAsync(AppUserDto user, string LoginProvider, string ProviderKey);
    bool SignInAsync(AppUserDto user, bool isPersistent);
    string GenerateChangePhoneNumberTokenAsync(AppUserDto user, string PhoneNumber);
    bool ResetAuthenticatorKeyAsync(AppUserDto user);
    IEnumerable<string> GenerateNewTwoFactorRecoveryCodesAsync(AppUserDto user, int val);
    bool SetTwoFactorEnabledAsync(AppUserDto user, bool val);
    IdentityResult ChangePhoneNumberAsync(AppUserDto user, string phoneNumber, string Code);
    IdentityResult SetPhoneNumberAsync(AppUserDto user, string? phoneNumber);
    IdentityResult ChangePasswordAsync(AppUserDto user, string OldPassword, string NewPassword);
    IdentityResult AddPasswordAsync(AppUserDto user, string NewPassword);
    IEnumerable<AuthenticationScheme> GetExternalAuthenticationSchemesAsync();
    AuthenticationProperties ConfigureExternalAuthenticationProperties(string? provider, [StringSyntax(StringSyntaxAttribute.Uri)] string? redirectUrl, string? userId = null);
    ExternalLoginInfo GetExternalLoginInfoAsync(string userId);
    IdentityResult AddLoginAsync(AppUserDto user, UserLoginInfo info);
    AppUserDto GetUserAsync(ClaimsPrincipal principal);
    string GetUserId(ClaimsPrincipal userClaims);
    string GetUserIdAsync(AppUserDto user);
}
