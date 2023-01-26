using API.DTOs;
using API.Entity;
<<<<<<< HEAD
using API.Helpers;
=======
>>>>>>> 929b681754124ca02c81088fac73c6ff8f2352f6

namespace API.Interfaces
{
 // the idea of this repository system that providing method that going to support for different entities.
 public interface IUserRepository
 {
  //update the profile //update is not an async method ,it just going to update the tracking status in entity 
  void Update(AppUser user);

  Task<bool> SaveAllAsync();

  Task<IEnumerable<AppUser>> GetUsersAsync();

  Task<AppUser> GetUserByIdAsync(int id);

  Task<AppUser> GetUserByUsernameAsync(string username);
<<<<<<< HEAD
  Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams); 
=======
  Task<IEnumerable<MemberDto>> GetMembersAsync();
>>>>>>> 929b681754124ca02c81088fac73c6ff8f2352f6
  Task<MemberDto> GetMemberAsync(string username);
 }
}