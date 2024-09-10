using Homeworl_7_Toyota_Color.Data;
using Homeworl_7_Toyota_Color.Models.Toyota;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Homeworl_7_Toyota_Color.Controllers;

public class ColorsHandMadeController : Controller
{
    private readonly ApplicationDbContext _context;

    public ColorsHandMadeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create ([Bind("Name","Rgb","Code")] ColorModel colorModel,IFormFile file)
    {
        string baseUrl = "/storageOne/color";
        
        if (file != null && file.Length > 0)
        {
            // Путь для сохранения файла (например, папка "uploads" в wwwroot)
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + baseUrl, file.FileName);
            
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);  // Создает директорию, если она не существует
            }
            
            // Сохранение файла на сервере
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Вы можете добавить информацию о файле в модель, если необходимо
            // Например, сохранить путь к файлу или его имя
            // colorModel.FilePath = filePath; // если добавить свойство для хранения пути
             colorModel.Url = baseUrl + "/" + file.FileName;
        }

        
        
        
            _context.Add(colorModel);
            await _context.SaveChangesAsync();
        

        return RedirectToAction(nameof(Create));
    }
}