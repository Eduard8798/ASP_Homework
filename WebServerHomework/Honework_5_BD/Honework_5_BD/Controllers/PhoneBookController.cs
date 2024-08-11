using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Honework_5_BD.Data;
using Honework_5_BD.Models.PhoneNumber;

namespace Honework_5_BD.Controllers
{
    public class PhoneBookController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PhoneBookController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PhoneBook
        public async Task<IActionResult> Index()
        {
            return View(await _context.NumberBooks.ToListAsync());
        }

        // GET: PhoneBook/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var numberBookModel = await _context.NumberBooks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (numberBookModel == null)
            {
                return NotFound();
            }

            return View(numberBookModel);
        }

        // GET: PhoneBook/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PhoneBook/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Age,Name,Phone")] NumberBookModel numberBookModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(numberBookModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(numberBookModel);
        }

        // GET: PhoneBook/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var numberBookModel = await _context.NumberBooks.FindAsync(id);
            if (numberBookModel == null)
            {
                return NotFound();
            }
            return View(numberBookModel);
        }

        // POST: PhoneBook/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Age,Name,Phone")] NumberBookModel numberBookModel)
        {
            if (id != numberBookModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(numberBookModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NumberBookModelExists(numberBookModel.Id))
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
            return View(numberBookModel);
        }

        // GET: PhoneBook/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var numberBookModel = await _context.NumberBooks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (numberBookModel == null)
            {
                return NotFound();
            }

            return View(numberBookModel);
        }

        // POST: PhoneBook/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var numberBookModel = await _context.NumberBooks.FindAsync(id);
            if (numberBookModel != null)
            {
                _context.NumberBooks.Remove(numberBookModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NumberBookModelExists(int id)
        {
            return _context.NumberBooks.Any(e => e.Id == id);
        }
    }
}
