using LCFila.Controllers.Sistema;
//TODO: There is a way to send the Feature stuff to app?
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
