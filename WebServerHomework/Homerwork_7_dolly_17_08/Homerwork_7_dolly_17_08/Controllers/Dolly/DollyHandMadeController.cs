using Homerwork_7_dolly_17_08.Data;
using Homerwork_7_dolly_17_08.Models;
using Microsoft.AspNetCore.Mvc;

namespace Homerwork_7_dolly_17_08.Controllers.Dolly;

public class DollyHandMadeController : Controller
{
    private readonly ApplicationDbContext _context;

    public DollyHandMadeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Name", "Url", "Style")] DollyClothesModel dollyClothesModel,
        IFormFile file
    )
    {
        string baseUrl = "/storage/clothes";
        
        // Проверяем, загружен ли файл
        if (file != null && file.Length > 0)
        {
            // Указываем путь для сохранения файла
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + baseUrl, file.FileName);

            // Сохраняем файл на сервере
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            
            dollyClothesModel.Url = baseUrl + "/" + file.FileName;
        }
        
        
        _context.Add(dollyClothesModel);
        await _context.SaveChangesAsync();

        
        return RedirectToAction(nameof(Create));
    }
    

}