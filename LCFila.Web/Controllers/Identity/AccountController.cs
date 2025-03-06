using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using LCFila.Application.Interfaces.Identity;
using LCFila.Controllers;
using LCFila.Web.Models;
using LCFila.Web.Models.Identity.AccountViewModels;

namespace IdentitySample.Controllers;

[Authorize]
public class AccountController : Controller
{
    private readonly IIdentityService _identityService;
    private readonly ILogger _logger;

    public AccountController(
        IIdentityService identityService,
        ILoggerFactory loggerFactory)
    {
        _identityService = identityService;
        _logger = loggerFactory.CreateLogger<AccountController>();
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login(string? returnUrl)
    {
        //TODO: Review
        
        //if (!string.IsNullOrEmpty(ErrorMessage))
        //{
        //    ModelState.AddModelError(string.Empty, ErrorMessage);
        //}

        returnUrl = returnUrl ?? Url.Content("~/");

        // Clear the existing external cookie to ensure a clean login process
        HttpContext.SignOutAsync("Identity.External");

        //ExternalLogins = (_signInManager.GetExternalAuthenticationSchemesAsync()).Result.ToList();

        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public IActionResult Login(LoginViewModel model, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl ?? "";
        if (ModelState.IsValid)
        {
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = _identityService.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                _logger.LogInformation(1, "User logged in.");
                return RedirectToLocal(returnUrl!);
            }
            if (result.RequiresTwoFactor)
            {
                return RedirectToAction(nameof(SendCode), new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
            }
            if (result.IsLockedOut)
            {
                _logger.LogWarning(2, "User account locked out.");
                return View("Lockout");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }
        }

        // If we got this far, something failed, redisplay form
        return View(model);

        #region oldLogin
        //returnUrl = returnUrl ?? Url.Content("~/");

        //if (Input.Email.IndexOf('@') > -1)
        //{
        //    //Validate email format
        //    string emailRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
        //                           @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
        //                              @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        //    Regex re = new Regex(emailRegex);
        //    if (!re.IsMatch(Input.Email))
        //    {
        //        ModelState.AddModelError("Email", "Email is not valid");
        //    }
        //}
        //else
        //{
        //    //validate Username format
        //    string emailRegex = @"^[a-zA-Z0-9]*$";
        //    Regex re = new Regex(emailRegex);
        //    if (!re.IsMatch(Input.Email))
        //    {
        //        ModelState.AddModelError("Email", "Username is not valid");
        //    }
        //}

        //if (ModelState.IsValid)
        //{
        //    var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);

        //if (result.Succeeded)
        //{
        //    var user = await _userManager.Users.SingleOrDefaultAsync(p => p.Email == Input.Email);
        //    var roles = await _userManager.GetRolesAsync(user);

        //    var teste = roles.Where(p => p.Contains("SysAdmin")).FirstOrDefault();
        //    if (teste != null)
        //    {
        //        _logger.LogInformation("SysAdmin logged in.");
        //        return RedirectToAction("Index", "Sysadmin");
        //    }
        //    var AllEmpresas = await _empresaRepository.ObterTodos();
        //    var empresa = AllEmpresas.FirstOrDefault(p => p.IdAdminEmpresa == Guid.Parse(user.Id));
        //    if (empresa.Ativo)
        //    {
        //        _logger.LogInformation("User logged in.");
        //        return LocalRedirect(returnUrl);
        //    }
        //    else
        //    {
        //        await _signInManager.SignOutAsync();
        //        _logger.LogInformation("User logged out.");
        //        return RedirectToPage("./Lockout");
        //    }
        //}
        //if (result.RequiresTwoFactor)
        //{
        //    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
        //}
        //if (result.IsLockedOut)
        //{
        //    _logger.LogWarning("User account locked out.");
        //    return RedirectToPage("./Lockout");
        //}
        //else
        //{
        //    ModelState.AddModelError(string.Empty, "Tentativa de login inválida.");
        //    return Page();
        //}
        //}
        #endregion
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register(string? returnUrl = null)
    {
        //TODO: Review
        //ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public IActionResult Register(RegisterViewModel model, string? returnUrl = null)
    {
        //TODO: Review
        
        ViewData["ReturnUrl"] = returnUrl;
        if (ModelState.IsValid)
        {
            var user = new AppUserViewModel { UserName = model.Email, Email = model.Email };
            var result = _identityService.CreateAsync(user.ConvertToAppUser(), model.Password);
            if (result.Succeeded)
            {
                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                // Send an email with this link
                //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                //await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
                //    "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a>");
                _identityService.SignInAsync(user.ConvertToAppUser(), isPersistent: false);
                _logger.LogInformation(3, "User created a new account with password.");
                return RedirectToLocal(returnUrl!);
            }
            AddErrors(result.ConvertToIdentityResult());
        }

        // If we got this far, something failed, redisplay form
        return View(model);
        /*
         returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
         
         */


    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult LogOff(string? returnUrl = null)
    {
        _identityService.SignOutAsync();
        _logger.LogInformation(4, "User logged out.");
        if (returnUrl != null)
        {
            return LocalRedirect(returnUrl);
        }
        else
        {
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public IActionResult ExternalLogin(string provider, string? returnUrl = null)
    {
        //TODO: Review
        // Request a redirect to the external login provider.
        var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });
        var properties = _identityService.ConfigureExternalAuthenticationProperties(provider, redirectUrl!);
        return Challenge(properties, provider);

        /*
         var redirectUrl = Url.Page("./ExternalLogin", pageHandler: "Callback", values: new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        --  ONPOST
        returnUrl = returnUrl ?? Url.Content("~/");
            // Get the information about the user from the external login provider
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ErrorMessage = "Error loading external login information during confirmation.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };

                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);

                        var userId = await _userManager.GetUserIdAsync(user);
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { area = "Identity", userId = userId, code = code },
                            protocol: Request.Scheme);

                        await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                        // If account confirmation is required, we need to show the link if we don't have a real email sender
                        if (_userManager.Options.SignIn.RequireConfirmedAccount)
                        {
                            return RedirectToPage("./RegisterConfirmation", new { Email = Input.Email });
                        }

                        await _signInManager.SignInAsync(user, isPersistent: false, info.LoginProvider);

                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            ProviderDisplayName = info.ProviderDisplayName;
            ReturnUrl = returnUrl;
            return Page();
         */
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult ExternalLoginCallback(string? returnUrl = null, string? remoteError = null)
    {
        //TODO: Review
        if (remoteError != null)
        {
            ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
            return View(nameof(Login));
        }
        var info = _identityService.GetExternalLoginInfoAsync();
        if (info == null)
        {
            return RedirectToAction(nameof(Login));
        }

        // Sign in the user with this external login provider if the user already has a login.
        var result = _identityService.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
        if (result.Succeeded)
        {
            // Update any authentication tokens if login succeeded
            _identityService.UpdateExternalAuthenticationTokensAsync(info);

            _logger.LogInformation(5, "User logged in with {Name} provider.", info.LoginProvider);
            return RedirectToLocal(returnUrl!);
        }
        if (result.RequiresTwoFactor)
        {
            return RedirectToAction(nameof(SendCode), new { ReturnUrl = returnUrl });
        }
        if (result.IsLockedOut)
        {
            return View("Lockout");
        }
        else
        {
            // If the user does not have an account, then ask the user to create an account.
            ViewData["ReturnUrl"] = returnUrl;
            ViewData["ProviderDisplayName"] = info.ProviderDisplayName;
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = email! });
        }
       
        /*
         returnUrl = returnUrl ?? Url.Content("~/");
            if (remoteError != null)
            {
                ErrorMessage = $"Error from external provider: {remoteError}";
                return RedirectToPage("./Login", new {ReturnUrl = returnUrl });
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ErrorMessage = "Error loading external login information.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor : true);
            if (result.Succeeded)
            {
                _logger.LogInformation("{Name} logged in with {LoginProvider} provider.", info.Principal.Identity.Name, info.LoginProvider);
                return LocalRedirect(returnUrl);
            }
            if (result.IsLockedOut)
            {
                return RedirectToPage("./Lockout");
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                ReturnUrl = returnUrl;
                ProviderDisplayName = info.ProviderDisplayName;
                if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
                {
                    Input = new InputModel
                    {
                        Email = info.Principal.FindFirstValue(ClaimTypes.Email)
                    };
                }
                return Page();
            }
         */
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public IActionResult ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string? returnUrl = null)
    {
        if (ModelState.IsValid)
        {
            // Get the information about the user from the external login provider
            var info = _identityService.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return View("ExternalLoginFailure");
            }
            var user = new AppUserViewModel { UserName = model.Email, Email = model.Email };
            var result = _identityService.CreateAsync(user.ConvertToAppUser());
            if (result.Succeeded)
            {
                result = _identityService.AddLoginAsync(user.ConvertToAppUser(), info);
                if (result.Succeeded)
                {
                    _identityService.SignInAsync(user.ConvertToAppUser(), isPersistent: false);
                    _logger.LogInformation(6, "User created an account using {Name} provider.", info.LoginProvider);

                    // Update any authentication tokens as well
                    _identityService.UpdateExternalAuthenticationTokensAsync(info);

                    return RedirectToLocal(returnUrl!);
                }
            }
            AddErrors(result.ConvertToIdentityResult());
        }

        ViewData["ReturnUrl"] = returnUrl;
        return View(model);
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult ConfirmEmail(string userId, string code)
    {
        if (userId == null || code == null)
        {
            return View("Error");
        }
        var user = _identityService.FindByIdAsync(userId);
        if (user == null)
        {
            return View("Error");
        }
        var result = _identityService.ConfirmEmailAsync(user, code);
        return View(result.Succeeded ? "ConfirmEmail" : "Error");
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public IActionResult ForgotPassword(ForgotPasswordViewModel model)
    {
        //TODO: Review
        if (ModelState.IsValid)
        {
            var user = _identityService.FindByEmailAsync(model.Email);
            if (user == null || !(_identityService.IsEmailConfirmedAsync(user)))
            {
                // Don't reveal that the user does not exist or is not confirmed
                return View("ForgotPasswordConfirmation");
            }

            // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
            // Send an email with this link
            //var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            //var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
            //await _emailSender.SendEmailAsync(model.Email, "Reset Password",
            //   "Please reset your password by clicking here: <a href=\"" + callbackUrl + "\">link</a>");
            //return View("ForgotPasswordConfirmation");
        }

        // If we got this far, something failed, redisplay form
        return View(model);

        /*
         if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please 
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { area = "Identity", code },
                    protocol: Request.Scheme);

                await _emailSender.SendEmailAsync(
                    Input.Email,
                    "Reiniciar Senha",
                    $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return Page();
         
         */
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult ForgotPasswordConfirmation()
    {
        return View();
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult ResetPassword(string? code = null)
    {
        return code == null ? View("Error") : View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public IActionResult ResetPassword(ResetPasswordViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        var user = _identityService.FindByEmailAsync(model.Email);
        if (user == null)
        {
            // Don't reveal that the user does not exist
            return RedirectToAction(nameof(AccountController.ResetPasswordConfirmation), "Account");
        }
        var result = _identityService.ResetPasswordAsync(user, model.Code, model.Password);
        if (result.Succeeded)
        {
            return RedirectToAction(nameof(AccountController.ResetPasswordConfirmation), "Account");
        }
        AddErrors(result.ConvertToIdentityResult());
        return View();
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult ResetPasswordConfirmation()
    {
        return View();
    }

    [HttpGet]
    [AllowAnonymous]
    public ActionResult SendCode(string? returnUrl = null, bool rememberMe = false)
    {
        var user = _identityService.GetTwoFactorAuthenticationUserAsync();
        if (user == null)
        {
            return View("Error");
        }
        var userFactors = _identityService.GetValidTwoFactorProvidersAsync(user);
        var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
        return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl!, RememberMe = rememberMe });
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public IActionResult SendCode(SendCodeViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        var user = _identityService.GetTwoFactorAuthenticationUserAsync();
        if (user == null)
        {
            return View("Error");
        }

        if (model.SelectedProvider == "Authenticator")
        {
            return RedirectToAction(nameof(VerifyAuthenticatorCode), new { ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        // Generate the token and send it
        var code = _identityService.GenerateTwoFactorTokenAsync(user, model.SelectedProvider);
        if (string.IsNullOrWhiteSpace(code))
        {
            return View("Error");
        }
        
        _identityService.SendCode(model.SelectedProvider, code, user);


        return RedirectToAction(nameof(VerifyCode), new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult VerifyCode(string provider, bool rememberMe, string? returnUrl = null)
    {
        // Require that the user has already logged in via username/password or external login
        var user = _identityService.GetTwoFactorAuthenticationUserAsync();
        if (user == null)
        {
            return View("Error");
        }
        return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl!, RememberMe = rememberMe });
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public IActionResult VerifyCode(VerifyCodeViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // The following code protects for brute force attacks against the two factor codes.
        // If a user enters incorrect codes for a specified amount of time then the user account
        // will be locked out for a specified amount of time.
        var result = _identityService.TwoFactorSignInAsync(model.Provider, model.Code, model.RememberMe, model.RememberBrowser);
        if (result.Succeeded)
        {
            return RedirectToLocal(model.ReturnUrl);
        }
        if (result.IsLockedOut)
        {
            _logger.LogWarning(7, "User account locked out.");
            return View("Lockout");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Invalid code.");
            return View(model);
        }
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult VerifyAuthenticatorCode(bool rememberMe, string? returnUrl = null)
    {
        // Require that the user has already logged in via username/password or external login
        var user = _identityService.GetTwoFactorAuthenticationUserAsync();
        if (user == null)
        {
            return View("Error");
        }
        return View(new VerifyAuthenticatorCodeViewModel { ReturnUrl = returnUrl!, RememberMe = rememberMe });
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public IActionResult VerifyAuthenticatorCode(VerifyAuthenticatorCodeViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // The following code protects for brute force attacks against the two factor codes.
        // If a user enters incorrect codes for a specified amount of time then the user account
        // will be locked out for a specified amount of time.
        var result = _identityService.TwoFactorAuthenticatorSignInAsync(model.Code, model.RememberMe, model.RememberBrowser);
        if (result.Succeeded)
        {
            return RedirectToLocal(model.ReturnUrl);
        }
        if (result.IsLockedOut)
        {
            _logger.LogWarning(7, "User account locked out.");
            return View("Lockout");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Invalid code.");
            return View(model);
        }
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult UseRecoveryCode(string? returnUrl = null)
    {
        // Require that the user has already logged in via username/password or external login
        var user = _identityService.GetTwoFactorAuthenticationUserAsync();
        if (user == null)
        {
            return View("Error");
        }
        return View(new UseRecoveryCodeViewModel { ReturnUrl = returnUrl! });
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public IActionResult UseRecoveryCode(UseRecoveryCodeViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = _identityService.TwoFactorRecoveryCodeSignInAsync(model.Code);
        if (result.Succeeded)
        {
            return RedirectToLocal(model.ReturnUrl);
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Invalid code.");
            return View(model);
        }
    }

    #region Helpers
    private void AddErrors(List<IdentityResultViewModel> result)
    {
        foreach (var error in result)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
    }

    private IActionResult RedirectToLocal(string returnUrl)
    {
        if (Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }
        else
        {
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
    #endregion
}
