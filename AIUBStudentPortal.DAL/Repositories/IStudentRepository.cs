using System.Collections.Generic;
using AIUBStudentPortal.DAL.Entities;

namespace AIUBStudentPortal.DAL.Repositories
{
    public interface IStudentRepository
    {
        void Add(Student student);
        IEnumerable<Student> GetAll();
        // Add more as needed (e.g., GetById, Delete, etc.)

        Student GetById(int id);

        // ✅ Add these missing declarations
        Student GetByStudentId(string studentId);
        void Update(Student student);

    }
}
