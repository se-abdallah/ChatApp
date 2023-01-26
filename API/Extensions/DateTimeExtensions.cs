namespace API.Extensions
{
 public static class DateTimeExtensions
 {
  public static int CalcuateAge(this DateOnly dob)
  {
   var today = DateOnly.FromDateTime(DateTime.UtcNow);

   var age = today.Year - dob.Year;

   if (dob > today.AddYears(-age)) age--;

   return age;

  }
 }
}


// public static int CalculateAge(this DateTime bornDate)
//   {
//    DateTime today = DateTime.Today;
//    var age = today.Year - bornDate.Year;
//    if (bornDate.Date > today.AddYears(-age)) age--;
//    return age;