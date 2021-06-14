using BackendProject.DAL;
using BackendProject.Models;
using BackendProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
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

        public async Task<IActionResult> Subscribe(string email)
        {
            if (email == null)
            {
                return Content("Email cannot be null");
            }
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (!match.Success)
            {
                return Content("Write right Email");
            }
            var isExist = await _context.Subcribes.AnyAsync(x => x.Email == email);
            if (isExist)
            {
                return Content("You have already subscribed");
            }

            Subcribe subcribe = new Subcribe { Email = email };
            await _context.Subcribes.AddAsync(subcribe);
            await _context.SaveChangesAsync();
            return Content("Successfully completed");


        }
    }
}
