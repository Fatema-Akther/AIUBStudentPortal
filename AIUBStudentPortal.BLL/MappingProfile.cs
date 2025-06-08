using AIUBStudentPortal.BLL.DTO;
using AIUBStudentPortal.DAL.Entities;
using AutoMapper;

namespace AIUBStudentPortal.BLL
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Course mappings
            CreateMap<Course, CourseDTO>();
            CreateMap<CourseDTO, Course>();

            // DropApplication mappings
            CreateMap<DropApplication, DropApplicationDTO>()
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Course.Name))
                .ForMember(dest => dest.CourseSection, opt => opt.MapFrom(src => src.Course.Section));

            CreateMap<DropApplicationDTO, DropApplication>();
        }
    }
}
