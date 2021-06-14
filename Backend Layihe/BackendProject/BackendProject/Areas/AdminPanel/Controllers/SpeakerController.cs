using BackendProject.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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


    }
}
