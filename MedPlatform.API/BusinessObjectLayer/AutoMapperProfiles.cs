using AutoMapper;
using BusinessObjectLayer.Dtos;
using BusinessObjectLayer.Entities;

namespace BusinessObjectLayer
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<SimplifiedUserDto, BaseUserEntity>();
            CreateMap<BaseUserEntity, SimplifiedUserDto>();

            CreateMap<UserUpdateDto, BaseUserEntity>();
            CreateMap<BaseUserEntity, UserUpdateDto>();
        }
    }
}
