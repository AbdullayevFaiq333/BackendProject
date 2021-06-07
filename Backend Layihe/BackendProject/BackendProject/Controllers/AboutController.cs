using BackendProject.DAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Controllers
{
    public class AboutController : Controller
    {
        private readonly AppDbContext _cotext;

        public AboutController(AppDbContext cotext)
        {
            _cotext = cotext;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
