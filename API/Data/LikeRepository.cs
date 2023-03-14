using API.DTOs;
using API.Entity;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
 public class LikeRepository : ILikeRepository
 {
  private readonly DataContext _context;
  private readonly IMapper _mapper;
  public LikeRepository(DataContext context, IMapper mapper)
  {
   _mapper = mapper;
   _context = context;

  }
  public async Task<UserLike> GetUserLike(int sourceUserId, int targetUserId)
  {
   return await _context.Likes.FindAsync(sourceUserId, targetUserId);
  }

  public async Task<PagedList<LikeDto>> GetUserLikes(LikesParams likesParams)
  {
   var users = _context.Users.OrderBy(u => u.UserName).AsQueryable();
   var likes = _context.Likes.AsQueryable();

   if (likesParams.Predicate == "liked")
   {
    likes = likes.Where(like => like.SourceUserId == likesParams.UserId);
    users = likes.Select(likes => likes.TargetUser);
   }
   if (likesParams.Predicate == "likedBy")
   {
    likes = likes.Where(like => like.TargetUserId == likesParams.UserId);
    users = likes.Select(likes => likes.SourceUser);
   }

   var likesUsers = users.Select(user => new LikeDto
   {
    UserName = user.UserName,
    KnownAs = user.KnownAs,
    Age = user.DateOfBirth.CalcuateAge(),
    City = user.City,
    PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain).Url,
    Id = user.Id
   });

   return await PagedList<LikeDto>.CreateAsync(likesUsers, likesParams.PageNumber, likesParams.PageSize);
  }

  public async Task<AppUser> GetUserWithLikes(int userId)
  {
   return await _context.Users
              .Include(x => x.LikedUsers)
              .FirstOrDefaultAsync(x => x.Id == userId);

  }
 }
}