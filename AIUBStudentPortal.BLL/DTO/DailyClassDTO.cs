using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIUBStudentPortal.BLL.DTO
{
    public class DailyClassDTO
    {
        public string DateLabel { get; set; } // e.g. "Today", "Tomorrow", "29-May-2025"
        public List<CourseDTO> Courses { get; set; }
    }

}
