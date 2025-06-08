using AIUBStudentPortal.BLL.DTO;
using AIUBStudentPortal.BLL.DTOs;
using AIUBStudentPortal.DAL.Entities;
using AIUBStudentPortal.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace AIUBStudentPortal.BLL.Services
{
    public class StudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public void RegisterStudent(StudentDTO studentDTO)
        {
            var student = new Student
            {
                FullName = studentDTO.FullName,
                StudentID = studentDTO.StudentID,
                Email = studentDTO.Email,
                Department = studentDTO.Department,
                ContactNumber = studentDTO.ContactNumber,
                RegistrationDate = studentDTO.RegistrationDate,
                PasswordHash = studentDTO.Password // Already hashed in Controller
            };

            _studentRepository.Add(student);
        }


        public List<StudentDTO> GetAllStudents()
        {
            var students = _studentRepository.GetAll();
            var dtoList = new List<StudentDTO>();

            foreach (var student in students)
            {
                dtoList.Add(new StudentDTO
                {
                    Id = student.Id,
                    FullName = student.FullName,
                    StudentID = student.StudentID,
                    Email = student.Email,
                    Department = student.Department,
                    ContactNumber = student.ContactNumber,
                    RegistrationDate = student.RegistrationDate,
                    Password = student.PasswordHash // ✅ ADD THIS LINE
                });
            }

            return dtoList;
        }



        public StudentGeneralInfoDTO GetStudentGeneralInfo(string studentId)
        {
            var student = _studentRepository.GetByStudentId(studentId);
            return new StudentGeneralInfoDTO
            {
                Id = student.Id,
                FullName = student.FullName,
                Email = student.Email,
                ContactNumber = student.ContactNumber
            };
        }

        public void UpdateStudentGeneralInfo(StudentGeneralInfoDTO dto)
        {
            var student = _studentRepository.GetById(dto.Id);
            if (student != null)
            {
                student.FullName = dto.FullName;
                student.Email = dto.Email;
                student.ContactNumber = dto.ContactNumber;
                _studentRepository.Update(student);
            }
        }

    }
}
