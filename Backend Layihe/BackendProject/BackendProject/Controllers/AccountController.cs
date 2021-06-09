using BackendProject.Data;
using BackendProject.Models;
using BackendProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var existUser = await _userManager.FindByEmailAsync(login.Email);
            if (existUser == null)
            {
                ModelState.AddModelError("", "Email yada Password sefdi");
                return View();
            }

            var loginResult = await _signInManager.PasswordSignInAsync(existUser, login.Password, login.RememberMe, true);
            if (!loginResult.Succeeded)
            {
                ModelState.AddModelError("", "Email yada Password sefdi");
                return View();
            }

            return RedirectToAction("Index", "Home");

        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var dbUser = await _userManager.FindByNameAsync(register.Username);
            if (dbUser != null)
            {
                ModelState.AddModelError("Username", "Bu adda user var");
                return View();
            }

            var newUser = new User
            {
                UserName = register.Username,
                Fullname = register.Fullname,
                Email = register.Email

            };

            var identityResult = await _userManager.CreateAsync(newUser, register.Password);
            if (!identityResult.Succeeded)
            {
                foreach (var eror in identityResult.Errors)
                {
                    ModelState.AddModelError("", eror.Description);
                }
                return View();
            }

            await _userManager.AddToRoleAsync(newUser, RoleConstants.UserRole);
            await _signInManager.SignInAsync(newUser, true);

            return RedirectToAction("Index", "Home");
        }
    }
}
