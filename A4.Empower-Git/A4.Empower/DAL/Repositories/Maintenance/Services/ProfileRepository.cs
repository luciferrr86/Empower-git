using A4.DAL.Entites;
using DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace A4.DAL.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        public readonly DbContext _context;
        public ProfileRepository(DbContext context)
        {
            this._context = context;
        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;
        public int GetProfilePicId(string userId)
        {
            int picId = 0;
            var aspNetUser = _appContext.Users.Where(m => m.Id == userId).FirstOrDefault();
            if (aspNetUser != null)
            {
                picId = Convert.ToInt32(aspNetUser.PictureId);
            }
            return picId;
        }
        public void UpdateProfilePicDetail(string picId, string userId)
        {
            var aspNetUser = _appContext.Users.Where(m => m.Id == userId).FirstOrDefault();
            if (aspNetUser != null)
            {
                aspNetUser.PictureId = Convert.ToInt32(picId);
                _appContext.Users.Update(aspNetUser);
                _appContext.SaveChanges();
            }

        }

        public void AddPersonalDetail(Personal personalDetail)
        {
            _appContext.Personal.Add(personalDetail);
        }

        public void AddProfessionalDetail(Professional professionalDetail)
        {
            _appContext.Professional.Add(professionalDetail);
            _appContext.SaveChanges();
        }

        public void AddQualificationDetail(Qualification qualificationDetail)
        {
            _appContext.Qualification.Add(qualificationDetail);
        }

        public Personal GetPersonalDetail(string userId)
        {
            var query = from user in _appContext.Users
                        join emp in _appContext.Employee on user.Id equals emp.UserId
                        join personal in _appContext.Personal on emp.Id equals personal.EmployeeId
                        where user.Id == userId
                        select new Personal
                        {
                            Id = personal.Id,
                            Employee = new Employee { ApplicationUser = new ApplicationUser { FullName = user.FullName, Email = user.Email } },
                            City = personal.City,
                            ContactNo = personal.ContactNo,
                            OfficialContactNo = personal.OfficialContactNo,
                            Country = personal.Country,
                            CurrentAddress = personal.CurrentAddress,
                            DOB = personal.DOB,
                            EmailId = personal.EmailId,
                            EmergencyContactName = personal.EmergencyContactName,
                            EmergencyContactNo = personal.EmergencyContactNo,
                            EmergencyContactRelation = personal.EmergencyContactRelation,
                            Gender = personal.Gender,
                            IdProofDetail = personal.IdProofDetail,
                            MaritalStatus = personal.MaritalStatus,
                            Nationality = personal.Nationality,
                            PermanentAddress = personal.PermanentAddress,
                            State = personal.State,
                            ZipCode = personal.ZipCode,
                            PanNumber=personal.PanNumber,
                            CurrentCity = personal.CurrentCity,
                            CurrentState = personal.CurrentState,
                            CurrentCountry = personal.CurrentCountry,
                            CurrentZipCode = personal.CurrentZipCode
                        };

            return query.FirstOrDefault();
        }
        public Personal GetPersonal(Guid id)
        {
            var personal = _appContext.Personal.Where(m => m.Id == id && m.IsActive == true).FirstOrDefault();
            return personal;
        }

        public List<Professional> GetProfessionalDetail(Guid empId)
        {
            var professional = new List<Professional>();
            var employee = _appContext.Employee.Where(e => e.Id == empId && e.IsActive == true).FirstOrDefault();
            if (employee != null)
            {
                professional = _appContext.Professional.Where(c => c.EmployeeId == employee.Id && c.IsActive == true).ToList();
            }
            return professional;
        }

        public Professional GetProfessional(Guid id)
        {
            var professional = _appContext.Professional.Where(c => c.Id == id && c.IsActive == true).FirstOrDefault();
            return professional;
        }
        public List<Professional> GetProfessionalEmployee(Guid empId)
        {
            var professional = _appContext.Professional.Where(c => c.EmployeeId == empId && c.IsActive == true).ToList();
            return professional;
        }

        public Qualification GetQualificationDetail(Guid empId)
        {
            var qualification = new Qualification();
            var employee = _appContext.Employee.Where(e => e.Id == empId && e.IsActive == true).FirstOrDefault();
            if (employee != null)
            {
                qualification = _appContext.Qualification.Where(a => a.EmployeeId == employee.Id).FirstOrDefault();
            }
            return qualification;
        }

        public Qualification GetQualification(Guid id)
        {
            var qualification = _appContext.Qualification.Where(m => m.Id == id).FirstOrDefault();
            return qualification;
        }

        public void UpdatePersonalDetail(Personal personalDetail)
        {
            _appContext.Personal.Update(personalDetail);
            _appContext.SaveChanges();
        }

        public void UpdateProfessionalDetail(Professional professionalDetail)
        {
            _appContext.Professional.Update(professionalDetail);
            _appContext.SaveChanges();
        }

        public void UpdateQualificationDetail(Qualification qualificationDetail)
        {
            _appContext.Qualification.Update(qualificationDetail);
            _appContext.SaveChanges();
        }

        public void DeleteProfessionalDetail(Professional professionalDetail)
        {
            _appContext.Professional.Remove(professionalDetail);
        }

    }
}