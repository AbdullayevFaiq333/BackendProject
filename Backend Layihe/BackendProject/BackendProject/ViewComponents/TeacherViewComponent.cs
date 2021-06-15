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
    public class TeacherViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public TeacherViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int count = 4, int page = 1)
        {
            var teachers = await _context.Teachers.Where(x => x.IsDeleted == false).OrderByDescending(x => x.Id)
                                              .Skip((page - 1) * count).Take(count).Include(y => y.TeacherDetail)
                                              .Include(z => z.SocialMedias).Include(z => z.Position)
                                              .ToListAsync();
            return View(teachers);

           

        }
    }
}
