using System.Text.Json;
using API.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
 public class Seed
 {
  public static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
  {
   if (await userManager.Users.AnyAsync()) return;
   var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");
   var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
   var users = JsonSerializer.Deserialize<List<AppUser>>(userData);

   var roles = new List<AppRole>
   {
    new AppRole{Name = "Member"},
    new AppRole{Name = "Admin"},
    new AppRole{Name = "Moderator"}
   };
   //add the role to database
   foreach (var role in roles)
   {
    await roleManager.CreateAsync(role);

   }
   // !Dotnet 6 using it without double check if the user exsists
   // if (users == null) return;
   foreach (var user in users)
   {
    user.Photos.First().IsApproved = true;
    user.UserName = user.UserName.ToLower();
    await userManager.CreateAsync(user, "Pa$$w0rd");
    await userManager.AddToRoleAsync(user, "Member");
   }

   var admin = new AppUser
   {
    UserName = "admin"
   };

   await userManager.CreateAsync(admin, "AdminPa$$w0rd");
   await userManager.AddToRolesAsync(admin, new[] { "Admin", "Moderator" });

  }
 }
}