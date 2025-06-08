using AIUBStudentPortal.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIUBStudentPortal.DAL.Repositories
{
    public interface ICourseRepository
    {
        List<Course> GetAllCourses();
        Course GetById(int id);

   
        void InsertCourse(Course course); // ✅ Add this


        void DeleteCourse(int id);
    }


    


    }
