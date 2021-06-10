using BackendProject.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.ViewComponents
{
    public class BlogViewComponent:ViewComponent
    {
        private readonly AppDbContext _context;

        public BlogViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int count = 3, int page = 1)
        {
            var blogs = await _context.Blogs.OrderByDescending(x => x.Id).Skip((page - 1) * count).Take(count).ToListAsync();
            return View(blogs);
        }
    }
}
