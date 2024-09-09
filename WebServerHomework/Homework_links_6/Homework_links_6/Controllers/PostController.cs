using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Homework_links_6.Data;
using Homework_links_6.Models;

namespace Homework_links_6.Controllers
{
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Post
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Posts
                .Include(t=> t.Tags)
                .Include(p => p.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Post/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postModel = await _context.Posts
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postModel == null)
            {
                return NotFound();
            }

            return View(postModel);
        }

        // GET: Post/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["Tags"] = new MultiSelectList(_context.Tags, "Id", "Name");
            return View();
        }

        // POST: Post/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Body,Slug,CategoryId")] PostModel postModel, int[] Tags)
        {
            // Проверка на валидность данных
            if (ModelState.IsValid)
            {
                // Проверяем, есть ли теги и добавляем их в пост
                if (Tags != null && Tags.Any())
                {
                    postModel.Tags = new List<TagModel>();

                    foreach (var tagId in Tags)
                    {
                        var tag = await _context.Tags.FindAsync(tagId);
                        if (tag != null)
                        {
                            postModel.Tags.Add(tag); // Добавляем тег в коллекцию тегов поста
                        }
                    }
                }

                // Добавляем пост в контекст и сохраняем изменения
                _context.Add(postModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Если данные не валидны, снова загружаем категории и теги
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", postModel.CategoryId);
            ViewData["Tags"] = new MultiSelectList(_context.Tags, "Id", "Name", Tags);
            return View(postModel);
        }

        // GET: Post/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postModel = await _context.Posts.FindAsync(id);
            if (postModel == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", postModel.CategoryId);
            ViewData["Tags"] = new MultiSelectList(_context.Categories, "Id", "Name");
            return View(postModel);
        }

        // POST: Post/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Body,Slug,CategoryId")] PostModel postModel, int[] Tags)
{
    if (id != postModel.Id)
    {
        return NotFound();
    }

    if (ModelState.IsValid)
    {
        // Загружаем существующий пост вместе с тегами из базы данных
        var existingPost = await _context.Posts
            .Include(p => p.Tags)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (existingPost == null)
        {
            return NotFound();
        }

        try
        {
            // Обновляем основные поля поста
            existingPost.Title = postModel.Title;
            existingPost.Body = postModel.Body;
            existingPost.Slug = postModel.Slug;
            existingPost.CategoryId = postModel.CategoryId;

            // Обновляем теги
            existingPost.Tags.Clear(); // Очищаем текущие теги
            if (Tags != null)
            {
                foreach (var tagId in Tags)
                {
                    var tag = await _context.Tags.FindAsync(tagId);
                    if (tag != null)
                    {
                        existingPost.Tags.Add(tag);
                    }
                }
            }

            // Сохраняем изменения в базе данных
            _context.Update(existingPost);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PostModelExists(postModel.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        // Перенаправляем пользователя на страницу списка постов
        return RedirectToAction(nameof(Index));
    }

    // В случае ошибки валидации загружаем данные категорий и тегов снова
    ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", postModel.CategoryId);
    ViewData["Tags"] = new MultiSelectList(_context.Tags, "Id", "Name", Tags); // Исправлено

    // Возвращаем представление с моделью поста для редактирования
    return View(postModel);
}


        // GET: Post/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postModel = await _context.Posts
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postModel == null)
            {
                return NotFound();
            }

            return View(postModel);
        }

        // POST: Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postModel = await _context.Posts.FindAsync(id);
            if (postModel != null)
            {
                _context.Posts.Remove(postModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostModelExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
