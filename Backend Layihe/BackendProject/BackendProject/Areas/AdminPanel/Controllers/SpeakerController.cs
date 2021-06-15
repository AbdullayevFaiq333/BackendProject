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
    public class SpeakerController : Controller
    {
        private readonly AppDbContext _context;

        public SpeakerController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var speakers = _context.Speakers.Where(x => x.IsDelete == false).Include(x => x.EventDetailSpeakers)
                                           .ThenInclude(x => x.EventDetail).ThenInclude(x => x.Event).ToList();


            return View(speakers);
        }

        public IActionResult Create()
        {
            var eventDetails = _context.EventDetail.Where(x => x.IsDelete == false).Include(x=>x.Event).ToList();
            ViewBag.EventDetails = eventDetails;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Speaker speaker,List<int> eventDetailsId)
        {
            var eventDetails = _context.EventDetail.Where(x => x.IsDelete == false).Include(x => x.Event).ToList();
            ViewBag.EventDetails = eventDetails;

            if (speaker.Photo == null)
            {
                ModelState.AddModelError("Photo", "Photo cannot be empty");
                return View();
            }
            if (!speaker.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "You must choose only Image");
                return View();
            }
            if (!speaker.Photo.IsSizeAllowed(2048))
            {
                ModelState.AddModelError("Photo", "Image size can be 2 MB");
                return View();
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            foreach (var item in eventDetailsId)
            {
                if (eventDetails.All(x=>x.Id != item))
                {
                    return NotFound();
                }
            }

            var speakerImg = Path.Combine(Constants.ImageFolderPath, "event");
            var fileName = await FileUtil.GenerateFileAsync(speakerImg, speaker.Photo);
            speaker.Image = fileName;

            var eventDetailSpeakerList = new List<EventDetailSpeaker>();
            foreach (int item in eventDetailsId)
            {
               
                var eventDetailSpeaker = new EventDetailSpeaker
                {
                    EventDetailId = item,
                    SpeakerId = speaker.Id

                };
                eventDetailSpeakerList.Add(eventDetailSpeaker);
            }
            speaker.EventDetailSpeakers = eventDetailSpeakerList;
            speaker.IsDelete = false;

            await _context.Speakers.AddAsync(speaker);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }



        public async Task<IActionResult> Update(int? id)
        {

            var eventDetails = _context.EventDetail.Where(x => x.IsDelete == false).Include(x => x.Event).ToList();
            ViewBag.EventDetails = eventDetails;

            if (id == null)
                return NotFound();

            var speaker =await _context.Speakers.Where(x => x.IsDelete == false).Include(x => x.EventDetailSpeakers)
                                           .ThenInclude(x => x.EventDetail).ThenInclude(x => x.Event)
                                           .FirstOrDefaultAsync(x=>x.Id==id);

            if (speaker == null)
                return NotFound();


            return View(speaker);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(int? id, Speaker speaker,List<int> eventDetailsId)
        {
            var eventDetails = _context.EventDetail.Where(x => x.IsDelete == false).Include(x => x.Event).ToList();
            ViewBag.EventDetails = eventDetails;

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id == null)
                return NotFound();

            if (id != speaker.Id)
                return BadRequest();

            var dbSpeaker = await _context.Speakers.Where(x => x.IsDelete == false).Include(x => x.EventDetailSpeakers)
                                           .ThenInclude(x => x.EventDetail).ThenInclude(x => x.Event)
                                           .FirstOrDefaultAsync(x => x.Id == id);

            if (dbSpeaker == null)
                return NotFound();

            if (speaker.Image != null)
            {
                var path = Path.Combine(Constants.ImageFolderPath, "event", dbSpeaker.Image);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                var speakerImg = Path.Combine(Constants.ImageFolderPath, "event");
                var fileName = await FileUtil.GenerateFileAsync(speakerImg, speaker.Photo);

                if (speaker.Photo == null)
                {
                    ModelState.AddModelError("Photo", "Select photo.");
                    return View();
                }

                if (!speaker.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Select photo.");
                    return View();
                }

                if (!speaker.Photo.IsSizeAllowed(2048))
                {
                    ModelState.AddModelError("Photo", "Max size is 2 MB.");
                    return View();
                }
                speaker.Image = fileName;
            }

            dbSpeaker.FullName = speaker.FullName;

            var eventDetailSpeakerList = new List<EventDetailSpeaker>();
            foreach (int item in eventDetailsId)
            {

                var eventDetailSpeaker = new EventDetailSpeaker
                {
                    EventDetailId = item,
                    SpeakerId = speaker.Id

                };
                eventDetailSpeakerList.Add(eventDetailSpeaker);
            }
            dbSpeaker.EventDetailSpeakers = eventDetailSpeakerList;
            
            
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var speaker = await _context.Speakers.Where(x => x.IsDelete == false).Include(x => x.EventDetailSpeakers)
                                           .ThenInclude(x => x.EventDetail).ThenInclude(x => x.Event)
                                           .FirstOrDefaultAsync(x => x.Id == id);

            if (speaker == null)
                return NotFound();

            return View(speaker);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]

        public async Task<IActionResult> DeleteSpeaker(int? id)
        {
            if (id == null)
                return NotFound();

            var speaker = await _context.Speakers.Where(x => x.IsDelete == false).Include(x => x.EventDetailSpeakers)
                                           .ThenInclude(x => x.EventDetail).ThenInclude(x => x.Event)
                                           .FirstOrDefaultAsync(x => x.Id == id);

            if (speaker == null)
                return NotFound();

            speaker.IsDelete = true;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
