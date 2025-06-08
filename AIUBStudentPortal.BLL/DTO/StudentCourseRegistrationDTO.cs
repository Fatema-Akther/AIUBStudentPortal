using AIUBStudentPortal.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIUBStudentPortal.BLL.DTO
{
    public class StudentCourseRegistrationDTO
    {
        public int StudentId { get; set; }
        public List<int> SelectedCourseIds { get; set; }
    }
}
