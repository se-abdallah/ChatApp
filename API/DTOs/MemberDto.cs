namespace API.DTOs
{
 public class MemberDto
 {
  public int Id { get; set; }
<<<<<<< HEAD
  public string UserName { get; set; }
=======
  public string Username { get; set; }
>>>>>>> 929b681754124ca02c81088fac73c6ff8f2352f6
  public string PhotoUrl { get; set; }
  public int Age { get; set; }
  public string KnownAs { get; set; }
  public DateTime Created { get; set; }
  public DateTime LastSeen { get; set; }
  public string Gender { get; set; }
  public string Introduction { get; set; }
  public string LookingFor { get; set; }
<<<<<<< HEAD
  public string JobTitle { get; set; }  
=======
>>>>>>> 929b681754124ca02c81088fac73c6ff8f2352f6
  public string Interests { get; set; }
  public string City { get; set; }
  public string Country { get; set; }
  public ICollection<PhotoDto> Photos { get; set; }
 }
}