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
            CreateMap<Subject, SubjectDTO>().ReverseMap();
            CreateMap<SubjectIssue, SubjectIssueDTO>().ReverseMap();
            CreateMap<Company, CompanyDTO>().ReverseMap();
            CreateMap<CompanyRate, CompanyRateDTO>().ReverseMap();
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
            CreateMap<CompanySubjectTellUsDTO, CompanySubjectIssue>()
                .ForMember(dest => dest.TellUs, opt => opt.MapFrom(src => src.TellUs))
                .ForMember(dest => dest.SubjectId, opt => opt.MapFrom(src => src.SubjectId))
                .ForMember(dest => dest.SubjectIssueId, opt => opt.MapFrom(src => src.SubjectIssueId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
                .ForMember(dest => dest.DateAndTime, opt => opt.MapFrom(src => src.DateAndTime))
                .ReverseMap();
            CreateMap<CompanySubjectTellUsDTO, Subject>()
                .ForMember(dest => dest.SubjectText, opt => opt.MapFrom(src => src.SubjectDescribed))
                .ReverseMap();
            CreateMap<CompanySubjectTellUsDTO, SubjectIssue>()
                .ForMember(dest => dest.SubjectIssueText, opt => opt.MapFrom(src => src.SubjectIssueDescribed))
                .ReverseMap();
            CreateMap<CompanySubjectTellUsDTO, Company>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CompanyName))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.WebSiteAddress, opt => opt.MapFrom(src => src.WebSiteAddress))
                .ReverseMap();
        }
    }
}
