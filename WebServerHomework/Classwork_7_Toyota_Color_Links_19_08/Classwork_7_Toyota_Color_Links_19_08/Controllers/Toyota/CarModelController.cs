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
    public class CarModelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarModelController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Toyota
        public async Task<IActionResult> Index()
        {
            return View(await _context.Toyota.ToListAsync());
        }

        // GET: Toyota/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toyotaModel = await _context.Toyota
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toyotaModel == null)
            {
                return NotFound();
            }

            return View(toyotaModel);
        }

        // GET: Toyota/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Toyota/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] ToyotaModel toyotaModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(toyotaModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(toyotaModel);
        }

        // GET: Toyota/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toyotaModel = await _context.Toyota.FindAsync(id);
            if (toyotaModel == null)
            {
                return NotFound();
            }
            return View(toyotaModel);
        }

        // POST: Toyota/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ToyotaModel toyotaModel)
        {
            if (id != toyotaModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toyotaModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToyotaModelExists(toyotaModel.Id))
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
            return View(toyotaModel);
        }

        // GET: Toyota/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toyotaModel = await _context.Toyota
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toyotaModel == null)
            {
                return NotFound();
            }

            return View(toyotaModel);
        }

        // POST: Toyota/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var toyotaModel = await _context.Toyota.FindAsync(id);
            if (toyotaModel != null)
            {
                _context.Toyota.Remove(toyotaModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToyotaModelExists(int id)
        {
            return _context.Toyota.Any(e => e.Id == id);
        }
    }
}
