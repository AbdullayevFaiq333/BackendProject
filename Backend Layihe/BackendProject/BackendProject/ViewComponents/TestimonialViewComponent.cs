using BackendProject.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.ViewComponents
{
    public class TestimonialViewComponent:ViewComponent
    {
        private readonly AppDbContext _context;

        public TestimonialViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var testimonial = await _context.Testimonial.FirstOrDefaultAsync();

            return View(testimonial);
        }
    }
}
