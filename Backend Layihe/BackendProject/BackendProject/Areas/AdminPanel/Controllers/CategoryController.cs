using BackendProject.DAL;
using BackendProject.Data;
using BackendProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    //[Authorize(Roles = RoleConstants.AdminRole)]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            ViewBag.PageCount = Decimal.Ceiling((decimal)_context.Categories.Where(x => x.IsDeleted == false).Count() / 5);
            ViewBag.Page = page;

            if (ViewBag.PageCount < page || page <= 0)
                return NotFound();

            var categories = await _context.Categories.Where(x => x.IsDeleted == false)
                .OrderByDescending(x => x.Id).Skip((page - 1) * 5).Take(5).ToListAsync();

            return View(categories);
        }

        #region Create

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            var isExist = await _context.Categories.AnyAsync(x => x.Title == category.Title && x.IsDeleted == false);
            if (isExist)
            {
                ModelState.AddModelError("Name", "There is such a category");
                return View();
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        #endregion

        #region Update

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();

            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Category category)
        {
            if (id == null)
                return NotFound();

            if (id != category.Id)
                return NotFound();

            var dbCategory = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (dbCategory == null)
                return NotFound();

            var isExist = await _context.Categories.AnyAsync(x => x.Title == category.Title && x.Id != category.Id && x.IsDeleted == false);
            if (isExist)
            {
                ModelState.AddModelError("Name", "There is such a category");
                return View();
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            dbCategory.Title = category.Title;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        #endregion

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteCategory(int? id)
        {
            if (id == null)
                return NotFound();

            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (category == null)
                return NotFound();

            category.IsDeleted = true;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
