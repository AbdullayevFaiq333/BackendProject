using BackendProject.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.ViewComponents
{
    public class LatestPostViewComponent:ViewComponent
    {
        private readonly AppDbContext _context;

        public LatestPostViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int count = 3)
        {
            var blogs = await _context.Blogs.Where(x => x.IsDeleted == false).OrderByDescending(x => x.Id).Take(count).ToListAsync();
            return View(blogs);
        }
    }
}
