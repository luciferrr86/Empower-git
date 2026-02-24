using A4.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Repositories
{
    public interface IProfileRepository
    {
        int GetProfilePicId(string userId);
        void UpdateProfilePicDetail(string picId,string userId);

        Personal GetPersonalDetail(string userId);
        Personal GetPersonal(Guid id);

        List<Professional> GetProfessionalDetail(Guid empId);
        Professional GetProfessional(Guid id);
        List<Professional> GetProfessionalEmployee(Guid empId);

        Qualification GetQualificationDetail(Guid empId);
        Qualification GetQualification(Guid id);

        void UpdatePersonalDetail(Personal personalDetail);
        void UpdateProfessionalDetail(Professional professionalDetail);
        void UpdateQualificationDetail(Qualification qualificationDetail);
        void AddPersonalDetail(Personal personalDetail);
        void AddProfessionalDetail(Professional professionalDetail);
        void AddQualificationDetail(Qualification qualificationDetail);
        void DeleteProfessionalDetail(Professional professionalDetail);
    }
}
