using AIUBStudentPortal.BLL.DTO;
using AIUBStudentPortal.DAL.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIUBStudentPortal.BLL.Services
{
    public class RegistrationService
    {
        private readonly IStudentCourseRegistrationRepository _registrationRepo;
        private readonly ICourseRepository _courseRepo;
        private readonly IMapper _mapper;

        public RegistrationService(
            IStudentCourseRegistrationRepository registrationRepo,
            ICourseRepository courseRepo,
            IMapper mapper)
        {
            _registrationRepo = registrationRepo;
            _courseRepo = courseRepo;
            _mapper = mapper;
        }

        public bool HasRegistered(int studentId)
        {
            return _registrationRepo.HasStudentRegistered(studentId);
        }

        public void RegisterCourses(StudentCourseRegistrationDTO dto)
        {
            _registrationRepo.RegisterCourses(dto.StudentId, dto.SelectedCourseIds);
        }

        public List<CourseDTO> GetRegisteredCourses(int studentId)
        {
            var courses = _registrationRepo.GetRegisteredCourses(studentId);

            // Inline mapping
            var courseDtos = new List<CourseDTO>();
            foreach (var course in courses)
            {
                var dto = _mapper.Map<CourseDTO>(course);
                courseDtos.Add(dto);
            }

            return courseDtos;
        }



        // ClassSchedule
        public List<DailyClassDTO> GetClassScheduleByDate(int studentId)
        {
            var courses = _registrationRepo.GetRegisteredCourses(studentId);
            var courseDtos = _mapper.Map<List<CourseDTO>>(courses);

            var scheduleByDate = new List<DailyClassDTO>();

            // Prepare 7 dates starting from today
            for (int i = 0; i < 7; i++)
            {
                var date = DateTime.Today.AddDays(i);
                var fullDayName = date.DayOfWeek.ToString(); // "Sunday", "Monday", etc.

                string formattedDate;

                if (date.Date == DateTime.Today)
                {
                    formattedDate = "Today";
                }
                else if (date.Date == DateTime.Today.AddDays(1))
                {
                    formattedDate = "Tomorrow";
                }
                else
                {
                    formattedDate = date.ToString("dd-MM-yyyy");
                }

                var matchingCourses = courseDtos
                    .Where(c =>
                        !string.IsNullOrEmpty(c.Schedule) &&
                        c.Schedule.StartsWith(fullDayName, StringComparison.OrdinalIgnoreCase)
                    )
                    .ToList();

                scheduleByDate.Add(new DailyClassDTO
                {
                    DateLabel = formattedDate,
                    Courses = matchingCourses
                });
            }


            return scheduleByDate;
        }


        // Handle Drop Request
        public void SubmitDropRequest(int studentId, int courseId)
        {
            _registrationRepo.CreateDropRequest(studentId, courseId);
        }

    }
}
