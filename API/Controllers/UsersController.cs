using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using API.Interfaces;
using API.DTOs;
using AutoMapper;

namespace API.Controllers
{
 [Authorize]
 //[EnableCors("MyCors")]
 //[EnableCors("https://localhost:4200")]
 // [ApiController]
 // [Route("api/[controller]")]
 public class UsersController : BaseApiController
 {

  private readonly IUserRepository _userRepository;
  private readonly IMapper _mapper;

  public UsersController(IUserRepository userRepository, IMapper mapper)
  {
   _mapper = mapper;
   _userRepository = userRepository;

  }
  //api/users
  [HttpGet]
  // [AllowAnonymous]
  public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
  {
   var users = await _userRepository.GetMembersAsync();
   return Ok(users);
  }
  //api/users/1 or /2 etc
  // [Authorize]
  [HttpGet("{username}")]
  public async Task<ActionResult<MemberDto>> GetUser(string username)
  {
   return await _userRepository.GetMemberAsync(username);
  }
 }
}