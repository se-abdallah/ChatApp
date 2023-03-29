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
   .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalcuateAge()));

   // ! new
   // CreateMap<AppUser, MemberDto>().AfterMap<PagedList<MemberDto>>();
   //    CreateMap<UserParams, AppUser>()
   //         .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
   //         .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.CurrentUsername));
   CreateMap<Photo, PhotoDto>();
   CreateMap<MemberUpdateDto, AppUser>();
   CreateMap<RegisterDto, AppUser>();
   // CreateMap<LikeDto,AppUser>();
   CreateMap<Message, MessageDto>()
   .ForMember(d => d.SenderPhotoUrl, o => o.MapFrom(s => s.Sender.Photos.FirstOrDefault(x => x.IsMain).Url))
   .ForMember(d => d.RecipientPhotoUrl, o => o.MapFrom(s => s.Recipient.Photos.FirstOrDefault(x => x.IsMain).Url));

   CreateMap<DateTime, DateTime>().ConvertUsing(d => DateTime.SpecifyKind(d, DateTimeKind.Utc));
   CreateMap<DateTime?, DateTime?>().ConvertUsing(d => d.HasValue ? DateTime.SpecifyKind(d.Value, DateTimeKind.Utc) : null);
  }
 }
}
// var config = new MapperConfiguration(cfg =>
//     {
//         cfg.CreateMap<AppUser, MemberDto>();
//     });
//     var mapper = config.CreateMapper();
//     var memberDtos = mapper.Map<List<MemberDto>>(appUsers.ToList());


// var currentUser = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
//    userParams.CurrentUsername = currentUser.UserName;

//    if (string.IsNullOrEmpty(userParams.Gender))
//    {
//     userParams.Gender = currentUser.Gender == "male" ? "Female" : "male";
//    }

//    var users = await _userRepository.GetMembersAsync(userParams);
//    Response.AddPaginationHeader(new PaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages));
//    return Ok(users);