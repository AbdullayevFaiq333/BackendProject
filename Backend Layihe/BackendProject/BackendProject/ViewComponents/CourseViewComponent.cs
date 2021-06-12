using BackendProject.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.ViewComponents
{
    public class CourseViewComponent:ViewComponent
    {
        private readonly AppDbContext _context;

        public CourseViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int count=3,int page=1)
        {
            
            var courses = await _context.Courses.Where(x=>x.IsDeleted==false).OrderByDescending(x=>x.Id).Skip((page-1)*count).Take(count).ToListAsync();
            return View(courses);
        }
    }
}
