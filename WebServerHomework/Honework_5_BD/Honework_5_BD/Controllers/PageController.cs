using Microsoft.AspNetCore.Mvc;

namespace Honework_5_BD.Controllers;

public class PageController : Controller
{
    public IActionResult About()
    {
        return View();
    }
}