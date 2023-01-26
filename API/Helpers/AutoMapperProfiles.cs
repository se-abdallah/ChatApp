using API.DTOs;
using API.Entity;
using API.Extensions;
using AutoMapper;

namespace API.Helpers
{
 public class AutoMapperProfiles : Profile
 {
  public AutoMapperProfiles()
  {
   CreateMap<AppUser, MemberDto>()
   .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url))
<<<<<<< HEAD
   .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalcuateAge()));
   CreateMap<Photo, PhotoDto>();
   CreateMap<MemberUpdateDto, AppUser>();
   CreateMap<RegisterDto, AppUser>();
=======
   .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
   CreateMap<Photo, PhotoDto>();
>>>>>>> 929b681754124ca02c81088fac73c6ff8f2352f6
  }
 }
}