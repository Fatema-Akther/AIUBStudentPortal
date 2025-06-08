using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIUBStudentPortal.DAL.Entities
{
public class StudentCourseRegistration
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }  // This maps to Course.Id
        public DateTime RegistrationDate { get; set; }

        [ForeignKey("CourseId")]  // Explicitly tell EF this is a FK to Course.Id
        public virtual Course Course { get; set; }

        [ForeignKey("StudentId")]  // Explicitly tell EF this is a FK to Student.Id
        public virtual Student Student { get; set; }

    }
}
