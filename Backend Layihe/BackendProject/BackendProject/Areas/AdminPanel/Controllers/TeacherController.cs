using BackendProject.Areas.AdminPanel.Utils;
using BackendProject.DAL;
using BackendProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class TeacherController : Controller
    {
        private readonly AppDbContext _context;

        public TeacherController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var teachers = _context.Teachers.Where(x => x.IsDeleted == false).Include(x=>x.Position).OrderByDescending(x => x.Id).ToList();
            return View(teachers);
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
                return NotFound();

            var teacher = await _context.Teachers.Where(x => x.IsDeleted == false).Include(x => x.TeacherDetail)
                                                .Include(x => x.SocialMedias).Include(x => x.Position)
                                                .FirstOrDefaultAsync(x => x.Id == id);

            if (teacher == null)
                return NotFound();

            return View(teacher);
        }

        public IActionResult Create()
        {
            var positions =  _context.Positions.Where(x => x.IsDeleted == false).ToList();
            ViewBag.Position = positions;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Teacher teacher, int PositionId)
        {
            var positions = await _context.Positions.Where(x => x.IsDeleted == false).ToListAsync();
            ViewBag.Position = positions;

            if (teacher.Photo == null)
            {
                ModelState.AddModelError("Photo", "Photo cannot be empty");
                return View();
            }
            if (!teacher.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "You must choose only Image");
                return View();
            }
            if (!teacher.Photo.IsSizeAllowed(2048))
            {
                ModelState.AddModelError("Photo", "Image size can be 2 MB");
                return View();
            }
            
            if (!ModelState.IsValid)
            {
                return View();
            }
            
            var teacherImg = Path.Combine(Constants.ImageFolderPath, "teacher");
            var fileName = await FileUtil.GenerateFileAsync(teacherImg, teacher.Photo);
            teacher.Image = fileName;
            teacher.PositionId = PositionId;
            
            await _context.AddRangeAsync(teacher, teacher.TeacherDetail);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Update(int? id)
        {

            var positions = await _context.Positions.Where(x => x.IsDeleted == false).ToListAsync();
            ViewBag.Position = positions;
            
            if (id == null)
                return NotFound();

            var teacher = await _context.Teachers.Where(x => x.IsDeleted == false).Include(x => x.TeacherDetail)
                                                .Include(x => x.SocialMedias).Include(x => x.Position)
                                                .FirstOrDefaultAsync(x=>x.Id==id);

            if (teacher == null)
                return NotFound();
            
            
            return View(teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(int? id,Teacher teacher,int? PositionId)
        {
            var positions = await _context.Positions.Where(x => x.IsDeleted == false).ToListAsync();
            ViewBag.Position = positions;

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id == null)
                return NotFound();

            if (id != teacher.Id)
                return BadRequest();

            var dbTeacher=await _context.Teachers.Where(x => x.IsDeleted == false).Include(x => x.TeacherDetail)
                                                .Include(x => x.SocialMedias).Include(x => x.Position)
                                                .FirstOrDefaultAsync(x => x.Id == id);

            if (dbTeacher == null)
                return NotFound();

            if (teacher.Image != null)
            {
                var path = Path.Combine(Constants.ImageFolderPath, "teacher", dbTeacher.Image);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                var courseImg = Path.Combine(Constants.ImageFolderPath, "teacher");
                var fileName = await FileUtil.GenerateFileAsync(courseImg, teacher.Photo);

                if (teacher.Photo == null)
                {
                    ModelState.AddModelError("Photo", "Select photo.");
                    return View();
                }

                if (!teacher.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Select photo.");
                    return View();
                }

                if (!teacher.Photo.IsSizeAllowed(2048))
                {
                    ModelState.AddModelError("Photo", "Max size is 2 MB.");
                    return View();
                }
                teacher.Image = fileName;
            }

            dbTeacher.Name = teacher.Name;
            dbTeacher.TeacherDetail.AboutMe = teacher.TeacherDetail.AboutMe;
            dbTeacher.TeacherDetail.Degree = teacher.TeacherDetail.Degree;
            dbTeacher.TeacherDetail.Experience = teacher.TeacherDetail.Experience;
            dbTeacher.TeacherDetail.Hobbies = teacher.TeacherDetail.Hobbies;
            dbTeacher.TeacherDetail.Faculty = teacher.TeacherDetail.Faculty;
            dbTeacher.TeacherDetail.Email = teacher.TeacherDetail.Email;
            dbTeacher.TeacherDetail.Skype = teacher.TeacherDetail.Skype;
            dbTeacher.TeacherDetail.Language = teacher.TeacherDetail.Language;
            dbTeacher.TeacherDetail.TeamLeader = teacher.TeacherDetail.TeamLeader;
            dbTeacher.TeacherDetail.Development = teacher.TeacherDetail.Development;
            dbTeacher.TeacherDetail.Design = teacher.TeacherDetail.Design;
            dbTeacher.TeacherDetail.Innovation = teacher.TeacherDetail.Innovation;
            dbTeacher.TeacherDetail.Communication = teacher.TeacherDetail.Communication;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var teacher = await _context.Teachers.Where(x => x.IsDeleted == false).Include(x => x.TeacherDetail)
                                                .Include(x => x.SocialMedias).Include(x => x.Position)
                                                .FirstOrDefaultAsync(x => x.Id == id);

            if (teacher == null)
                return NotFound();

            return View(teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]

        public async Task<IActionResult> DeleteTeacher(int? id)
        {
            if (id == null)
                return NotFound();

            var teacher = await _context.Teachers.Where(x => x.IsDeleted == false).Include(x => x.TeacherDetail)
                                                .Include(x => x.SocialMedias).Include(x => x.Position)
                                                .FirstOrDefaultAsync(x => x.Id == id);

            if (teacher == null)
                return NotFound();

            teacher.IsDeleted = true;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


    }
}
