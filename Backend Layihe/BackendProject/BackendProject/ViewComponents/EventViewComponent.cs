using BackendProject.DAL;
using BackendProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.ViewComponents
{
    public class EventViewComponent: ViewComponent
    {
        private readonly AppDbContext _context;

        public EventViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int count = 3, int page = 1)
        {
            
            var events = await _context.Events.Where(x => x.IsDelete == false).OrderByDescending(x => x.Id)
                                              .Skip((page - 1) * count).Take(count).Include(y => y.EventDetail)
                                              .ThenInclude(z => z.EventDetailSpeakers).ThenInclude(z => z.Speaker)
                                              .ToListAsync();
            return View(events);
            
        }
    }
}
