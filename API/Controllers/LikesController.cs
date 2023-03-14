using API.DTOs;
using API.Entity;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
 public class LikesController : BaseApiController
 {
  private readonly IUserRepository _userRepository;
  private readonly ILikeRepository _likeRepository;

  public LikesController(IUserRepository userRepository, ILikeRepository likeRepository)
  {
   _likeRepository = likeRepository;
   _userRepository = userRepository;
  }
  [HttpPost("{username}")]
  public async Task<ActionResult> AddLike(string username)
  {
   var sourceUserId = User.GetUserId();
   var likedUser = await _userRepository.GetUserByUsernameAsync(username);
   var sourceUser = await _likeRepository.GetUserWithLikes(sourceUserId);

   if (likedUser == null) return NotFound();
   if (sourceUser.UserName == username) return BadRequest("Nice Try! You Cannot like yourself");

   var userlike = await _likeRepository.GetUserLike(sourceUserId, likedUser.Id);
   if (userlike != null) return BadRequest("Already Liked");

   userlike = new UserLike
   {
    SourceUserId = sourceUserId,
    TargetUserId = likedUser.Id
   };
   sourceUser.LikedUsers.Add(userlike);
   if (await _userRepository.SaveAllAsync()) return Ok();
   return BadRequest("Faild to like user");



  }

  [HttpGet]
  public async Task<ActionResult<PagedList<LikeDto>>> GetUserLikes([FromQuery] LikesParams likesParams)
  {
   likesParams.UserId = User.GetUserId();
   var users = await _likeRepository.GetUserLikes(likesParams);
   Response.AddPaginationHeader(new PaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages));
   return Ok(users);
  }
 }
}