using Core.Entities.Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


//we use this extentions to make the code simple in controller
namespace skyNetApp.Extentions
{
    public static class UserManagerExtention
    {
        public static async Task<AppUser> GetCurrentUserWithAddress (this UserManager<AppUser> usermanager,ClaimsPrincipal user) {

            //claimprinciple==Httpcontext.User
            var email = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            return await usermanager.Users.Include(x => x.Address).SingleOrDefaultAsync(x => x.Email == email);
        
        
        }
        public static async Task<AppUser> FindCurrentUserByEmail(this UserManager<AppUser> usermanager, ClaimsPrincipal user) {
            var email = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            return await usermanager.FindByEmailAsync(email);

        }
    }
}
