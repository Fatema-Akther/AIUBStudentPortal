using AIUBStudentPortal.BLL.DTO;
using System.Collections.Generic;

namespace AIUBStudentPortal.BLL.ViewModels
{
    public class DropApplicationViewModel
    {
        public List<CourseDTO> RegisteredCourses { get; set; }
        public List<DropApplicationDTO> DropRequests { get; set; }
    }
}
