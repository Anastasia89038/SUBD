using Microsoft.AspNetCore.Mvc;

namespace MyCompany.Controllers
{
    public class ControlPanelController : Controller
    {
        public IActionResult Index() => View();
    }
}
