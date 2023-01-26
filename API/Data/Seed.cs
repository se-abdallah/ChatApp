using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Entity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
 public class Seed
 {
  public static async Task SeedUsers(DataContext context)
  {
   if (await context.Users.AnyAsync()) return;
<<<<<<< HEAD
   var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");
   var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
   var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
   // !Dotnet 6 using it without double check if the user exsists
   // if (users == null) return;
=======
   var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
   var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
   //from neil Repository end of section 8
   if (users == null) return;
>>>>>>> 929b681754124ca02c81088fac73c6ff8f2352f6
   foreach (var user in users)
   {
    using var hmac = new HMACSHA512();
    user.UserName = user.UserName.ToLower();
<<<<<<< HEAD
    user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("pa$$w0rd"));
    user.PasswordSalt = hmac.Key;
=======
    user.PasswordSalt = hmac.Key;
    user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("pa$$w0rd"));
    //user.PasswordSalt = hmac.Key;
>>>>>>> 929b681754124ca02c81088fac73c6ff8f2352f6

    await context.Users.AddAsync(user);
   }
   await context.SaveChangesAsync();
  }
 }
}