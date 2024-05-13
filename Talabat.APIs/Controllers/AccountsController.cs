using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Extensions;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services;

namespace Talabat.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;


        public AccountsController(UserManager<AppUser> userManager ,
            SignInManager<AppUser> signInManager , ITokenService tokenService ,IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if(CheckEmailIsExist(registerDto.Email).Result.Value)
            {
                return BadRequest(new ApiResponse(400, "Email is Already Exist"));
            }
            var user = new AppUser()
            {
               UserName = registerDto.Email.Split("@")[0],
               Email = registerDto.Email,
               PhoneNumber = registerDto.PhoneNumber,
               DisplayName = registerDto.DisplayName,
            };

            var Result = await _userManager.CreateAsync(user,registerDto.Password);
            if (!Result.Succeeded) return BadRequest(new ApiResponse(400));

            var ReturnedUser = new UserDto()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                Token = await _tokenService.CreateToken(user,_userManager)
            };
            return Ok(ReturnedUser);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var User = await _userManager.FindByEmailAsync(loginDto.Email);
            if (User == null) return Unauthorized(new ApiResponse(401));
           var Result =  await _signInManager.CheckPasswordSignInAsync(User, loginDto.Password, false);
            if (!Result.Succeeded) return Unauthorized(new ApiResponse(401));
            return Ok(new UserDto() {
                DisplayName = User.DisplayName,
                Email = User.Email,
                Token = await _tokenService.CreateToken(User, _userManager)
            });
        }

        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
            var ReturnedObject = new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateToken(user, _userManager)
            };
            return Ok(ReturnedObject);
        }

        [Authorize]
        [HttpGet("GetAddress")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var user = await _userManager.FindUserAddressAsync(User);
            var mappedAddress = _mapper.Map<Address,AddressDto>(user.Address);
            return Ok(mappedAddress);
            
        }
        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto updatedAddress)
        {
            // 1- Retrive the Record From Database
            var user = await _userManager.FindUserAddressAsync(User);
            // Mapped The AddreesDto to Address
            var mappedAddress = _mapper.Map<AddressDto,Address>(updatedAddress);
            //2- Update based on id
            mappedAddress.Id = user.Address.Id;
            user.Address = mappedAddress;
            var Result = await _userManager.UpdateAsync(user);
            if (!Result.Succeeded) return BadRequest(new ApiResponse(400));
            // Send again the updated address
            return Ok(updatedAddress);
        }

        [HttpGet("EmailIsExist")]
        public async Task<ActionResult<bool>> CheckEmailIsExist(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null; //return true or false
        }
    }
}
