using BackendProject.DAL;
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

        public IActionResult Index(int? page=1)
        {
            ViewBag.PageCount = Math.Ceiling((decimal)_context.Courses.Count() / 6);
            ViewBag.Page = page;
            return View();
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
                return NotFound();
            var couseDeatil = await _context.CourseDetails.Include(x => x.Course).FirstOrDefaultAsync(x => x.CourseId == id);
            if (couseDeatil == null)
                return NotFound();
            return View(couseDeatil);
        }
    }
}
