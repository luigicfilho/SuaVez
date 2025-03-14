using Microsoft.FeatureManagement.Mvc;
using LCFila.Application.Interfaces;
using LCFila.Web.Controllers.Sistema;

namespace LCFila.Web.Controllers;

[FeatureGate("Scheduling")]
public class SchedulerController : BaseController
{
    public SchedulerController(IConfigAppService configAppService) : base(configAppService)
    {
    }
}
