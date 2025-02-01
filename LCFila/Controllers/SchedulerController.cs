using LCFila.Controllers.Sistema;
using LCFilaApplication.Consts;
using LCFilaApplication.Interfaces;
using LCFilaApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.FeatureManagement.Mvc;

namespace LCFila.Controllers;


[FeatureGate(Features.Scheduler)]
public class SchedulerController : BaseController
{
    public SchedulerController(INotificador notificador, UserManager<AppUser> userManager, IEmpresaLoginRepository empresaRepository) : base(notificador, userManager, empresaRepository)
    {
    }
}
