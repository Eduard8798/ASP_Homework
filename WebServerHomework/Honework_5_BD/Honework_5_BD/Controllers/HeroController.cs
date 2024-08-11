using Microsoft.AspNetCore.Mvc;

namespace Honework_5_BD.Controllers;

public class HeroController: Controller
{
    public IActionResult Home()
    {
        return View();
    }
}