using Microsoft.AspNetCore.Mvc;

namespace Classwork_7_Toyota_Color_Links_19_08.Controllers;

public class PageController : Controller
{
    
    public IActionResult ColorPage()
    {
        return View();
    }
}