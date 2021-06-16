using BackendProject.DAL;
using BackendProject.Data;
using BackendProject.Models;
using BackendProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class UserController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;

        public UserController(AppDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var dbUsers = await _userManager.Users.ToListAsync();
            var users = new List<UserViewModel>();

            foreach (var dbUser in dbUsers)
            {
                var user = new UserViewModel
                {
                    Id = dbUser.Id,
                    Username = dbUser.UserName,
                    Email = dbUser.Email,
                    Fullname = dbUser.Fullname,
                    IsDeleted = dbUser.IsDeleted,
                    Role = (await _userManager.GetRolesAsync(dbUser)).FirstOrDefault()
                };
                users.Add(user);
            }

            return View(users);

        }

        
        public async Task<IActionResult> Activate(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();


            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Activate(string id, bool IsDeleted)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            user.IsDeleted = IsDeleted;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> ChangeRole(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            var courses = await _context.Courses.Where(x => x.IsDeleted == false).ToListAsync();
            ViewBag.Courses = courses;

            var changeRoleViewModel = new ChangeRoleViewModel
            {
                Username = user.UserName,
                Role = (await _userManager.GetRolesAsync(user))[0],
                Roles = new List<string> { "Admin", "CourseModerator", "User" }
            };

            return View(changeRoleViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeRole(string id,ChangeRoleViewModel changeRoleViewModel, List<int> coursesId,string Role)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var courses = await _context.Courses.Where(x => x.IsDeleted == false).ToListAsync();
            ViewBag.Courses = courses;

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            var dbChangeRoleViewModel = new ChangeRoleViewModel
            {
                Username = user.UserName,
                Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault(),
                Roles = new List<string> { "Admin", "CourseModerator", "User" }
            };

            if (Role.ToLower() == RoleConstants.CourseModeratorRole.ToLower())
            {
                if (coursesId.Count == 0)
                {
                    ModelState.AddModelError("", "Please select category.");
                    return View(dbChangeRoleViewModel);
                }
                foreach (var courseId in coursesId)
                {
                    var dbCourses = await _context.Courses.Where(x => x.IsDeleted == false && x.Id == courseId)
                        .ToListAsync();
                    if (dbCourses == null)
                        return NotFound();

                    List<Course> courseList = new List<Course>();


                    foreach (var dbCourse in dbCourses)
                    {
                        dbCourse.UserId = user.Id;
                        if (user.Courses != null)
                        {
                            user.Courses.Add(dbCourse);
                        }
                        else
                        {
                            courseList.Add(dbCourse);
                        }
                    }
                    if (user.Courses == null)
                    {
                        user.Courses = courseList;
                    }
                }
            }

            string oldRole = (await _userManager.GetRolesAsync(user))[0];
            await _userManager.RemoveFromRoleAsync(user, oldRole);
            await _userManager.AddToRoleAsync(user, Role);

            return RedirectToAction("Index");
        }
    }
}
