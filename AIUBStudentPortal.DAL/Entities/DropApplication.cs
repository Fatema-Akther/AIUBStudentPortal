using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace AIUBStudentPortal.DAL.Entities
{
    public class DropApplication
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; }  // e.g. "Pending", "Approved", "Rejected"

        // Navigation properties (optional)
        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }
    }
}
