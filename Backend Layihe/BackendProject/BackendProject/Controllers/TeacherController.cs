using BackendProject.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Controllers
{
    public class TeacherController : Controller
    {
        private readonly AppDbContext _context;

        public TeacherController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? page = 1)
        {
            ViewBag.PageCount = Math.Ceiling((decimal)_context.Teachers.Count() / 6);
            ViewBag.Page = page;
            var teachers = _context.Teachers.Where(c => c.IsDeleted == false).ToList();
            return View(teachers);
        }
        public IActionResult Detail(int? id)
        {
            if (id == null)
                return NotFound();

            var teacherDetail = _context.TeacherDetails.Where(x => x.IsDeleted == false).Include(x => x.Teacher).ThenInclude(y => y.SocialMedias)
                .Include(t => t.Teacher).ThenInclude(t => t.Position).FirstOrDefault(z => z.TeacherId == id);

            if (teacherDetail == null)
                return NotFound();

            return View(teacherDetail);

        }
    }
}
