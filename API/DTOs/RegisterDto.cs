using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
 public class RegisterDto
 {
  [Required] public string Username { get; set; }

  [Required] public string KnownAs { get; set; }

  [Required] public string Gender { get; set; }

  [Required] public DateOnly? DateOfBirth { get; set; } //optinal to work required work!
  // [Required] public DateTime DateOfBirth { get; set; } 

  [Required] public string JobTitle { get; set; }

  [Required] public string City { get; set; }

  [Required] public string Country { get; set; }

  [Required]
<<<<<<< HEAD
  [StringLength(8, MinimumLength = 4)]
=======
  public string Username { get; set; }
  [Required]
  [StringLength(8,MinimumLength = 4)]
>>>>>>> 929b681754124ca02c81088fac73c6ff8f2352f6
  public string Password { get; set; }

 }
}