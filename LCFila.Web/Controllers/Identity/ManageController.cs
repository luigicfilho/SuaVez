using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LCFila.Application.Interfaces.Identity;
using LCFila.Web.Models;
using LCFila.Web.Models.Identity.ManageViewModels;
using LCFila.Web.Models.User;

namespace IdentitySamples.Controllers;

[Authorize]
public class ManageController : Controller
{
    private readonly IIdentityManagerService _identityManagerService;
    private readonly ILogger _logger;

    public ManageController(
            IIdentityManagerService identityManagerService,
            ILoggerFactory loggerFactory)
    {
        _identityManagerService = identityManagerService;
        _logger = loggerFactory.CreateLogger<ManageController>();
    }

    [HttpGet]
    public IActionResult Index(ManageMessageId? message = null)
    {
        ViewData["StatusMessage"] =
            message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
            : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
            : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
            : message == ManageMessageId.Error ? "An error has occurred."
            : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
            : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
            : "";

        var user = GetCurrentUserAsync();
        var model = new IndexViewModel
        {
            HasPassword = _identityManagerService.HasPasswordAsync(user.ConvertToAppUser()),
            PhoneNumber = _identityManagerService.GetPhoneNumberAsync(user.ConvertToAppUser()),
            TwoFactor = _identityManagerService.GetTwoFactorEnabledAsync(user.ConvertToAppUser()),
            Logins = _identityManagerService.GetLoginsAsync(user.ConvertToAppUser()),
            BrowserRemembered = _identityManagerService.IsTwoFactorClientRememberedAsync(user.ConvertToAppUser()),
            AuthenticatorKey = _identityManagerService.GetAuthenticatorKeyAsync(user.ConvertToAppUser())
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult RemoveLogin(RemoveLoginViewModel account)
    {
        ManageMessageId? message = ManageMessageId.Error;
        var user = GetCurrentUserAsync();
        if (user != null)
        {
            var result = _identityManagerService.RemoveLoginAsync(user.ConvertToAppUser(), account.LoginProvider, account.ProviderKey);
            if (result.Succeeded)
            {
                _identityManagerService.SignInAsync(user.ConvertToAppUser(), isPersistent: false);
                message = ManageMessageId.RemoveLoginSuccess;
            }
        }
        return RedirectToAction(nameof(ManageLogins), new { Message = message });
    }

    public IActionResult AddPhoneNumber()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddPhoneNumber(AddPhoneNumberViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        // Generate the token and send it
        var user = GetCurrentUserAsync();
        var code = _identityManagerService.GenerateChangePhoneNumberTokenAsync(user.ConvertToAppUser(), model.PhoneNumber);
        //await _smsSender.SendSmsAsync(model.PhoneNumber, "Your security code is: " + code);
        return RedirectToAction(nameof(VerifyPhoneNumber), new { PhoneNumber = model.PhoneNumber });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ResetAuthenticatorKey()
    {
        var user = GetCurrentUserAsync();
        if (user != null)
        {
            _identityManagerService.ResetAuthenticatorKeyAsync(user.ConvertToAppUser());
            _logger.LogInformation(1, "User reset authenticator key.");
        }
        return RedirectToAction(nameof(Index), "Manage");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult GenerateRecoveryCode()
    {
        var user = GetCurrentUserAsync();
        if (user != null)
        {
            var codes = _identityManagerService.GenerateNewTwoFactorRecoveryCodesAsync(user.ConvertToAppUser(), 5);
            _logger.LogInformation(1, "User generated new recovery code.");
            return View("DisplayRecoveryCodes", new DisplayRecoveryCodesViewModel { Codes = codes });
        }
        return View("Error");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult EnableTwoFactorAuthentication()
    {
        var user = GetCurrentUserAsync();
        if (user != null)
        {
            _identityManagerService.SetTwoFactorEnabledAsync(user.ConvertToAppUser(), true);
            _identityManagerService.SignInAsync(user.ConvertToAppUser(), isPersistent: false);
            _logger.LogInformation(1, "User enabled two-factor authentication.");
        }
        return RedirectToAction(nameof(Index), "Manage");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DisableTwoFactorAuthentication()
    {
        var user = GetCurrentUserAsync();
        if (user != null)
        {
            _identityManagerService.SetTwoFactorEnabledAsync(user.ConvertToAppUser(), false);
            _identityManagerService.SignInAsync(user.ConvertToAppUser(), isPersistent: false);
            _logger.LogInformation(2, "User disabled two-factor authentication.");
        }
        return RedirectToAction(nameof(Index), "Manage");
    }

    [HttpGet]
    public IActionResult VerifyPhoneNumber(string phoneNumber)
    {
        var code = _identityManagerService.GenerateChangePhoneNumberTokenAsync(GetCurrentUserAsync().ConvertToAppUser(), phoneNumber);
        // Send an SMS to verify the phone number
        return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        var user = GetCurrentUserAsync();
        if (user != null)
        {
            var result = _identityManagerService.ChangePhoneNumberAsync(user.ConvertToAppUser(), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                _identityManagerService.SignInAsync(user.ConvertToAppUser(), isPersistent: false);
                return RedirectToAction(nameof(Index), new { Message = ManageMessageId.AddPhoneSuccess });
            }
        }
        // If we got this far, something failed, redisplay the form
        ModelState.AddModelError(string.Empty, "Failed to verify phone number");
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult RemovePhoneNumber()
    {
        var user = GetCurrentUserAsync();
        if (user != null)
        {
            var result = _identityManagerService.SetPhoneNumberAsync(user.ConvertToAppUser(), null);
            if (result.Succeeded)
            {
                _identityManagerService.SignInAsync(user.ConvertToAppUser(), isPersistent: false);
                return RedirectToAction(nameof(Index), new { Message = ManageMessageId.RemovePhoneSuccess });
            }
        }
        return RedirectToAction(nameof(Index), new { Message = ManageMessageId.Error });
    }

    [HttpGet]
    public IActionResult ChangePassword()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ChangePassword(ChangePasswordViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        var user = GetCurrentUserAsync();
        if (user != null)
        {
            var result = _identityManagerService.ChangePasswordAsync(user.ConvertToAppUser(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                _identityManagerService.SignInAsync(user.ConvertToAppUser(), isPersistent: false);
                _logger.LogInformation(3, "User changed their password successfully.");
                return RedirectToAction(nameof(Index), new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result.ConvertToIdentityResult());
            return View(model);
        }
        return RedirectToAction(nameof(Index), new { Message = ManageMessageId.Error });
    }

    [HttpGet]
    public IActionResult SetPassword()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult SetPassword(SetPasswordViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = GetCurrentUserAsync();
        if (user != null)
        {
            var result = _identityManagerService.AddPasswordAsync(user.ConvertToAppUser(), model.NewPassword);
            if (result.Succeeded)
            {
                _identityManagerService.SignInAsync(user.ConvertToAppUser(), isPersistent: false);
                return RedirectToAction(nameof(Index), new { Message = ManageMessageId.SetPasswordSuccess });
            }
            AddErrors(result.ConvertToIdentityResult());
            return View(model);
        }
        return RedirectToAction(nameof(Index), new { Message = ManageMessageId.Error });
    }

    [HttpGet]
    public IActionResult ManageLogins(ManageMessageId? message = null)
    {
        ViewData["StatusMessage"] =
            message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
            : message == ManageMessageId.AddLoginSuccess ? "The external login was added."
            : message == ManageMessageId.Error ? "An error has occurred."
            : "";
        var user = GetCurrentUserAsync();
        if (user == null)
        {
            return View("Error");
        }
        var userLogins = _identityManagerService.GetLoginsAsync(user.ConvertToAppUser());
        var schemes = _identityManagerService.GetExternalAuthenticationSchemesAsync();
        var otherLogins = schemes.Where(auth => userLogins.All(ul => auth.Name != ul.LoginProvider)).ToList();
        ViewData["ShowRemoveButton"] = user.PasswordHash != null || userLogins.Count > 1;
        return View(new ManageLoginsViewModel
        {
            CurrentLogins = userLogins,
            OtherLogins = otherLogins
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult LinkLogin(string provider)
    {
        // Request a redirect to the external login provider to link a login for the current user
        var redirectUrl = Url.Action("LinkLoginCallback", "Manage");
        var properties = _identityManagerService.ConfigureExternalAuthenticationProperties(provider, redirectUrl, _identityManagerService.GetUserId(User));
        return Challenge(properties, provider);
    }

    [HttpGet]
    public ActionResult LinkLoginCallback()
    {
        var user = GetCurrentUserAsync();
        if (user == null)
        {
            return View("Error");
        }
        var info = _identityManagerService.GetExternalLoginInfoAsync(_identityManagerService.GetUserIdAsync(user.ConvertToAppUser()));
        if (info == null)
        {
            return RedirectToAction(nameof(ManageLogins), new { Message = ManageMessageId.Error });
        }
        var result = _identityManagerService.AddLoginAsync(user.ConvertToAppUser(), info);
        var message = result.Succeeded ? ManageMessageId.AddLoginSuccess : ManageMessageId.Error;
        return RedirectToAction(nameof(ManageLogins), new { Message = message });
    }

    #region Helpers
    private void AddErrors(List<IdentityResultViewModel> result)
    {
        foreach (var error in result)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
    }

    public enum ManageMessageId
    {
        AddPhoneSuccess,
        AddLoginSuccess,
        ChangePasswordSuccess,
        SetTwoFactorSuccess,
        SetPasswordSuccess,
        RemoveLoginSuccess,
        RemovePhoneSuccess,
        Error
    }

    private AppUserViewModel GetCurrentUserAsync()
    {
        AppUserViewModel user = new();
        user.ConvertToViewModel(_identityManagerService.GetUserAsync(HttpContext.User));
        return user;
    }
    #endregion
}
