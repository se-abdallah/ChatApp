namespace API.Extensions
{
 public static class DateTimeExtensions
 {
<<<<<<< HEAD
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
=======
  public static int CalculateAge(this DateTime bornDate)
  {
   DateTime today = DateTime.Today;
   var age = today.Year - bornDate.Year;
   if (bornDate.Date > today.AddYears(-age)) age--;
   return age;
  }
 }
}
>>>>>>> 929b681754124ca02c81088fac73c6ff8f2352f6
