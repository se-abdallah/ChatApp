using System.Security.Claims;

namespace API.Extensions
{
 public static class ClaimsPrincipalExtensions
 {
  public static string GetUsername(this ClaimsPrincipal user)
  {
   return user.FindFirst(ClaimTypes.Name)?.Value;
  }
  // public static int GetUserId(this ClaimsPrincipal user)
  // {
  //  return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
  // }

  public static string GetUserId(this ClaimsPrincipal user)
  {
   return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
  }



 }
}


// !neil answer
//  public static string GetUsername(this ClaimsPrincipal user)
//         {
//             return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//         }
// This should be:

//         public static string GetUsername(this ClaimsPrincipal user)
//         {
//             return user.FindFirst(ClaimTypes.Name)?.Value;
//         }


// !neil repo and current code
// public static string GetUsername(this ClaimsPrincipal user)
//         {
//             return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//         }

// ..
//  public static string GetUsername(this ClaimsPrincipal user)
//         {
//             return user.FindFirst(ClaimTypes.Name)?.Value;
//         }

//         public static string GetUserId(this ClaimsPrincipal user)
//         {
//             return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//         }