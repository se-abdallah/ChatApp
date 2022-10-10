using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Entity;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
 //[EnableCors("MyCors")]
 //[EnableCors("https://localhost:4200")]
 [ApiController]
 [Route("api/[controller]")]
 public class UsersController : ControllerBase
 {
  private readonly DataContext _context;

  public UsersController(DataContext context)
  {
   _context = context;
  }
  //api/users
  [HttpGet]
  public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
  {
   return await _context.Users.ToListAsync();
  }
  //api/users/1 or /2 etc
  [HttpGet("{id}")]
  public async Task<ActionResult<AppUser>> GetUser(int id)
  {
   return await _context.Users.FindAsync(id);
  }
 }
}