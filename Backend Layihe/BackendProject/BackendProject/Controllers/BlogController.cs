using BackendProject.DAL;
using BackendProject.Models;
using BackendProject.ViewModels;
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
        public IActionResult Index( int? categoryId, int? page = 1)
        {
            var blogs = new List<Blog>();

            if (categoryId == null)
            {
                ViewBag.PageCount = Decimal.Ceiling((decimal)_context.Blogs.Where(x => x.IsDeleted == false).Count() / 9);
                ViewBag.Page = page;

                if (ViewBag.PageCount < page || page <= 0)
                    return NotFound();

                return View(blogs);
            }
            else
            {
                var categoryBlogs = _context.BlogCategories.Where(x => x.CategoryId == categoryId)
                    .Include(x => x.Blog).Where(x => x.Blog.IsDeleted == false).OrderByDescending(x => x.Blog.Id);
                foreach (var categoryBlog in categoryBlogs)
                {
                    blogs.Add(categoryBlog.Blog);
                }
                return View(blogs);
            }

            
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
                return NotFound();

            var blog = await _context.Blogs.Where(x => x.IsDeleted == false)
                .Include(x => x.BlogDetail).FirstOrDefaultAsync(x => x.Id == id);
            if (blog == null)
                return NotFound();
            var blogViewModel = new BlogViewModel
            {
                Categories = await _context.Categories.Include(x => x.BlogCategories.Where(y => y.Blog.IsDeleted == false)).ThenInclude(x => x.Blog).Where(x => x.IsDeleted == false).ToListAsync(),
                Blog = blog
            };

            return View(blogViewModel);

            
        }
    }
}
