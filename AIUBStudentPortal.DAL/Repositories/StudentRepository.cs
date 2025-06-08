using AIUBStudentPortal.DAL.DataContext;
using AIUBStudentPortal.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace AIUBStudentPortal.DAL.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentPortalDbContext _context;

        public StudentRepository()
        {
            _context = new StudentPortalDbContext();
        }

        public void Add(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }

        public IEnumerable<Student> GetAll()
        {
            return _context.Students.ToList();
        }


        public Student GetById(int id) // ✅ Add this
        {
            return _context.Students.FirstOrDefault(s => s.Id == id);
        }



        public void Update(Student student)
        {
            var existingStudent = _context.Students.FirstOrDefault(s => s.Id == student.Id);
            if (existingStudent != null)
            {
                existingStudent.FullName = student.FullName;
                existingStudent.Email = student.Email;
                existingStudent.ContactNumber = student.ContactNumber;
                // Add other editable fields here

                _context.SaveChanges();
            }
        }

        public Student GetByStudentId(string studentId)
        {
            return _context.Students.FirstOrDefault(s => s.StudentID == studentId); // assuming StudentId is like "21-XXXXX-1"
        }



        public StudentRepository(StudentPortalDbContext context)
        {
            _context = context;
        }

    }
}
