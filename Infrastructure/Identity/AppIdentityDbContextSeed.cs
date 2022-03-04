using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {

        public static async Task SeedUsersAsync(UserManager<AppUser> usermanager)
        {
            //we use user manager badl mn context and this get by asp,net core ,identity
            //check if there is no users in dtatbas identity
            if (!usermanager.Users.Any()) {
                var user = new AppUser { 
                    DisplayName = "emad",
                    Email="emad@gmail.com",
                    UserName="emad@gmail.com",
                    Address=new Address { 
                        FirstName="bob",
                        LastName="top",
                        ZipCode="35aa",
                        City="luxor",
                        Street="mystreet",
                    },
                
                };

                await usermanager.CreateAsync(user, "Pa$$w0rd");
            
            
            }
        }
    }
}
