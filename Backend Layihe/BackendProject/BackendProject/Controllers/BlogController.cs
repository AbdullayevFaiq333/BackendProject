using BackendProject.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? page = 1)
        {
            ViewBag.PageCount = Math.Ceiling((decimal)_context.Blogs.Count() / 9);
            ViewBag.Page = page;
            var blogs = _context.Blogs.Where(c => c.IsDeleted == false).ToList();
            return View(blogs);
        }

        public async Task<IActionResult> Detail(int? id)
        {

            if (id == null)
                return NotFound();
            var blogDeatil = await _context.BlogDetails.Where(c => c.IsDeleted == false).Include(x => x.Blog).FirstOrDefaultAsync(x => x.BlogId == id);
            if (blogDeatil == null)
                return NotFound();
            
            return View(blogDeatil);
        }
    }
}
