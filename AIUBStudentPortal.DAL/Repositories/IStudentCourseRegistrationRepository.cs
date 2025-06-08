using AIUBStudentPortal.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIUBStudentPortal.DAL.Repositories
{
    public interface IStudentCourseRegistrationRepository
    {
        bool HasStudentRegistered(int studentId);
        void RegisterCourses(int studentId, List<int> courseIds);
        List<Course> GetRegisteredCourses(int studentId);


        void CreateDropRequest(int studentId, int courseId);

    }

}
