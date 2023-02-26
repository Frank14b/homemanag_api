
using API.Entities;
using API.UsersDTOs;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, ResultUpdateUserDto>();
            CreateMap<AppUser, ResultloginDto>();
        }
    }
}