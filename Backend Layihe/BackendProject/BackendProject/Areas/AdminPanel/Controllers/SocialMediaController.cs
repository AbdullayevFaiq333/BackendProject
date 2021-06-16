using BackendProject.DAL;
using BackendProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    //[Authorize(Roles = RoleConstants.AdminRole)]
    public class SocialMediaController : Controller
    {
        private readonly AppDbContext _context;

        public SocialMediaController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var socialMedias = _context.SocialMedias.Where(x => x.IsDeleted == false).Include(x => x.Teacher).ToList();
            return View(socialMedias);
        }

        public IActionResult Create()
        {
            var teachers = _context.Teachers.Where(x => x.IsDeleted == false).ToList();
            ViewBag.Teacher = teachers;
            ViewBag.SocialMedias = _context.AllSocialMedias.ToList(); 
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SocialMedia socialMedia, int TeacherId)
        {
            var teachers = _context.Teachers.Where(x => x.IsDeleted == false).ToList();
            ViewBag.Teacher = teachers;
            ViewBag.SocialMedias = _context.AllSocialMedias.ToList(); 


            if (!ModelState.IsValid)
            {
                return View();
            }


            socialMedia.TeacherId = TeacherId;

            await _context.AddRangeAsync(socialMedia);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int? id)
        {

            var teachers = _context.Teachers.Where(x => x.IsDeleted == false).ToList();
            ViewBag.Teacher = teachers;

            if (id == null)
                return NotFound();

            var socialMedia = await _context.SocialMedias.Where(x => x.IsDeleted == false).Include(x => x.Teacher)
                                                         .FirstOrDefaultAsync(x => x.Id == id);


            if (socialMedia == null)
                return NotFound();


            return View(socialMedia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(int? id, SocialMedia socialMedia, int? TeacherId)
        {
            var teachers = _context.Teachers.Where(x => x.IsDeleted == false).ToList();
            ViewBag.Teacher = teachers;

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id == null)
                return NotFound();

            if (id != socialMedia.Id)
                return BadRequest();

            var dbSocialMedia = await _context.SocialMedias.Where(x => x.IsDeleted == false).Include(x => x.Teacher)
                                                         .FirstOrDefaultAsync(x => x.Id == id);

            if (dbSocialMedia == null)
                return NotFound();

            dbSocialMedia.Icon = socialMedia.Icon;
            dbSocialMedia.Link = socialMedia.Link;
            dbSocialMedia.TeacherId = (int)TeacherId;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var socialMedia = await _context.SocialMedias.Where(x => x.IsDeleted == false).Include(x => x.Teacher)
                                                         .FirstOrDefaultAsync(x => x.Id == id);


            if (socialMedia == null)
                return NotFound();

            return View(socialMedia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]

        public async Task<IActionResult> DeleteSocialMedia(int? id)
        {
            if (id == null)
                return NotFound();

            var socialMedia = await _context.SocialMedias.Where(x => x.IsDeleted == false).Include(x => x.Teacher)
                                                         .FirstOrDefaultAsync(x => x.Id == id);


            if (socialMedia == null)
                return NotFound();

            socialMedia.IsDeleted = true;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    }
}
