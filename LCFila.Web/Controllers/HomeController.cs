using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LCFila.Controllers.Sistema;
using LCFila.Application.Interfaces;
using LCFila.Web.Models;

namespace LCFila.Controllers;

[Authorize(Roles = "SysAdmin,EmpAdmin,OperatorEmp")]
public class HomeController : BaseController
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUserAppService _userAppService;

    public HomeController(ILogger<HomeController> logger,
                          IUserAppService userAppService,
                          IConfigAppService configAppService) : base(configAppService)
    {
        _logger = logger;
        _userAppService = userAppService;
    }

    public IActionResult Index()
    {
        ConfigEmpresa();
        return View("index");
    }

    public IActionResult Privacy()
    {
        ConfigEmpresa();
        return View();
    }

    public IActionResult Logout(string? returnUrl)
    {
        ConfigEmpresa();
        var result = _userAppService.Logout();
        if (result)
        {
            _logger.LogInformation("User logged out.");
            return RedirectToAction("Index", "Home");
        }
        return BadRequest();
        
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
