using BackendProject.DAL;
using BackendProject.Models;
using BackendProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Controllers
{
    public class EventController : Controller
    {
        private readonly AppDbContext _context;

        public EventController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? categoryId, int? page = 1)
        {
            var events = new List<Event>();

            if (categoryId == null)
            {
                ViewBag.PageCount = Decimal.Ceiling((decimal)_context.Events.Where(x => x.IsDelete == false).Count() / 6);
                ViewBag.Page = page;

                if (ViewBag.PageCount < page || page <= 0)
                    return NotFound();

                return View(events);
            }
            else
            {
                var categoryEvents = _context.EventCategories.Where(x => x.CategoryId == categoryId)
                    .Include(x => x.Event).Where(x => x.Event.IsDelete == false).OrderByDescending(x => x.Event.Id);
                foreach (var categoryEvent in categoryEvents)
                {
                    events.Add(categoryEvent.Event);
                }
                return View(events);
            }
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
                return NotFound();

            var blog = await _context.Blogs.Where(x => x.IsDeleted == false)
                .Include(x => x.BlogDetail).FirstOrDefaultAsync(x => x.Id == id);

            var eventt = _context.EventDetail.Where(x => x.IsDelete == false).Include(x => x.Event).ThenInclude(y => y.EventDetail)
                                                    .Include(t => t.EventDetailSpeakers).ThenInclude(t => t.Speaker)
                                                    .OrderByDescending(t => t.Id).FirstOrDefault(z => z.EventId == id);

            if (eventt == null)
                return NotFound();

            var eventViewModel = new EventViewModel
            {
                Categories = await _context.Categories.Include(x => x.EventCategories.Where(y => y.Event.IsDelete == false)).ThenInclude(x => x.Event).Where(x => x.IsDeleted == false).ToListAsync(),
                Event = eventt.Event
            };


            return View(eventViewModel);
        }
    }
}
