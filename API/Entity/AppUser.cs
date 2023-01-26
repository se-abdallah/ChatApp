namespace API.Entity
{
 public class AppUser
 {
  public int Id { get; set; }
  public string UserName { get; set; }
  public byte[] PasswordHash { get; set; }
  public byte[] PasswordSalt { get; set; }
<<<<<<< HEAD
  public DateOnly DateOfBirth { get; set; }
  public string KnownAs { get; set; }
  public DateTime Created { get; set; } 
  public DateTime LastSeen { get; set; } = DateTime.UtcNow;
  public string Gender { get; set; }
  public string Introduction { get; set; }
  public string JobTitle { get; set; }
=======
  public DateTime DateOfBirth { get; set; }
  public string KnownAs { get; set; }
  public DateTime Created { get; set; } = DateTime.Now;
  public DateTime LastSeen { get; set; } = DateTime.Now;
  public string Gender { get; set; }
  public string Introduction { get; set; }
>>>>>>> 929b681754124ca02c81088fac73c6ff8f2352f6
  public string LookingFor { get; set; }
  public string Interests { get; set; }
  public string City { get; set; }
  public string Country { get; set; }
<<<<<<< HEAD
  public List<Photo> Photos { get; set; } = new();
=======
  public ICollection<Photo> Photos { get; set; }
>>>>>>> 929b681754124ca02c81088fac73c6ff8f2352f6



  // public int GetAge()
  // {
  //  return DateOfBirth.CalculateAge();
  // }
 }
}