using Microsoft.AspNetCore.Mvc;

namespace OGWeb.UI.Areas.Cms.Controllers;

[Area("Cms")]
public class HomeController : Controller
{
    [Route("/cms")]
    public IActionResult Index()
    {
        return View();
    }
}
