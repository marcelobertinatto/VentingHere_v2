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
            CreateMap<Company, CompanyDTO>().ReverseMap();
            CreateMap<User, UserDetailsDTO>()
                .ForMember(dest => dest.Fullname, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Userimage, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.NewPassword, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ReverseMap();
            CreateMap<UserDetailsDTO, User>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Fullname))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Userimage))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ReverseMap();
        }
    }
}
