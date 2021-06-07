using BackendProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }

        public DbSet<BackgroundArea> BackgroundAreas { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<About> About { get; set; }
        public DbSet<CoursesAreaBackground> CoursesAreaBackground { get; set; }

        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseDetail> CourseDetails { get; set; }
        public DbSet<NoticeBoard> NoticeBoards { get; set; }
        public DbSet<NoticeVideo> NoticeVideo { get; set; }
        public DbSet<Testimonial> Testimonial { get; set; }
    }
}
