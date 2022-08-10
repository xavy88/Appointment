﻿using MedicalAppointmentSystem.Models;
using MedicalAppointmentSystem.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MedicalAppointmentSystem.Data.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count()>0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (System.Exception)
            {

                throw;
            }
            if (_db.Roles.Any(x => x.Name == Utility.Helper.Admin)) return;
            {
                _roleManager.CreateAsync(new IdentityRole(Helper.Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(Helper.Doctor)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(Helper.Patient)).GetAwaiter().GetResult();

                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "admin@gmail.com",
                    Email= "admin@gmail.com",
                    EmailConfirmed = true,
                    Name = "Admin Appointment"
                },"AdminPass123*").GetAwaiter().GetResult();

                ApplicationUser user = _db.Users.FirstOrDefault(u=> u.Email == "admin@gmail.com");
                _userManager.AddToRoleAsync(user, Helper.Admin).GetAwaiter().GetResult();

            }
        }
    }
}
