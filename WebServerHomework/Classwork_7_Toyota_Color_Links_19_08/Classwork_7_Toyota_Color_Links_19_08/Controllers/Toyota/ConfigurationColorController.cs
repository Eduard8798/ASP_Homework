using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Classwork_7_Toyota_Color_Links_19_08.Data;
using Classwork_7_Toyota_Color_Links_19_08.Models;

namespace Classwork_7_Toyota_Color_Links_19_08.Controllers.Toyota
{
    public class ConfigurationColorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConfigurationColorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Configuration
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ConfigurationColors.Include(c => c.Color).Include(c => c.Configuration);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Configuration/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configurationColorsModel = await _context.ConfigurationColors
                .Include(c => c.Color)
                .Include(c => c.Configuration)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (configurationColorsModel == null)
            {
                return NotFound();
            }

            return View(configurationColorsModel);
        }

        // GET: Configuration/Create
        public IActionResult Create()
        {
            ViewData["ColorId"] = new SelectList(_context.Color, "Id", "Name");
            ViewData["ConfigurationId"] = new SelectList(_context.Configurations, "Id", "Name");
            return View();
        }

        // POST: Configuration/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,MainImageUrl,ColorId,ConfigurationId")] 
            ConfigurationColorsModel configurationColorsModel,IFormFile file)
        {
            string baseUrl = "/storage/configuration_color";
        
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
            
                configurationColorsModel.MainImageUrl = baseUrl + "/" + file.FileName;
            }
            
            configurationColorsModel.Configuration =
                _context.Configurations.Find(configurationColorsModel.ConfigurationId);
            
            configurationColorsModel.Color =
                _context.Color.Find(configurationColorsModel.ColorId);
            
           
                _context.Add(configurationColorsModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            ViewData["ColorId"] = new SelectList(_context.Color, "Id", "Name", configurationColorsModel.ColorId);
            ViewData["ConfigurationId"] = new SelectList(_context.Configurations, "Id", "Name", configurationColorsModel.ConfigurationId);
            return View(configurationColorsModel);
        }

        // GET: Configuration/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configurationColorsModel = await _context.ConfigurationColors.FindAsync(id);
            if (configurationColorsModel == null)
            {
                return NotFound();
            }
            ViewData["ColorId"] = new SelectList(_context.Color, "Id", "Name", configurationColorsModel.ColorId);
            ViewData["ConfigurationId"] = new SelectList(_context.Configurations, "Id", "Name", configurationColorsModel.ConfigurationId);
            return View(configurationColorsModel);
        }

        // POST: Configuration/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MainImageUrl,ColorId,ConfigurationId")] ConfigurationColorsModel configurationColorsModel)
        {
            if (id != configurationColorsModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(configurationColorsModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConfigurationColorsModelExists(configurationColorsModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ColorId"] = new SelectList(_context.Color, "Id", "Name", configurationColorsModel.ColorId);
            ViewData["ConfigurationId"] = new SelectList(_context.Configurations, "Id", "Name", configurationColorsModel.ConfigurationId);
            return View(configurationColorsModel);
        }

        // GET: Configuration/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configurationColorsModel = await _context.ConfigurationColors
                .Include(c => c.Color)
                .Include(c => c.Configuration)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (configurationColorsModel == null)
            {
                return NotFound();
            }

            return View(configurationColorsModel);
        }

        // POST: Configuration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var configurationColorsModel = await _context.ConfigurationColors.FindAsync(id);
            if (configurationColorsModel != null)
            {
                _context.ConfigurationColors.Remove(configurationColorsModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConfigurationColorsModelExists(int id)
        {
            return _context.ConfigurationColors.Any(e => e.Id == id);
        }
    }
}
