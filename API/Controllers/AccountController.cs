using API.DTOs;
using API.Entity;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
 public class AccountController : BaseApiController
 {
  private readonly ITokenService _tokenService;
  private readonly IMapper _mapper;
  private readonly UserManager<AppUser> _userManager;
  public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, IMapper mapper)
  {
   _userManager = userManager;
   _mapper = mapper;
   _tokenService = tokenService;

  }

  // !Register
  [HttpPost("register")]
  public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
  {
   if (await UserExists(registerDto.Username)) return BadRequest("username is taken");
   var user = _mapper.Map<AppUser>(registerDto);

   user.UserName = registerDto.Username.ToLower();

   var result = await _userManager.CreateAsync(user, registerDto.Password);
   if (!result.Succeeded) return BadRequest(result.Errors);

   // *deternine the registerd user in member role
   var roleResult = await _userManager.AddToRoleAsync(user, "member");
   if (!roleResult.Succeeded) return BadRequest(result.Errors);
   return new UserDto
   {
    Username = user.UserName,
    Token = await _tokenService.CreateToken(user),
    KnownAs = user.KnownAs,
    Gender = user.Gender
   };
  }

  // !Login
  [HttpPost("login")]
  public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
  {
   var user = await _userManager.Users.Include(p => p.Photos).SingleOrDefaultAsync(x => x.UserName == loginDto.username);
   //  check if the username is valid
   if (user == null) return Unauthorized("Invalid username");

   //  check if the password is valid
   var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
   if (!result) return Unauthorized("Invalid Password"); //?we can use it without flag to make it more clear and easy to reuse

   return new UserDto
   {
    Username = user.UserName,
    Token = await _tokenService.CreateToken(user),
    PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url,
    KnownAs = user.KnownAs,
    Gender = user.Gender
   };
  }
  private async Task<bool> UserExists(string username)
  {
   return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
  }

 }
}