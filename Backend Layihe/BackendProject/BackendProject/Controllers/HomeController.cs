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
                    .Where(x=>x.IsDeleted==false && x.Name.ToLower().Contains(search.ToLower())).Take(10).ToList();

                return PartialView("_SearchCoursePartial", courses);
            }
            else if (path == "Blog")
            {
                var blogs = _context.Blogs.OrderByDescending(x => x.Id)
                    .Where(x => x.IsDeleted == false && x.Name.ToLower().Contains(search.ToLower()) || x.MainDescription.ToLower().Contains(search.ToLower())).Take(10).ToList();

                return PartialView("_SearchBlogPartial", blogs);
            }
            else if (path == "Event")
            {
                var events = _context.Events.OrderByDescending(x => x.Id)
                    .Where(x => x.IsDelete == false && x.Title.ToLower().Contains(search.ToLower())).Take(10).ToList();

                return PartialView("_SearchEventPartial", events);
            }
            else if (path == "Teacher")
            {
                var teachers = _context.Teachers.OrderByDescending(x => x.Id)
                    .Where(x => x.IsDeleted == false && x.Name.ToLower().Contains(search.ToLower())).Take(10).ToList();

                return PartialView("_SearchTeacherPartial", teachers);
            }
            return PartialView("_SearchCoursePartial");
        }

        public async Task<IActionResult> GlobalSearch(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return NotFound();
            }

            var searchViewModel = new SearchViewModel
            {
                Blogs = await _context.Blogs.Where(x => x.IsDeleted == false && x.Name.Contains(search.ToLower()))
                                .OrderByDescending(x => x.Id).Take(4).ToListAsync(),
                Courses = await _context.Courses.Where(x => x.IsDeleted == false && x.Name.Contains(search.ToLower()))
                                .OrderByDescending(x => x.Id).Take(4).ToListAsync(),
                Events = await _context.Events.Where(x => x.IsDelete == false && x.Title.Contains(search.ToLower()))
                                .OrderByDescending(x => x.Id).Take(4).ToListAsync(),
                Teachers = await _context.Teachers.Where(x => x.IsDeleted == false && x.Name.Contains(search.ToLower())).Take(4).ToListAsync()
            };

            return PartialView("_SearchViewPartial", searchViewModel);
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
