using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Honework_5_BD.Data;
using Honework_5_BD.Models.Vender;

namespace Honework_5_BD.Controllers
{
    public class VenderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VenderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vender
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vender.ToListAsync());
        }

        // GET: Vender/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venderModel = await _context.Vender
                .FirstOrDefaultAsync(m => m.Id == id);
            if (venderModel == null)
            {
                return NotFound();
            }

            return View(venderModel);
        }

        // GET: Vender/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vender/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Contact,Address")] VenderModel venderModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(venderModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(venderModel);
        }

        // GET: Vender/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venderModel = await _context.Vender.FindAsync(id);
            if (venderModel == null)
            {
                return NotFound();
            }
            return View(venderModel);
        }

        // POST: Vender/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Contact,Address")] VenderModel venderModel)
        {
            if (id != venderModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venderModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VenderModelExists(venderModel.Id))
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
            return View(venderModel);
        }

        // GET: Vender/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venderModel = await _context.Vender
                .FirstOrDefaultAsync(m => m.Id == id);
            if (venderModel == null)
            {
                return NotFound();
            }

            return View(venderModel);
        }

        // POST: Vender/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venderModel = await _context.Vender.FindAsync(id);
            if (venderModel != null)
            {
                _context.Vender.Remove(venderModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VenderModelExists(int id)
        {
            return _context.Vender.Any(e => e.Id == id);
        }
    }
}
