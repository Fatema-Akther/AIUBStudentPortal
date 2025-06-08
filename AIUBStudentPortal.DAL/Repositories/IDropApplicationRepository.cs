using AIUBStudentPortal.DAL.Entities;
using System.Collections.Generic;

namespace AIUBStudentPortal.DAL.Repositories
{
    public interface IDropApplicationRepository
    {
        void AddDropApplication(DropApplication dropApplication);
        List<DropApplication> GetDropApplicationsByStudent(int studentId);
        DropApplication GetDropApplicationById(int id);
        void UpdateDropApplication(DropApplication dropApplication);

        List<DropApplication> GetAll();

    }
}
