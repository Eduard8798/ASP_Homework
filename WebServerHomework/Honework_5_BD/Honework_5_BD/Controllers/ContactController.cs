using Microsoft.AspNetCore.Mvc;

namespace Honework_5_BD.Controllers;

public class ContactController : Controller
{
    public IActionResult Form()
    {
        return View();
    }
}