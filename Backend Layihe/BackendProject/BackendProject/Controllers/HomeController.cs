using BackendProject.DAL;
using BackendProject.Models;
using BackendProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var backgroundAreas = _context.BackgroundAreas.ToList();
            var services = _context.Services.ToList();
            var coursesAreaBackground = _context.CoursesAreaBackground.FirstOrDefault();

            var homeViewModel = new HomeViewModel
            {
                BackgroundAreas = backgroundAreas,
                Services=services,
                CoursesAreaBackground=coursesAreaBackground
            };

            return View(homeViewModel);
        }
        public IActionResult Search(string search,string path)
        {
            if (string.IsNullOrEmpty(search))
            {
                return Content("Error");
            }
            if (path == "Course")
            {
                var courses = _context.Courses.OrderByDescending(x => x.Id)
                    .Where(x => x.Name.ToLower().Contains(search.ToLower())).Take(10).ToList();

                return PartialView("_SearchCoursePartial", courses);
            }
            else if (path == "Blog")
            {
                var blogs = _context.Blogs.OrderByDescending(x => x.Id)
                    .Where(x => x.Name.ToLower().Contains(search.ToLower()) || x.MainDescription.ToLower().Contains(search.ToLower())).Take(10).ToList();

                return PartialView("_SearchBlogPartial", blogs);
            }
            return PartialView("_SearchCoursePartial");
        }


    }
}
