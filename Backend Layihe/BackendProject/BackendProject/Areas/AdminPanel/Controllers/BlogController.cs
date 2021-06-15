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
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var blogs = _context.Blogs.Where(x => x.IsDeleted == false).OrderByDescending(x => x.Id).ToList();
            return View(blogs);
        }

        public IActionResult Detail(int? id)
        {
            if (id == null)
                return NotFound();

            var blog = _context.Blogs.Where(x => x.IsDeleted == false).Include(x => x.BlogDetail).FirstOrDefault(x => x.Id == id);

            if (blog == null)
                return NotFound();

            return View(blog);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blog blog)
        {
            if (blog.Photo == null)
            {
                ModelState.AddModelError("Photo", "Photo cannot be empty");
                return View();
            }
            if (!blog.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "You must choose only Image");
                return View();
            }
            if (!blog.Photo.IsSizeAllowed(2048))
            {
                ModelState.AddModelError("Photo", "Image size can be 2 MB");
                return View();
            }

            if (!ModelState.IsValid)
            {
                return View();
            }
            var blogImg = Path.Combine(Constants.ImageFolderPath, "blog");
            var fileName = await FileUtil.GenerateFileAsync(blogImg, blog.Photo);
            blog.Image = fileName;

            var isExist = await _context.Courses.AnyAsync(x => x.IsDeleted == false && x.Name.ToLower() == blog.Name.ToLower());

            if (isExist)
            {
                ModelState.AddModelError("Name", "This name is available");
                return View();
            }

            await _context.AddRangeAsync(blog, blog.BlogDetail);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        public IActionResult Update(int? id)
        {

            if (id == null)
                return NotFound();

            var blog = _context.Blogs
                .Include(x => x.BlogDetail).Where(y => y.IsDeleted == false)
                .FirstOrDefault(x => x.Id == id);
            if (blog == null)
                return NotFound();

            return View(blog);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Blog blog)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id == null)
                return NotFound();

            if (id != blog.Id)
                return BadRequest();

            var dbBlog =await _context.Blogs
                .Include(x => x.BlogDetail).Where(y => y.IsDeleted == false)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (dbBlog == null)
                return NotFound();


            var isExist = await _context.Blogs.Where(x => x.IsDeleted == false)
                                                .AnyAsync(x => x.Name == blog.Name && x.Id != blog.Id);

            if (isExist)
            {
                ModelState.AddModelError("Name", "This blog name already exist.");
                return View();
            }




            if (blog.Image != null)
            {
                var path = Path.Combine(Constants.ImageFolderPath, "blog", dbBlog.Image);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                var courseImg = Path.Combine(Constants.ImageFolderPath, "blog");
                var fileName = await FileUtil.GenerateFileAsync(courseImg, blog.Photo);

                if (blog.Photo == null)
                {
                    ModelState.AddModelError("Photo", "Select photo.");
                    return View();
                }

                if (!blog.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Select photo.");
                    return View();
                }

                if (!blog.Photo.IsSizeAllowed(2048))
                {
                    ModelState.AddModelError("Photo", "Max size is 2 MB.");
                    return View();
                }
                blog.Image = fileName;
            }


            dbBlog.Name = blog.Name;
            dbBlog.Time = blog.Time;
            dbBlog.MessageCount = blog.MessageCount;
            dbBlog.MainDescription = blog.MainDescription;
            dbBlog.BlogDetail.Description1 = blog.BlogDetail.Description1;
            dbBlog.BlogDetail.Description2 = blog.BlogDetail.Description2;
            dbBlog.BlogDetail.Description3 = blog.BlogDetail.Description3;
            dbBlog.BlogDetail.Description4 = blog.BlogDetail.Description4;
            dbBlog.BlogDetail.Title = blog.BlogDetail.Title;
            dbBlog.BlogDetail.LastDescription = blog.BlogDetail.LastDescription;
            

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();
           
            var blog = _context.Blogs
                .Include(x => x.BlogDetail).Where(y => y.IsDeleted == false)
                .FirstOrDefault(x => x.Id == id);
            
            if (blog == null)
                return NotFound();

            return View(blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]

        public async Task<IActionResult> DeleteBlog(int? id)
        {
            if (id == null)
                return NotFound();

            var blog = _context.Blogs
                .Include(x => x.BlogDetail).Where(y => y.IsDeleted == false)
                .FirstOrDefault(x => x.Id == id);

            if (blog == null)
                return NotFound();

            blog.IsDeleted = true;
            blog.BlogDetail.IsDeleted = true;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    }
}
