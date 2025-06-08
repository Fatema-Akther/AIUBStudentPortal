using AIUBStudentPortal.DAL.DataContext;
using AIUBStudentPortal.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace AIUBStudentPortal.DAL.Repositories
{
    public class StudentCourseRegistrationRepository : IStudentCourseRegistrationRepository
    {
        private readonly StudentPortalDbContext _context;

        public StudentCourseRegistrationRepository(StudentPortalDbContext context)
        {
            _context = context;
        }

        public bool HasStudentRegistered(int studentId)
        {
            return _context.StudentCourseRegistrations.Any(r => r.StudentId == studentId);
        }

        public void RegisterCourses(int studentId, List<int> courseIds)
        {
            foreach (var courseId in courseIds)
            {
                _context.StudentCourseRegistrations.Add(new StudentCourseRegistration
                {
                    StudentId = studentId,
                    CourseId = courseId,
                    RegistrationDate = DateTime.Now
                });
            }

            _context.SaveChanges();
        }

        public List<Course> GetRegisteredCourses(int studentId)
        {
            return _context.StudentCourseRegistrations
                .Where(r => r.StudentId == studentId)
                .Include(r => r.Course)
                .Select(r => r.Course)
                .ToList();
        }




        public void CreateDropRequest(int studentId, int courseId)
        {
            var dropRequest = new DropApplication
            {
                StudentId = studentId,
                CourseId = courseId,
                RequestDate = DateTime.Now,
                Status = "Pending"
            };

            _context.DropApplications.Add(dropRequest);
            _context.SaveChanges();
        }

    }
}