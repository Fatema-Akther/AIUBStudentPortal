using AIUBStudentPortal.DAL.DataContext;
using AIUBStudentPortal.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIUBStudentPortal.DAL.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly StudentPortalDbContext _context;

        public CourseRepository(StudentPortalDbContext context)
        {
            _context = context;
        }

        public List<Course> GetAllCourses()
        {
            return _context.Courses.ToList();
        }

        public Course GetById(int id)
        {
            return _context.Courses.Find(id);
        }

        
        public void InsertCourse(Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
        }


        public void DeleteCourse(int id)
        {
            var course = _context.Courses.FirstOrDefault(c => c.Id == id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                _context.SaveChanges();
            }
        }

    }

}
