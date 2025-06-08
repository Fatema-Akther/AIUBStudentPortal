using AIUBStudentPortal.BLL.DTO;
using AIUBStudentPortal.DAL.Entities;
using AIUBStudentPortal.DAL.Repositories;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace AIUBStudentPortal.BLL.Services
{
    public class DropApplicationService
    {
        private readonly IDropApplicationRepository _dropRepo;
        private readonly IMapper _mapper;

        public DropApplicationService(IDropApplicationRepository dropRepo, IMapper mapper)
        {
            _dropRepo = dropRepo;
            _mapper = mapper;
        }

        public void SubmitDropRequest(DropApplicationDTO dto)
        {
            dto.RequestDate = System.DateTime.Now;
            dto.Status = "Pending";

            var entity = _mapper.Map<DropApplication>(dto);
            _dropRepo.AddDropApplication(entity);
        }

        public List<DropApplicationDTO> GetDropRequestsByStudent(int studentId)
        {
            var entities = _dropRepo.GetDropApplicationsByStudent(studentId);

            // Optionally include course info in DTO
            var dtos = entities.Select(d =>
            {
                var dto = _mapper.Map<DropApplicationDTO>(d);
                if (d.Course != null)
                {
                    dto.CourseName = d.Course.Name;
                    dto.CourseSection = d.Course.Section;
                }
                return dto;
            }).ToList();

            return dtos;
        }

        public DropApplicationDTO GetDropRequestById(int id)
        {
            var entity = _dropRepo.GetDropApplicationById(id);
            return _mapper.Map<DropApplicationDTO>(entity);
        }

        public void UpdateDropRequest(DropApplicationDTO dto)
        {
            var entity = _mapper.Map<DropApplication>(dto);
            _dropRepo.UpdateDropApplication(entity);
        }



        public List<DropApplicationDTO> GetAllDropRequests()
        {
            var all = _dropRepo.GetAll();
            return all.Select(d =>
            {
                var dto = _mapper.Map<DropApplicationDTO>(d);
                if (d.Course != null)
                {
                    dto.CourseName = d.Course.Name;
                    dto.CourseSection = d.Course.Section;
                }
                return dto;
            }).ToList();
        }

    }
}
