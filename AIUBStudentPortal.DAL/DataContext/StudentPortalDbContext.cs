using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;
using AIUBStudentPortal.DAL.Entities;

namespace AIUBStudentPortal.DAL.DataContext
{
    public class StudentPortalDbContext : DbContext
    {
        public StudentPortalDbContext() : base("name=StudentPortalDbConnection")
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourseRegistration> StudentCourseRegistrations { get; set; }

        public DbSet<DropApplication> DropApplications { get; set; }

    }
}
