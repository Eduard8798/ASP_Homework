using Microsoft.AspNetCore.Mvc;

namespace Homerwork_4.Controllers;

public class PageController : Controller
{
    public IActionResult About()
    {
        return View();
    }
}