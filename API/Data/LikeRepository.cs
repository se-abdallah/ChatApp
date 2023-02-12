using API.DTOs;
using API.Entity;
using API.Extensions;
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

  public async Task<IEnumerable<LikeDto>> GetUserLikes(string predicate, int userId)
  {
   var users = _context.Users.OrderBy(u => u.UserName).AsQueryable();
   var likes = _context.Likes.AsQueryable();

   if (predicate == "liked")
   {
    likes = likes.Where(like => like.SourceUserId == userId);
    users = likes.Select(likes => likes.TargetUser);
   }
   if (predicate == "likedBy")
   {
    likes = likes.Where(like => like.TargetUserId == userId);
    users = likes.Select(likes => likes.SourceUser);
   }

   return await users.Select(user => new LikeDto
   {
    UserName = user.UserName,
    KnownAs = user.KnownAs,
    Age = user.DateOfBirth.CalcuateAge(),
    City = user.City,
    PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain).Url,
    Id = user.Id
   }).ToListAsync();
  }

  public async Task<UserLike> GetUserWithLikes(int userId)
  {
   return await _context.Users
              .Include(x => x.LikedUsers)
              .FirstOrDefaultAsync(x => x.Id == userId);



  }
 }
}