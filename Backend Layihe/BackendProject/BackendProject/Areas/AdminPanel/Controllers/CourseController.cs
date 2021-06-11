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
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;

        public CourseController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var courses = _context.Courses.Where(x=>x.IsDeleted==false).OrderByDescending(x=>x.Id).ToList();
            return View(courses);
        }
        public IActionResult Detail(int? id)
        {
            if (id == null)
                return NotFound();

            var course = _context.Courses.Include(x=>x.CourseDetail).FirstOrDefault(x=>x.Id==id);

            if (course == null)
                return NotFound();

            return View(course);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {
            if (course.Photo == null)
            {
                ModelState.AddModelError("Photo", "Photo cannot be empty");
                return View();
            }
            if (!course.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "You must choose only Image");
                return View();
            }
            if (!course.Photo.IsSizeAllowed(2048))
            {
                ModelState.AddModelError("Photo", "Image size can be 2 MB");
                return View();
            }

            if (!ModelState.IsValid)
            {
                return View();
            }
            var courseImg = Path.Combine( Constants.ImageFolderPath, "course");
            var fileName = await FileUtil.GenerateFileAsync( courseImg ,course.Photo);
            course.Image = fileName;

            var isExist = await _context.Courses.AnyAsync(x => x.IsDeleted == false && x.Name.ToLower() == course.Name.ToLower());

            if (isExist)
            {
                ModelState.AddModelError("Name", "This name is available");
                return View();
            }

            await _context.AddRangeAsync(course, course.CourseDetail);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var course = _context.Courses.Include(x => x.CourseDetail).FirstOrDefault(x => x.Id == id);

            if (course == null)
                return NotFound();

            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]

        public async Task<IActionResult> DeleteCourse(int? id)
        {
            if (id == null)
                return NotFound();

            var course = _context.Courses.Include(x => x.CourseDetail).FirstOrDefault(x => x.Id == id);

            if (course == null)
                return NotFound();

            course.IsDeleted = true;
            await _context.SaveChangesAsync();
            
            return RedirectToAction("Index");
        }


        public IActionResult Update(int? id)
        {

            if (id == null)
                return NotFound();

            var course = _context.Courses
                .Include(x => x.CourseDetail).Where(y => y.IsDeleted == false)
                .FirstOrDefault(x => x.Id == id);
            if (course == null)
                return NotFound();

            return View(course);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Course course)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id == null)
                return NotFound();

            if (id != course.Id)
                return BadRequest();

            var dbCourse = _context.Courses
               .Include(x => x.CourseDetail).Where(y => y.IsDeleted == false)
               .FirstOrDefault(x => x.Id == id);

            if (dbCourse == null)
                return NotFound();


            var isExist = await _context.Courses.Where(x => x.IsDeleted == false)
                                                .AnyAsync(x => x.Name == course.Name && x.Id != course.Id);

            if (isExist)
            {
                ModelState.AddModelError("Name", "This course name already exist.");
                return View();
            }

            
            
            
            if (course.Image != null)
            {
                var path = Path.Combine(Constants.ImageFolderPath, "course", dbCourse.Image);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                var courseImg = Path.Combine(Constants.ImageFolderPath, "course");
                var fileName = await FileUtil.GenerateFileAsync(courseImg, course.Photo);

                if (course.Photo == null)
                {
                    ModelState.AddModelError("Photo", "Select photo.");
                    return View();
                }

                if (!course.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Select photo.");
                    return View();
                }

                if (!course.Photo.IsSizeAllowed(2048))
                {
                    ModelState.AddModelError("Photo", "Max size is 2 MB.");
                    return View();
                }
                course.Image = fileName;
            }
            

            dbCourse.Name = course.Name;
            dbCourse.Description = course.Description;
            dbCourse.CourseDetail.AboutCourse = course.CourseDetail.AboutCourse;
            dbCourse.CourseDetail.HowToApply = course.CourseDetail.HowToApply;
            dbCourse.CourseDetail.Certification = course.CourseDetail.Certification;
            dbCourse.CourseDetail.Starts = course.CourseDetail.Starts;
            dbCourse.CourseDetail.Duration = course.CourseDetail.Duration;
            dbCourse.CourseDetail.ClassDuration = course.CourseDetail.ClassDuration;
            dbCourse.CourseDetail.SkillLevel = course.CourseDetail.SkillLevel;
            dbCourse.CourseDetail.Language = course.CourseDetail.Language;
            dbCourse.CourseDetail.Students = course.CourseDetail.Students;
            dbCourse.CourseDetail.Assesments = course.CourseDetail.Assesments;
            dbCourse.CourseDetail.CourseFee = course.CourseDetail.CourseFee;

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
