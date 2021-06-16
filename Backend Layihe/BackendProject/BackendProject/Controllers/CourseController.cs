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
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;

        public CourseController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? categoryId,int? page=1)
        {
            var courses = new List<Course>();

            if (categoryId == null)
            {
                ViewBag.PageCount = Decimal.Ceiling((decimal)_context.Courses.Where(x => x.IsDeleted == false).Count() / 9);
                ViewBag.Page = page;

                if (ViewBag.PageCount < page || page <= 0)
                    return NotFound();

                return View(courses);
            }
            else
            {
                var categoryCourses = _context.CourseCategories.Where(x => x.CategoryId == categoryId)
                    .Include(x => x.Course).Where(x => x.Course.IsDeleted == false).OrderByDescending(x => x.Course.Id);
                foreach (var categoryCourse in categoryCourses)
                {
                    courses.Add(categoryCourse.Course);
                }
                return View(courses);
            }
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
                return NotFound();

            var course = await _context.Courses.Where(x => x.IsDeleted == false)
                .Include(x => x.CourseDetail).FirstOrDefaultAsync(x => x.Id == id);
            if (course == null)
                return NotFound();
            var courseViewModel = new CourseViewModel
            {
                Categories = await _context.Categories.Include(x => x.CourseCategories.Where(y => y.Course.IsDeleted == false)).ThenInclude(x => x.Course).Where(x => x.IsDeleted == false).ToListAsync(),
                Course = course
            };

            return View(courseViewModel);
        }

        
    }
}
