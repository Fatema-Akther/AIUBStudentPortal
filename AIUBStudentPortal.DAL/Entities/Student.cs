using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIUBStudentPortal.DAL.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string StudentID { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public string ContactNumber { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string PasswordHash { get; set; }

    }
}
