using Microsoft.AspNetCore.Mvc;

namespace Homerwork_4.Controllers;

public class HeroController: Controller
{
    public IActionResult Home()
    {
        return View();
    }
}