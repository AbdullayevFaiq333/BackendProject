using BackendProject.Areas.AdminPanel.Utils;
using BackendProject.DAL;
using BackendProject.Models;
using BackendProject.Utils;
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
    //[Authorize(Roles = RoleConstants.AdminRole)]
    public class EventController : Controller
    {
        private readonly AppDbContext _context;

        public EventController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var events = _context.Events.Where(x => x.IsDelete == false).OrderByDescending(x => x.Id).ToList();
            return View(events);
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
                return NotFound();

            var eventt = await _context.Events.Where(x => x.IsDelete == false).Include(x => x.EventDetail)
                                                .ThenInclude(x => x.EventDetailSpeakers).ThenInclude(x => x.Speaker)
                                                .FirstOrDefaultAsync(x => x.Id == id);

            if (eventt == null)
                return NotFound();

            return View(eventt);
        }

        public IActionResult Create()
        {
            var categories = _context.Categories.Where(x => x.IsDeleted == false).ToList();
            ViewBag.Categories = categories;
            return View();
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event eventt, int[] categoryId)
        {
            var categories = _context.Categories.Where(x => x.IsDeleted == false).ToList();
            ViewBag.Categories = categories;

            if (eventt.Photo == null)
            {
                ModelState.AddModelError("Photo", "Photo cannot be empty");
                return View();
            }
            if (!eventt.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "You must choose only Image");
                return View();
            }
            if (!eventt.Photo.IsSizeAllowed(2048))
            {
                ModelState.AddModelError("Photo", "Image size can be 2 MB");
                return View();
            }

            if (!ModelState.IsValid)
            {
                return View();
            }
            
            if (categoryId.Length == 0)
            {
                ModelState.AddModelError("", "Please select category.");
                return View(eventt);
            }

            var eventCategoryList = new List<EventCategory>();
            foreach (var item in categoryId)
            {
                var eventCategory = new EventCategory
                {
                    CategoryId = item,
                    EventId = eventt.Id
                };
                eventCategoryList.Add(eventCategory);
            }
            eventt.EventCategories = eventCategoryList;

            var eventImg = Path.Combine(Constants.ImageFolderPath, "event");
            var fileName = await FileUtil.GenerateFileAsync(eventImg, eventt.Photo);
            eventt.Image = fileName;
            

            await _context.AddRangeAsync(eventt, eventt.EventDetail);
            await _context.SaveChangesAsync();

            List<Subcribe> subcribes = _context.Subcribes.ToList();
            string subject = "Create event";
            string url = "https://localhost:44302/Event/Detail/" + eventt.Id;
            string message = $"<a href={url}>yeni event yarandi baxmaq ucun click edin</a>";
            foreach (Subcribe sub in subcribes)
            {
                await Helper.SendMessage(subject, message, sub.Email);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int? id)
        {

            if (id == null)
                return NotFound();

            var eventt = await _context.Events.Where(x => x.IsDelete == false).Include(x => x.EventDetail)
                                                .ThenInclude(x => x.EventDetailSpeakers).ThenInclude(x => x.Speaker)
                                                .FirstOrDefaultAsync(x => x.Id == id);

            if (eventt == null)
                return NotFound();


            return View(eventt);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(int? id, Event eventt)
        {
            

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id == null)
                return NotFound();

            if (id != eventt.Id)
                return BadRequest();

            var dbEventt = await _context.Events.Where(x => x.IsDelete == false).Include(x => x.EventDetail)
                                                .ThenInclude(x => x.EventDetailSpeakers).ThenInclude(x => x.Speaker)
                                                .FirstOrDefaultAsync(x => x.Id == id);

            if (dbEventt == null)
                return NotFound();

            if (eventt.Photo != null)
            {
                var path = Path.Combine(Constants.ImageFolderPath, "event", dbEventt.Image);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                var eventImg = Path.Combine(Constants.ImageFolderPath, "event");
                var fileName = await FileUtil.GenerateFileAsync(eventImg, eventt.Photo);

                if (eventt.Photo == null)
                {
                    ModelState.AddModelError("Photo", "Select photo.");
                    return View();
                }

                if (!eventt.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Select photo.");
                    return View();
                }

                if (!eventt.Photo.IsSizeAllowed(2048))
                {
                    ModelState.AddModelError("Photo", "Max size is 2 MB.");
                    return View();
                }
                dbEventt.Image = fileName;
            }
            dbEventt.ImageTime = eventt.ImageTime;
            dbEventt.TimeStart = eventt.TimeStart;
            dbEventt.TimeEnd = eventt.TimeEnd;
            dbEventt.Title = eventt.Title;
            dbEventt.Venue = eventt.Venue;
            dbEventt.EventDetail.Description = eventt.EventDetail.Description;
            dbEventt.EventDetail.LeaveAReply = eventt.EventDetail.LeaveAReply;
            
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var eventt = await _context.Events.Where(x => x.IsDelete == false).Include(x => x.EventDetail)
                                                .ThenInclude(x => x.EventDetailSpeakers).ThenInclude(x => x.Speaker)
                                                .FirstOrDefaultAsync(x => x.Id == id);

            if (eventt == null)
                return NotFound();

            return View(eventt);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]

        public async Task<IActionResult> DeleteEvent(int? id)
        {
            if (id == null)
                return NotFound();

            var eventt = await _context.Events.Where(x => x.IsDelete == false).Include(x => x.EventDetail)
                                                .ThenInclude(x => x.EventDetailSpeakers).ThenInclude(x => x.Speaker)
                                                .FirstOrDefaultAsync(x => x.Id == id);

            if (eventt == null)
                return NotFound();



            eventt.IsDelete = true;
            eventt.EventDetail.IsDelete = true;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
