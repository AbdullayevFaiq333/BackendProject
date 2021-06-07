using BackendProject.DAL;
using BackendProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.ViewComponents
{
    public class NoticeViewComponent:ViewComponent
    {
        private readonly AppDbContext _context;

        public NoticeViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var noticeBoards = await _context.NoticeBoards.ToListAsync();
            var noticeVideo = await _context.NoticeVideo.FirstOrDefaultAsync();

            var noticeViewModel = new NoticeViewModel
            {
                NoticeBoards=noticeBoards,
                NoticeVideo=noticeVideo
            };

            return View(noticeViewModel);
        }
    }
}
