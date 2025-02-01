using LCFila.Controllers.Sistema;
using LCFila.ViewModels;
using LCFilaApplication.Interfaces;
using LCFilaApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LCFila.Controllers;

[Authorize(Roles = "SysAdmin,EmpAdmin,OperatorEmp")]
public class HomeController : BaseController
{
    private readonly ILogger<HomeController> _logger;
    private readonly SignInManager<AppUser> _signInManager;
    public HomeController(ILogger<HomeController> logger,
                          SignInManager<AppUser> signInManager,
                          INotificador notificador,
                          UserManager<AppUser> userManager,
                          IEmpresaLoginRepository empresaRepository) : base(notificador, userManager, empresaRepository)
    {

        _logger = logger;
        _signInManager = signInManager;

    }

    public IActionResult Index()
    {
        ConfigEmpresa();
        ErrorViewModel teste = new ErrorViewModel();
        teste.Mensagem = "OLA!";
        return View("index",teste);
    }

    public IActionResult Privacy()
    {
        ConfigEmpresa();
        return View();
    }

    public async Task<IActionResult> Logout(string returnUrl = null)
    {
        ConfigEmpresa();
        await _signInManager.SignOutAsync();
        _logger.LogInformation("User logged out.");
        return RedirectToAction("Index", "Home");
    }

    [Route("erro/{id:length(3,3)}")]
    public IActionResult Errors(int id)
    {
        ConfigEmpresa();
        var modelErro = new ErrorViewModel();

        if (id == 500)
        {
            modelErro.Mensagem = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte.";
            modelErro.Titulo = "Ocorreu um erro!";
            modelErro.ErroCode = id;
        }
        else if (id == 404)
        {
            modelErro.Mensagem = "A página que está procurando não existe! <br />Em caso de dúvidas entre em contato com nosso suporte";
            modelErro.Titulo = "Ops! Página não encontrada.";
            modelErro.ErroCode = id;
        }
        else if (id == 403)
        {
            modelErro.Mensagem = "Você não tem permissão para fazer isto.";
            modelErro.Titulo = "Acesso Negado";
            modelErro.ErroCode = id;
        }
        else
        {
            return StatusCode(500);
        }

        return View("Error", modelErro);
    }
}
