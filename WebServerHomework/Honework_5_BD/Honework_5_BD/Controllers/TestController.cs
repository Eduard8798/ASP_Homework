using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Honework_5_BD.Data;
using Honework_5_BD.Models.Entities;

namespace Honework_5_BD.Controllers
{
    public class TestController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TestController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Test
        public async Task<IActionResult> Index()
        {
            return View(await _context.Subscribers.ToListAsync());
        }

        // GET: Test/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscribeModel = await _context.Subscribers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subscribeModel == null)
            {
                return NotFound();
            }

            return View(subscribeModel);
        }

        // GET: Test/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Test/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email")] SubscribeModel subscribeModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subscribeModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subscribeModel);
        }

        // GET: Test/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscribeModel = await _context.Subscribers.FindAsync(id);
            if (subscribeModel == null)
            {
                return NotFound();
            }
            return View(subscribeModel);
        }

        // POST: Test/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email")] SubscribeModel subscribeModel)
        {
            if (id != subscribeModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subscribeModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubscribeModelExists(subscribeModel.Id))
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
            return View(subscribeModel);
        }

        // GET: Test/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscribeModel = await _context.Subscribers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subscribeModel == null)
            {
                return NotFound();
            }

            return View(subscribeModel);
        }

        // POST: Test/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subscribeModel = await _context.Subscribers.FindAsync(id);
            if (subscribeModel != null)
            {
                _context.Subscribers.Remove(subscribeModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubscribeModelExists(int id)
        {
            return _context.Subscribers.Any(e => e.Id == id);
        }
    }
}
