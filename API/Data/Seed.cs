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
   var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
   var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
   //from neil Repository end of section 8
   if (users == null) return;
   foreach (var user in users)
   {
    using var hmac = new HMACSHA512();
    user.UserName = user.UserName.ToLower();
    user.PasswordSalt = hmac.Key;
    user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("pa$$w0rd"));
    //user.PasswordSalt = hmac.Key;

    await context.Users.AddAsync(user);
   }
   await context.SaveChangesAsync();
  }
 }
}