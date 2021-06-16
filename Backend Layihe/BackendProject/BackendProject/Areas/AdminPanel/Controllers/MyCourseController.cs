using BackendProject.Areas.AdminPanel.Utils;
using BackendProject.DAL;
using BackendProject.Models;
using Microsoft.AspNetCore.Identity;
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
    public class MyCourseController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;

        public MyCourseController(AppDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
                return NotFound();

            var myCourses = await _context.Courses.Where(x => x.IsDeleted == false && x.UserId == user.Id)
                .Include(x => x.CourseDetail).Include(x => x.User)
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * 5).Take(5).ToListAsync();

            ViewBag.PageCount = Decimal.Ceiling((decimal)myCourses.Count / 5);
            ViewBag.Page = page;

            if (ViewBag.PageCount < page || page <= 0)
                return NotFound();

            return View(myCourses);
        }

        #region Update

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
                return NotFound();

            var myCourses = await _context.Courses.Where(x => x.IsDeleted == false && x.UserId == user.Id)
                .Include(x => x.CourseDetail).Include(x => x.User).FirstOrDefaultAsync();
            if (myCourses == null)
                return NotFound();

            return View(myCourses);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Course course)
        {
            if (id == null)
                return NotFound();

            if (id != course.Id)
                return BadRequest();

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
                return NotFound();

            var myCourses = await _context.Courses.Where(x => x.IsDeleted == false && x.UserId == user.Id)
                .Include(x => x.CourseDetail).Include(x => x.User).FirstOrDefaultAsync();
            if (myCourses == null)
                return NotFound();

            if (!ModelState.IsValid)
            {
                return View();
            }

            var fileName = myCourses.Image;

            if (course.Photo != null)
            {
                if (!course.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "This is not a picture");
                    return View();
                }

                if (!course.Photo.IsSizeAllowed(3000))
                {
                    ModelState.AddModelError("Photo", "The size of the image you uploaded is 3 MB higher.");
                    return View();
                }

                var path = Path.Combine(Constants.ImageFolderPath, "course", myCourses.Image);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }


                var courseImg = Path.Combine(Constants.ImageFolderPath, "course");
                fileName = await FileUtil.GenerateFileAsync(courseImg, course.Photo);
               
            }

           

            myCourses.Image = fileName;
            myCourses.Name = course.Name;
            myCourses.Description = course.Description;
            myCourses.CourseDetail = course.CourseDetail;
            

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        #endregion

        #region Detail

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
                return NotFound();

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
                return NotFound();

            var myCourses = await _context.Courses.Where(x => x.IsDeleted == false && x.UserId == user.Id)
                .Include(x => x.CourseDetail).Include(x => x.User).FirstOrDefaultAsync();
            if (myCourses == null)
                return NotFound();

            return View(myCourses);
        }

        #endregion
    }
}
