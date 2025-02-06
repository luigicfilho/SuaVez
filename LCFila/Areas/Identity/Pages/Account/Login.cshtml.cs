using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LCFilaApplication.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using LCFilaInfra.Interfaces;

namespace LCFila.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IEmpresaLoginRepository _empresaRepository;

        public LoginModel(SignInManager<AppUser> signInManager, 
            ILogger<LoginModel> logger,
            UserManager<AppUser> userManager,
            IEmpresaLoginRepository empresaRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _empresaRepository = empresaRepository;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "O campo Email é obrigatório.")]
            [EmailAddress]
            public string Email { get; set; }

            [Required(ErrorMessage = "O campo Senha é obrigatório.")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Lembrar senha")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (Input.Email.IndexOf('@') > -1)
            {
                //Validate email format
                string emailRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                       @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                          @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
                Regex re = new Regex(emailRegex);
                if (!re.IsMatch(Input.Email))
                {
                    ModelState.AddModelError("Email", "Email is not valid");
                }
            }
            else
            {
                //validate Username format
                string emailRegex = @"^[a-zA-Z0-9]*$";
                Regex re = new Regex(emailRegex);
                if (!re.IsMatch(Input.Email))
                {
                    ModelState.AddModelError("Email", "Username is not valid");
                }
            }

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                //var user = await _userManager.FindByEmailAsync(Input.Email);
                //if (user == null)
                //{
                //    return SignInResult.Failed;
                //}

                //AppUser user;
                //if (Input.Email.Contains("@"))
                //    user = UserManager.FindByEmail(Input.Email);
                //else
                //    user = UserManager.FindByName(Input.Email);
                //var user = await _userManager.FindByNameAsync(Input.Email) ?? await _userManager.FindByEmailAsync(Input.Email);

                //var userName = Input.Email;
                //if (userName.IndexOf('@') > -1)
                //{
                //    var user = await _userManager.FindByEmailAsync(Input.Email);
                //    if (user == null)
                //    {
                //        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                //        return Page();
                //    }
                //    else
                //    {
                //        userName = user.UserName;
                //    }
                //}
              
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    var user = await _userManager.Users.SingleOrDefaultAsync(p => p.Email == Input.Email);
                    var roles = await _userManager.GetRolesAsync(user);

                    var teste = roles.Where(p => p.Contains("SysAdmin")).FirstOrDefault();
                    if (teste != null)
                    {
                        _logger.LogInformation("SysAdmin logged in.");
                        return RedirectToAction("Index", "Sysadmin");
                    }
                    var AllEmpresas = await _empresaRepository.ObterTodos();
                    var empresa = AllEmpresas.FirstOrDefault(p => p.IdAdminEmpresa == Guid.Parse(user.Id));
                    if (empresa.Ativo) { 
                        _logger.LogInformation("User logged in.");
                        return LocalRedirect(returnUrl);
                    } else
                    {
                        await _signInManager.SignOutAsync();
                        _logger.LogInformation("User logged out.");
                        return RedirectToPage("./Lockout");
                    }
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Tentativa de login inválida.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
