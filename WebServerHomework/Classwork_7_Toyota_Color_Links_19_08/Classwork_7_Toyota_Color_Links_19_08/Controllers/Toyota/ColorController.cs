using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Classwork_7_Toyota_Color_Links_19_08.Data;
using Classwork_7_Toyota_Color_Links_19_08.Models;
using Classwork_7_Toyota_Color_Links_19_08.Models.ViewModels;

namespace Classwork_7_Toyota_Color_Links_19_08.Controllers.Toyota
{
    public class ColorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ColorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Color
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 20,
            string sortColumn = "Name", string sortDirection = "asc" )
        {
            
            var query = _context.Color.AsQueryable();
            // Сортировка
            query = sortColumn switch
            {
                // Якщо сортировка по Id
                "Id" => sortDirection == "asc" ? query.OrderBy(c => c.Id) : query.OrderByDescending(c => c.Id),
                // Якщо сортировка по Name
                "Name" => sortDirection == "asc" ? query.OrderBy(c => c.Name) : query.OrderByDescending(c => c.Name),
                // За замовченням
                _ => sortDirection == "asc" ? query.OrderBy(c => c.Name) : query.OrderByDescending(c => c.Name),
            };
            
            
            // Подсчитываем общее количество записей в таблице Color
                        var totalItems = await query.CountAsync();
                        
            // Получаем нужные записи, используя Skip и Take для пагинации
            var colors = await query
                .Skip((pageNumber - 1) * pageSize) // Пропускаем предыдущие страницы
                .Take(pageSize)                    // Берем количество записей для текущей страницы
                .ToListAsync();

            // Используем рефлексию для получения свойств модели ColorModel
            var properties = typeof(ColorModel).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Создаем список SelectListItem на основе свойств модели
            var columns = properties.Select(prop => new SelectListItem 
            { 
                Value = prop.Name, 
                Text = prop.Name 
            }).ToList();

            SelectList sort = new SelectList(columns, "Value", "Text", sortColumn);
            
            // Определяем возможные значения для направления сортировки
            var sortDirections = new List<SelectListItem>
            {
                new SelectListItem { Value = "asc", Text = "Ascending" },
                new SelectListItem { Value = "desc", Text = "Descending" }
            };
            SelectList dir = new SelectList(sortDirections, "Value", "Text", sortDirection);
            // Передаем информацию о пагинации во ViewData
            ViewData["Paginate"] = new PaginateViewModel
            {
                //Pagination
                
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                //Sort
                SortColumn = sortColumn,
                SortColumnSelectedList = sort,
                
                SortDirectionSelectedList = dir,
                Columns = new List<string> (["Id","Name"])
            };

            // Возвращаем список цветов и пагинацию
            return View(colors);
        }


        // GET: Color/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colorModel = await _context.Color
                .FirstOrDefaultAsync(m => m.Id == id);
            if (colorModel == null)
            {
                return NotFound();
            }

            return View(colorModel);
        }

        // GET: Color/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Color/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,RGB,Code")] ColorModel colorModel,IFormFile file)
        {
            string baseUrl = "/storage/color";
        
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
            
                colorModel.Url = baseUrl + "/" + file.FileName;
            }
            
            
            _context.Add(colorModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
            return View(colorModel);
        }

        // GET: Color/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colorModel = await _context.Color.FindAsync(id);
            if (colorModel == null)
            {
                return NotFound();
            }
            return View(colorModel);
        }

        // POST: Color/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Url,RGB,Code")] ColorModel colorModel)
        {
            if (id != colorModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(colorModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ColorModelExists(colorModel.Id))
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
            return View(colorModel);
        }

        // GET: Color/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colorModel = await _context.Color
                .FirstOrDefaultAsync(m => m.Id == id);
            if (colorModel == null)
            {
                return NotFound();
            }

            return View(colorModel);
        }

        // POST: Color/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var colorModel = await _context.Color.FindAsync(id);
            if (colorModel != null)
            {
                _context.Color.Remove(colorModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ColorModelExists(int id)
        {
            return _context.Color.Any(e => e.Id == id);
        }
    }
}
