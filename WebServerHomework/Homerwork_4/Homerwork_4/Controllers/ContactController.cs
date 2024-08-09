using Microsoft.AspNetCore.Mvc;

namespace Homerwork_4.Controllers;

public class ContactController : Controller
{
    public IActionResult Form()
    {
        return View();
    }
}