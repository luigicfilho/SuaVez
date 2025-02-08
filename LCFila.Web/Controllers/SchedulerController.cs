using Microsoft.FeatureManagement.Mvc;
using LCFila.Controllers.Sistema;
using LCFila.Application.Interfaces;

namespace LCFila.Controllers;


[FeatureGate("Scheduling")]
public class SchedulerController : BaseController
{
    public SchedulerController(INotificador notificador, IConfigAppService configAppService) : base(notificador, configAppService)
    {
    }
}
