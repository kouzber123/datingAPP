using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace API.Helpers
{
  public class AutoMappersProfiles : Profile
  {
    public AutoMappersProfiles()
    {
      CreateMap<AppUser, MemberDTO>() //we want to go appuser to memberDTO
      .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url)) //individual mapping when mapper doesnt understand
      .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
      CreateMap<Photo, PhotoDto>(); // we want to go photo to photo dto
    }
  }
}