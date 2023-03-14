using API.DTOs;
using API.Entity;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
  public IPhotoService _photoService;

  public UsersController(IUserRepository userRepository, IMapper mapper, IPhotoService photoService)
  {
   _photoService = photoService;
   _mapper = mapper;
   _userRepository = userRepository;

  }

  // !GetUsers
  //api/users
  [HttpGet]
  // [AllowAnonymous]
  // public async Task<ActionResult<PagedList<MemberDto>>> GetUsers([FromQuery] UserParams userParams)
  // {
  //  var users = await _userRepository.GetMembersAsync(userParams);
  //  Response.AddPaginationHeader(new PaginationHeader(users.CurrentPage , users.PageSize, users.TotalCount, users.TotalPages));

  //  return Ok(users);
  // }
  // !GetUsers
  //api/users
  [HttpGet]
  // [AllowAnonymous]
  public async Task<ActionResult<PagedList<MemberDto>>> GetUsers([FromQuery] UserParams userParams)
  {
   var currentUser = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
   // if (currentUser == null)
   // {
   userParams.CurrentUsername = currentUser.UserName;
   // }
   // else
   // {
   //  return BadRequest("user not found");
   // }
   if (string.IsNullOrEmpty(userParams.Gender))
   {
    userParams.Gender = currentUser.Gender == "male" ? "Female" : "male";
   }

   var users = await _userRepository.GetMembersAsync(userParams);
   Response.AddPaginationHeader(new PaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages));
   return Ok(users);
  }

  // !GetUser
  //api/users/1 or /2 etc
  // [Authorize]
  [HttpGet("{username}")]
  public async Task<ActionResult<MemberDto>> GetUser(string username)
  {
   return await _userRepository.GetMemberAsync(username);
  }

  // !UpdateUser
  [HttpPut]
  public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
  {
   var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

   if (user == null) return NotFound();
   _mapper.Map(memberUpdateDto, user);

   if (await _userRepository.SaveAllAsync()) return NoContent();

   return BadRequest("Failed to update");
  }

  // !AddPhoto
  [HttpPost("add-photo")]
  // [Obsolete]
  public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
  {
   var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
   if (user == null) return NotFound();
   var result = await _photoService.AddPhotoAsync(file);

   if (result.Error != null) return BadRequest(result.Error.Message);
   var photo = new Photo
   {
    Url = result.SecureUrl.AbsoluteUri,
    PublicId = result.PublicId
   };

   if (user.Photos.Count == 0) photo.IsMain = true;
   user.Photos.Add(photo);

   if (await _userRepository.SaveAllAsync())
   {
    return CreatedAtAction(nameof(GetUser), new { username = user.UserName }, _mapper.Map<PhotoDto>(photo));
   }
   return BadRequest("Problem adding photo");
  }

  // !SetMainPhoto
  [HttpPut("set-main-photo/{photoId}")]
  public async Task<ActionResult> SetMainPhoto(int photoId)
  {
   var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

   if (user == null) return NotFound();
   var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

   if (photo == null) return NotFound();
   if (photo.IsMain) return BadRequest("image is already sets");
   var currentMain = user.Photos.FirstOrDefault(x => x.IsMain);

   if (currentMain != null)
    currentMain.IsMain = false;
   photo.IsMain = true;

   if (await _userRepository.SaveAllAsync()) return NoContent();
   return BadRequest("Problem settings the Main profile Picture");

  }

  // !DeletePhoto
  [HttpDelete("delete-photo/{photoId}")]
  public async Task<ActionResult> DeletePhoto(int photoId)
  {
   var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

   var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

   if (photo == null) return NotFound();

   if (photo.IsMain) return BadRequest("Can't Delete Your current Profile Picture");

   if (photo.PublicId != null)
   {
    var result = await _photoService.DeletePhotoAsync(photo.PublicId);
    if (result.Error != null) return BadRequest(result.Error.Message);
   }

   user.Photos.Remove(photo);

   if (await _userRepository.SaveAllAsync()) return Ok();

   return BadRequest("Error Deleting picture");
  }
 }
}

// [HttpDelete("delete-photo/{photoId}")]
// public async Task<ActionResult> DeletePhoto(int photoId)
// {

//  var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
//  // if (user == null) return NotFound();

//  var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);
//  if (photo == null)
//   return NotFound();

//  if (photo.IsMain)
//   return BadRequest("Can't Delete Your current Profile Picture");

//  if (photo.PublicId != null)
//  {
//   var result = await _photoService.DeletePhotoAsync(photo.PublicId);
//   if (result.Error != null)
//    return BadRequest(result.Error.Message);
//  }
//  user.Photos.Remove(photo);
//  if (await _userRepository.SaveAllAsync())
//   return Ok();
//  return BadRequest("Error Deleting picture ");
// }

