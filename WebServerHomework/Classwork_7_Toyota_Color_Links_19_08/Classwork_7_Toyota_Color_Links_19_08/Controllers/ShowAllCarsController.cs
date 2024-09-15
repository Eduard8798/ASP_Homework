using Classwork_7_Toyota_Color_Links_19_08.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Classwork_7_Toyota_Color_Links_19_08.Controllers
{
    public class ShowAllCarsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShowAllCarsController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        
        // GET: ShowAllCarsController
        public ActionResult Index()
        {
            var allModels 
                = _context.Toyota
                .Include(tModel => tModel.Configurations)
                .ThenInclude(Configuration => Configuration.Colors)
                .ThenInclude(TColors => TColors.Color)
                .ToList();

           
            return View(allModels);
        }

    }
}
