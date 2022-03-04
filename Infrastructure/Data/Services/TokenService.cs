using Core.Entities;
using Core.Entities.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Services
{

    //handle create token
 public   class TokenService:ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config)
        {
            _config = config;
            //tthe key use in decrypt is the same using in encrypt so it symmertic key
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]));

        }

        public string CreateToken(AppUser appuser)
        {
            //token consiaitts of list of claims and here i use email and username
            var claims = new List<Claim> {
               new Claim(ClaimTypes.Email,appuser.Email),
               new Claim(ClaimTypes.GivenName,appuser.DisplayName),
            };
           
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(claims),
                Expires=DateTime.Now.AddDays(7),
                SigningCredentials=creds,
                //issuer is localhost:5001 who is responsible for this token
                //Issuer=_config["Token: Issuer"],

            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
