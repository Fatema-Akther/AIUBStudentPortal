using AIUBStudentPortal.DAL.DataContext;
using AIUBStudentPortal.DAL.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace AIUBStudentPortal.DAL.Repositories
{
    public class DropApplicationRepository : IDropApplicationRepository
    {
        private readonly StudentPortalDbContext _context;

        public DropApplicationRepository(StudentPortalDbContext context)
        {
            _context = context;
        }

        public void AddDropApplication(DropApplication dropApplication)
        {
            _context.DropApplications.Add(dropApplication);
            _context.SaveChanges();
        }

        public List<DropApplication> GetDropApplicationsByStudent(int studentId)
        {
            return _context.DropApplications
                .Where(d => d.StudentId == studentId)
                .ToList();
        }

        public DropApplication GetDropApplicationById(int id)
        {
            return _context.DropApplications.FirstOrDefault(d => d.Id == id);
        }

        /*public void UpdateDropApplication(DropApplication dropApplication)
        {
            _context.Entry(dropApplication).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }*/


        public void UpdateDropApplication(DropApplication dropApplication)
        {
            var existingEntity = _context.DropApplications.Local.FirstOrDefault(d => d.Id == dropApplication.Id);
            if (existingEntity != null)
            {
                _context.Entry(existingEntity).State = EntityState.Detached;
            }
            _context.Entry(dropApplication).State = EntityState.Modified;
            _context.SaveChanges();
        }



        public List<DropApplication> GetAll()
        {
            return _context.DropApplications.ToList();
        }


    }
}
