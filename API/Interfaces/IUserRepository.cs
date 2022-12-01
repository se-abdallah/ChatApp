using API.DTOs;
using API.Entity;

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
  Task<IEnumerable<MemberDto>> GetMembersAsync();
  Task<MemberDto> GetMemberAsync(string username);
 }
}