using BackendProject.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.ViewComponents
{
    public class AboutViewComponent:ViewComponent
    {
        private readonly AppDbContext _context;

        public AboutViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var about = await _context.About.FirstOrDefaultAsync();

            return View(about);
        }
    }
}
