using AutoMapper;
using ps_product_api.Entities;
using ps_product_api.Models;

namespace ps_product_api.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}