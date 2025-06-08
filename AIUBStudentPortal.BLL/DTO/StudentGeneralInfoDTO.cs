using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIUBStudentPortal.BLL.DTO
{
    public class StudentGeneralInfoDTO
    {
        public int Id { get; set; } // Student ID
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
    }

}
