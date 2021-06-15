using BackendProject.DAL;
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
        public IActionResult Index(int? page = 1)
        {
            ViewBag.PageCount = Math.Ceiling((decimal)_context.Events.Count() / 6);
            ViewBag.Page = page;
            var events = _context.Events.Where(c => c.IsDelete == false).ToList();

            return View(events);
        }
        public IActionResult Detail(int? id)
        {
            if (id == null)
                return NotFound();

            var eventDetail = _context.EventDetail.Where(x => x.IsDelete == false).Include(x => x.Event).ThenInclude(y => y.EventDetail)
                                                    .Include(t => t.EventDetailSpeakers).ThenInclude(t => t.Speaker)
                                                    .OrderByDescending(t => t.Id).FirstOrDefault(z => z.EventId == id);


            

            if (eventDetail == null)
                return NotFound();

            return View(eventDetail);
        }
    }
}
