using BackendProject.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Detail(int? id)
        {

            if (id == null)
                return NotFound();
            var blogDeatil = await _context.BlogDetails.Include(x => x.Blog).FirstOrDefaultAsync(x => x.BlogId == id);
            if (blogDeatil == null)
                return NotFound();
            
            return View(blogDeatil);
        }
    }
}
