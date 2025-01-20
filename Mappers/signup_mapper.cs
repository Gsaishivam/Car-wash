using AutoMapper;
using Car_wash.Data.DTO;
using Car_wash.Models;

namespace Car_wash.Mappings
{
    public class Signup_mapper : Profile
    {
        public Signup_mapper()
        {
            // Mapping Signup DTO to Users
            CreateMap<Signup, Users>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => BCrypt.Net.BCrypt.HashPassword(src.Password)))
                .ForMember(dest => dest.RoleID, opt => opt.MapFrom(src => 1)) 
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.LastLogin, opt => opt.MapFrom(src => DateTime.Now));

            // Mapping Signup DTO to Washers
            CreateMap<Signup, Washers>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => BCrypt.Net.BCrypt.HashPassword(src.Password)))
                .ForMember(dest => dest.RoleID, opt => opt.MapFrom(src => 2)) 
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.LastLogin, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
