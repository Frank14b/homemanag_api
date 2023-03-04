
using API.AccessDTOs;
using API.DTOs.Business;
using API.DTOs.Roles;
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
            CreateMap<AppUser, ResultAllUserDto>();
            CreateMap<AppUser, ResultloginDto>();
            CreateMap<AppAcces, AccessResultDto>();
            CreateMap<AppAcces, AccessListResultDto>();
            CreateMap<CreateAccessDto, AppAcces>();
            // CreateMap<CreateRolesDto, AppRole>();
            // CreateMap<UpdateRolesDto, AppRole>();
            CreateMap<AppRole, RoleResultDtos>();
            // CreateMap<DeleteRolesDto, AppRole>();
            // CreateMap<DeleteAccessDto, AppAcces>();
            CreateMap<CreateBusinessDto, AppBusiness>();
            CreateMap<AppBusiness, BusinessResultDtos>();
        }
    }
}