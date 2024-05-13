using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any()) // For Seed Data once
            {
                var user = new AppUser()
                {
                    DisplayName = "Mohamed ElShahat",
                    Email = "mohamedelshahat582@gmail.com",
                    UserName = "mohamedelshahat",
                    PhoneNumber = "01019301860"
                };
                await userManager.CreateAsync(user,"Pa$$w0rd");
            }
        }
    }
}
