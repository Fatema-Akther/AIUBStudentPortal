using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace AIUBStudentPortal.BLL.DTO
{
    public class DropApplicationDTO
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; }
        public string CourseName { get; set; }  // Optional for UI
        public string CourseSection { get; set; } // Optional for UI
    }
}
