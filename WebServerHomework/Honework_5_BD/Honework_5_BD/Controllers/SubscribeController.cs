using Honework_5_BD.Data;
using Honework_5_BD.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Honework_5_BD.Controllers;

public class SubscribeController : Controller
{
    
    private readonly ApplicationDbContext _context;
/// <summary>
/// Создания класса контроллера
/// </summary>
/// <param name="context"> Соедениния с БД </param>
    public SubscribeController(ApplicationDbContext context)
    {
        _context = context;
    }
    /// <summary>
    /// Add user email to BD
    /// <param name="email">User email</param>
    /// <returns>THK message</returns>
    ///
    [HttpPost]
    [ValidateAntiForgeryToken]
    
    public IActionResult SaveEmail(String email) 
    {
        ViewBag.email = email;
        

        SubscribeModel newSubscribeModel = new SubscribeModel();

        newSubscribeModel.Email = email;
        
        
        //перезашрузка бд
        //ApplicationDbContext db = new ApplicationDbContext();

        _context.Subscribers.Add(newSubscribeModel);
        _context.SaveChangesAsync();
            
        return View();
    }
}