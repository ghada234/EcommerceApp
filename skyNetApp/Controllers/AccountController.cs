using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Mvc;
using skyNetApp.Dto;
using skyNetApp.Errors;
using skyNetApp.Extentions;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace skyNetApp.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _usermanager;
        private readonly SignInManager<AppUser> _signinmanager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenservice;

        public AccountController(UserManager<AppUser> usermanager, SignInManager<AppUser> signinmanager, IMapper mapper,ITokenService tokenservice)
        {
            _usermanager = usermanager;
            _signinmanager = signinmanager;
            _mapper = mapper;
            _tokenservice = tokenservice;
            //httpcontext inside constructor equal null so i can't inject it
        }


        //get current user method
        [Authorize]
        [HttpGet]

        public async Task<ActionResult<UserDto>> GetCurrenttUser() {

            //get email of current user
            //var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

            ////get current user from emial
            var user = await _usermanager.FindCurrentUserByEmail(HttpContext.User);


            var UsertoReturn = _mapper.Map<AppUser, UserDto>(user);
            UsertoReturn.Token = _tokenservice.CreateToken(user);

            return UsertoReturn;


        }
        //check email exist method
        [HttpGet("emailexist")]
        public async Task<ActionResult<bool>> IsEmailExist([FromQuery] string email) {

            var Email = await _usermanager.FindByEmailAsync(email);
            if (Email == null) {
                return false;
            }
            return true;
        }

        //gett user include address of current log in user 
        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress() {
            //old method
            //var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            //var user = await _usermanager.FindByEmailAsync(email);
            //error here
            //var user = await _usermanager.Users.Include(x => x.Address).FirstOrDefaultAsync(x => x.Email == email);
            //return Ok(user);
            //using extention metod
          var user=  await _usermanager.GetCurrentUserWithAddress(HttpContext.User);
            return _mapper.Map<Address, AddressDto>(user.Address);

        }
        //updatte address of current user
        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAdress(AddressDto adress) {
            var user = await _usermanager.GetCurrentUserWithAddress(HttpContext.User);

            //from adress dto to address
            user.Address = _mapper.Map<AddressDto, Address>(adress);
        var result=await _usermanager.UpdateAsync(user);
            if (result.Succeeded == true) {
                return Ok(_mapper.Map<Address, AddressDto>(user.Address));
            }
            return BadRequest("sorry !!problem in updating adress");

        
        }



        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto logindto)
        {

            var userFromData = await _usermanager.FindByEmailAsync(logindto.Email);
            if (userFromData == null)
            {
                return Unauthorized(new ApiResponse(401));
            }
            var result = await _signinmanager.CheckPasswordSignInAsync(userFromData, logindto.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized(new ApiResponse(401));
            }
            var UseroReturn = _mapper.Map<AppUser, UserDto>(userFromData);
            UseroReturn.Token = _tokenservice.CreateToken(userFromData);

            return Ok(UseroReturn);


        }

        [HttpPost("register")]

        public async Task<ActionResult<UserDto>> Register(RegisterDto registerdto)
        {
            //we don't use to check if there is emai; in database tthe same the register dto identity take care of it,and weak password also get abad request
            //we will write it to just ta7seeen el user experience to know what is happened exactly

            //becaust it async method we have to use .result.value
            if (  IsEmailExist(registerdto.Email).Result.Value) {
                return new BadRequestObjectResult(new ApiValidaionErrorResponse { Errors=new[] { "email address is already exist"} });
                // ie email is exist
               
            }

            var CreatedUser = new AppUser { Email = registerdto.Email, DisplayName = registerdto.DisplayName, UserName = registerdto.Email };

            var result = await _usermanager.CreateAsync(CreatedUser, registerdto.Password);
            if (!result.Succeeded) {
                return BadRequest(new ApiResponse(400));
            }

            var UseroReturn = _mapper.Map<AppUser, UserDto>(CreatedUser);
            UseroReturn.Token = _tokenservice.CreateToken(CreatedUser);
            return Ok(UseroReturn);

        }


    }
}
