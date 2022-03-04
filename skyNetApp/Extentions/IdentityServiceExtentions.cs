using Core.Entities.Identity;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace skyNetApp.Extentions
{
    public  static class IdentityServiceExtentions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services ,IConfiguration config )
        { 
             //add and configures the identity system for specific user type
            //builder of type identity builder
            var builder = services.AddIdentityCore<AppUser>();
            builder = new IdentityBuilder(builder.UserType, builder.Services);

            //allow usermanager to work with identity database
            builder.AddEntityFrameworkStores<AppIdentityDbContext>();
            //sigining manager work with it
            builder.AddSignInManager<SignInManager<AppUser>>();
            //handle reciving the token
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt=> {

                //handle tooken validation params
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    //if we didnt use validatinissurekeyy any anonomus token will be pass, so user can send anyyy token
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"])),
                    ValidateIssuer = false,
                    //ValidIssuer=config["Token:Issuer"],
                   ValidateAudience=false,

                };
            
            });
            return services;




        }



    }
}
