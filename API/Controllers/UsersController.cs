using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
 //[EnableCors("MyCors")]
 //[EnableCors("https://localhost:4200")]
 // [ApiController]
 // [Route("api/[controller]")]
 public class UsersController : BaseApiController
 {
  private readonly DataContext _context;

  public UsersController(DataContext context)
  {
   _context = context;
  }
  //api/users
  [HttpGet]
  [AllowAnonymous]
  public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
  {
   return await _context.Users.ToListAsync();
  }
  //api/users/1 or /2 etc
  [Authorize]
  [HttpGet("{id}")]
  public async Task<ActionResult<AppUser>> GetUser(int id)
  {
   return await _context.Users.FindAsync(id);
  }
 }
}