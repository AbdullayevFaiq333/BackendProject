using BackendProject.Data;
using BackendProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.DAL
{
    public class DataInitializer
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public DataInitializer(AppDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;


        }

        public async Task SeedDataAsync()
        {
            await _context.Database.MigrateAsync();

            #region Role seed

            var roles = new List<string>
            {
                RoleConstants.AdminRole,
                RoleConstants.CourseModeratorRole,
                RoleConstants.UserRole
            };

            foreach (var role in roles)
            {
                if (await _roleManager.RoleExistsAsync(role))
                    continue;


                await _roleManager.CreateAsync(new IdentityRole(role));
            }

            #endregion

            #region User seed

            var user = new User
            {
                Email = "admin@code.az",
                UserName = "Admin",
                Fullname = "Admin Admin"
            };
            if (await _userManager.FindByEmailAsync(user.Email) == null)
            {
                await _userManager.CreateAsync(user, "Admin@123");
                await _userManager.AddToRoleAsync(user, RoleConstants.AdminRole);
            }

            #endregion
        }
    }
}
