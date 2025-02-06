using LCFila.Controllers.Sistema;
using LCFilaApplication.Consts;
using LCFilaApplication.Interfaces;
using Microsoft.FeatureManagement.Mvc;

namespace LCFila.Controllers;


[FeatureGate(Features.Scheduler)]
public class SchedulerController : BaseController
{
    public SchedulerController(INotificador notificador, IConfigAppService configAppService) : base(notificador, configAppService)
    {
    }
}
