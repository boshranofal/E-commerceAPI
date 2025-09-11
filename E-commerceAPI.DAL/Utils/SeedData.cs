using E_commerceAPI.DAL.Model;
using KAStore.DAL.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceAPI.DAL.Utils
{
    public class SeedData : ISeedData
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public SeedData(
            ApplicationDbContext context,
            RoleManager<IdentityRole>roleManager,
            UserManager<ApplicationUser>userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task DataSeeding()
        {
            if ((await _context.Database.GetPendingMigrationsAsync()).Any())
            {
                await _context.Database.MigrateAsync();
            }
            if (!await _context.Categories.AnyAsync())
            {
                await _context.Categories.AddRangeAsync(
                    new Category {  Name = "Electronics" },
                    new Category {  Name = "Clothing" }
                    );
            }
            if(!await _context.Brands.AnyAsync())
            {
                    await _context.Brands.AddRangeAsync(
                        new Brand { Name ="Nick",ImageMain= "Nike-Logo.png" },
                        new Brand {  Name = "Apple",ImageMain= "apple.png" },
                        new Brand {  Name = "Sumssung",ImageMain= "Samsung_logo_blue.png" }
                );
            }
            await _context.SaveChangesAsync();
        }

        public async Task IdentityDataSeddingAsyn()
        {
            if (!await _roleManager.Roles.AnyAsync())
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("Customer"));
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
            }
            if (!await _userManager.Users.AnyAsync())
            {
                var user1 = new ApplicationUser()
                {
                    Email = "BoshraNofal@gmail.com",
                    FullName = "Boshra Sami Nofal",
                    PhoneNumber = "038487462",
                    UserName = "boshra",
                    EmailConfirmed = true
                };
                var user2 = new ApplicationUser()
                {
                    Email = "Ahmad@gmail.com",
                    FullName = "Ahmad Nofal",
                    PhoneNumber = "038487469",
                    UserName = "ANofal1",
                    EmailConfirmed = true

                };
                var user3 = new ApplicationUser()
                {
                    Email = "Ali@gmail.com",
                    FullName = "Ali Nofal",
                    PhoneNumber = "038487467",
                    UserName = "ANofal",
                    EmailConfirmed = true
                };
                await _userManager.CreateAsync(user1, "Pass@12345");
                await _userManager.CreateAsync(user2, "Pass@12345");
                await _userManager.CreateAsync(user3, "Pass@12345");

                await _userManager.AddToRoleAsync(user1, "Admin");
                await _userManager.AddToRoleAsync(user2, "SuperAdmin");
                await _userManager.AddToRoleAsync(user3, "Customer");


            }
            await _context.SaveChangesAsync();
        }
    }
}
