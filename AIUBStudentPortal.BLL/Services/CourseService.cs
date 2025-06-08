using AIUBStudentPortal.BLL.DTO;
using AIUBStudentPortal.DAL.Entities;
using AIUBStudentPortal.DAL.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIUBStudentPortal.BLL.Services
{
    public class CourseService
    {
        private readonly ICourseRepository _courseRepo;
        private readonly IMapper _mapper;

        public CourseService(ICourseRepository courseRepo, IMapper mapper)
        {
            _courseRepo = courseRepo;
            _mapper = mapper;
        }
        /*
         public List<CourseDTO> GetAllCourses()
         {
             var courses = _courseRepo.GetAllCourses();

             // Inline mapping
             var courseDtos = new List<CourseDTO>();
             foreach (var course in courses)
             {
                 var dto = _mapper.Map<CourseDTO>(course);
                 courseDtos.Add(dto);
             }

             return courseDtos;
         }*/


        public List<CourseDTO> GetAllCourses()
        {
            var courses = _courseRepo.GetAllCourses();
            return _mapper.Map<List<CourseDTO>>(courses);
        }


        public void InsertCourse(CourseDTO dto)
        {
            var course = _mapper.Map<Course>(dto);
            _courseRepo.InsertCourse(course);
        }



        public void DeleteCourse(int id)
        {
            _courseRepo.DeleteCourse(id);
        }


    }

}
