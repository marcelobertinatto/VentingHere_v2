using AutoMapper;
using VentingHere.Domain.Entities;
using VentingHere.ModelView;

namespace VentingHere.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserLoginDTO>().ReverseMap();
        }
    }
}
