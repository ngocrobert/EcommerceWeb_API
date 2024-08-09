using Microsoft.AspNetCore.Identity;
using Product.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure.Data.Config
{
    public class IdentitySeed
    {
        public static async Task SeedUserAsync(UserManager<AppUsers> userManager)
        {
            if(!userManager.Users.Any())
            {
                //Create new user
                var user = new AppUsers
                {
                    DisplayName = "NgocRobert",
                    Email = "ngocrobert@gmail.com",
                    UserName = "ngocrobert@gmail.com",
                    Address = new Address
                    {
                        FirstName = "Ngoc",
                        LastName = "Nguyen",
                        City = "HaNoi",
                        State = "VietNam",
                        Street = "HaDong",
                        ZipCode = "123123"
                    }
                };

                await userManager.CreateAsync(user,"Ngoc1801.");
            }
        }
    }
}
