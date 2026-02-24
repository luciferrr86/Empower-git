using A4.BAL.Leave;
using A4.DAL.Entites;
using A4.DAL.Entites.Leave;
using DAL;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace A4.DAL.Repositories
{
    public class AttendenceSummaryRepository : Repository<AttendenceSummary>, IAttendenceSummaryRepository
    {
        public AttendenceSummaryRepository(DbContext context) : base(context)
        {

        }

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;
        public List<AttendenceSummary> GetAttSummary(int month, int year)
        {
            var attendence = new List<AttendenceSummary>();
            attendence = _appContext.AttendenceSummary.Where(m => m.Month == month && m.Year == year).ToList();
            return attendence;
        }
        public AttendenceSummary GetEmployeeSummary(Guid employeeId, int month, int year)
        {
           
            var attendence = new AttendenceSummary();

            if (employeeId != null)
            {
                attendence = _appContext.AttendenceSummary.Where(m => m.EmployeeId == employeeId && m.Month == month && m.Year == year).FirstOrDefault();

            }
            //else
            //{
            //    attendence = _appContext.AttendenceSummary.Where(m=> m.Month == month.ToString() && m.Year == year.ToString()).FirstOrDefault();
            //}
            
            return attendence;
        }


    }
}
