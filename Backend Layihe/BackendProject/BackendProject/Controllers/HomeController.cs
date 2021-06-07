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

        
    }
}
