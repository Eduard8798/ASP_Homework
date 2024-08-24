using System.Drawing;
using Honework_5_BD.Data;
using Honework_5_BD.Models.Tag;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Honework_5_BD.Controllers;

public class TagsController : Controller
{
    private readonly ApplicationDbContext _context;
    
    public TagsController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<IActionResult> IndexTag()
    {
        return View(await _context.Tag.ToListAsync());
    }
    
    // GET: Tag/Details/5
    public async Task<IActionResult> DetailsTag(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var tagModel = await _context.Tag
            .FirstOrDefaultAsync(m => m.Id == id);
        if (tagModel == null)
        {
            return NotFound();
        }

        return View(tagModel);

    }
    
    // GET: Tag/CreateTag
    
    public IActionResult CreateTag()
        {
            return View();
    
        }
// POST: Tag/CreateTag
    [HttpPost]
    [ValidateAntiForgeryToken]
    
    public async Task <IActionResult> CreateTag ([Bind("Id,Name")] TagModel tagModel)
    {
        if (ModelState.IsValid)
        {
            _context.Add(tagModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexTag));
        }

        return View (tagModel);

    }
    // GET: Tag/EditTag/

    public async Task<IActionResult> EditTag(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var tagModel = await _context.Tag.FindAsync(id);
        if (tagModel == null)
        {
            return NotFound();
        }

        return View(tagModel);
    }
    
    // POST: Tag/EditTag/
    
    [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> EditTag(int id, [Bind("Id,Name")] TagModel tagModel)
{
    if (id != tagModel.Id)
    {
        return NotFound();
    }

    if (ModelState.IsValid)
    {
        try
        {
            _context.Update(tagModel);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!tagModelExist(tagModel.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return RedirectToAction(nameof(IndexTag));
    }

    return View(tagModel);
}


    // GET: Tag/DeleteTag

    public async Task<IActionResult> DeleteTag(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var tagModel = await _context.Tag
            .FirstOrDefaultAsync(m => m.Id == id);
        if (tagModel == null)
        {
            return NotFound();
        }

        return View(tagModel);
    }
    
    

    // POST: Tag/DeleteTag
    [HttpPost, ActionName("DeleteTag")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmedTag(int id)
    {
        var tagModel = await _context.Tag.FindAsync(id);
        if (tagModel != null)
        {
            _context.Tag.Remove(tagModel);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(IndexTag));
    }
    
    
    
    private bool tagModelExist(int id)
    {
        return _context.Tag.Any(e => e.Id == id);
    }
    
}