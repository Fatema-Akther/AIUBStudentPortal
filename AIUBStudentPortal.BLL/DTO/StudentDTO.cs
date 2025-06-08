using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AIUBStudentPortal.BLL.DTOs
{
    public class StudentDTO
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^\d{2}-\d{5}-\d$", ErrorMessage = "Student ID must be in format XX-XXXXX-X")]
        public string StudentID { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [RegularExpression(@"^([0-9]{11})$", ErrorMessage = "Contact number must be exactly 11 digits.")]
        public string ContactNumber { get; set; }

        [Required]
        [EmailAddress]
        [RegularExpression(@"^\d{2}-\d{5}-\d@student\.aiub\.edu$", ErrorMessage = "Email must be in format XX-XXXXX-X@student.aiub.edu")]
        public string Email { get; set; }

        [Required]
        public string Department { get; set; }

        public DateTime RegistrationDate { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; }

    }
}
