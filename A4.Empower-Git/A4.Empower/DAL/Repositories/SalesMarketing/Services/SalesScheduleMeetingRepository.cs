using A4.BAL;
using A4.DAL.Entites;
using DAL;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Repositories
{
   public  class SalesScheduleMeetingRepository : Repository<SalesScheduleMeeting>, ISalesScheduleMeetingRepository
    {
        public SalesScheduleMeetingRepository(DbContext context) : base(context)
        {

        }

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public PagedList<SalesScheduleMeeting> GetAllSalesScheduleMeeting(Guid salesCompanyId, string name = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var listSalesScheduleMeeting = _appContext.SalesScheduleMeeting.AsQueryable().Where(c=>c.SalesCompanyId  == salesCompanyId && c.IsActive == true);
            if (!string.IsNullOrWhiteSpace(name) && name != "undefined")
                listSalesScheduleMeeting = listSalesScheduleMeeting.Where(c => c.Subject.Contains(name));
            listSalesScheduleMeeting = listSalesScheduleMeeting.OrderBy(c => c.Subject).ThenBy(c => c.Id);
            return new PagedList<SalesScheduleMeeting>(listSalesScheduleMeeting, pageIndex, pageSize);
        }

   

        public List<ScheduleDropdownList> GetInternalPersonList()
        {
            var lstInternalPerson = new List<ScheduleDropdownList>();
            var internalPersonList = GetEmployeeList();
            if (internalPersonList.Count > 0)
            {
                foreach (var item in internalPersonList)
                {
                    lstInternalPerson.Add(new ScheduleDropdownList
                    {
                        Value = Convert.ToString(item.Id),
                        Label = item.ApplicationUser.FullName + " " + item.ApplicationUser.Email,
                        Ischecked = true
                    });
                }
            }
            return lstInternalPerson;
        }

        private List<Employee> GetEmployeeList()
        {
            var query = from emp in _appContext.Employee.Where(m => m.IsActive == true)
                        join appuser in _appContext.Users.Where(m => m.IsActive == true) on emp.UserId equals appuser.Id
                        select new Employee { Id = emp.Id, ApplicationUser = new ApplicationUser { FullName = emp.ApplicationUser.FullName, Email = emp.ApplicationUser.Email } };

            return query.ToList();
        }

    }
}
