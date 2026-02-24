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
    public class EmployeeAttendenceRepository : Repository<EmployeeAttendence>, IEmployeeAttendenceRepository
    {
        public EmployeeAttendenceRepository(DbContext context) : base(context)
        {

        }

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        //public List<EmployeeAttendence> GetAllEmployeeMonthlyDetail(int month, int year)
        //{
        //    int startDay = 1;
        //    if (year == 0)
        //    {
        //        year = DateTime.Today.Year;
        //    }
        //    if (month == 0)
        //    {
        //        month = DateTime.Today.Month;
        //    }

        //    var monthStartDate = new DateTime(year, month, startDay);
        //    DateTime monthEndDay;
        //    if (startDay == 1)
        //    {
        //        monthEndDay = new DateTime(year, month, DateTime.DaysInMonth(year, month));
        //    }
        //    else
        //    {
        //        monthEndDay = monthStartDate.AddMonths(1).AddDays(-1);
        //    }

        //    var monthlyAttendence = new MonthlyAttendence();
        //    //      Expression<Func<EmployeeAttendence, bool>> emFilter = e => e.IsActive && e.Date >= monthStartDate && e.Date <= monthEndDay;
        //    var attendence = _appContext.EmployeeAttendence.Where(m => m.IsActive && m.Date >= monthStartDate && m.Date <= monthEndDay).Select(e=> e.EmployeeId).ToList();
        //    //var attendence = _unitOfWork.EmployeeAttendence.Get(emFilter, null, "").ToList();
        //    //monthlyAttendence.EmployeeAttendenceVM = Mapper.Map<IEnumerable<EmployeeAttendence>, IEnumerable<EmployeeAttendenceViewModel>>(attendence);

        //   //  monthlyAttendence.EmployeeAttendence = Mapper.Map<IEnumerable<EmployeeAttendence>, IEnumerable<EmployeeAttendenceViewModel>>(attendence);
        //    // var att = Mapper.Map<EmployeeAttendence, EmployeeAttendenceViewModel>(attendence);
        //    monthlyAttendence.MonthDates = GetDates(month, year, startDay);
        //    monthlyAttendence.Month = month;
        //    monthlyAttendence.Year = year;
        //    monthlyAttendence.StartDay = startDay;

        //    return null;
        //}
        public List<EmployeeAttendence> GetEmployeeMonthlyDetail(Guid? employeeId, int month, int year)
        {
            int startDay = 1;           

            var monthStartDate = new DateTime(year, month, startDay);
            DateTime monthEndDay;
            if (startDay == 1)
            {
                monthEndDay = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            }
            else
            {
                monthEndDay = monthStartDate.AddMonths(1).AddDays(-1);
            }

           
            //      Expression<Func<EmployeeAttendence, bool>> emFilter = e => e.IsActive && e.Date >= monthStartDate && e.Date <= monthEndDay;
            var attendence = new List<EmployeeAttendence>();
            
            if (employeeId.HasValue)
            {
             attendence= _appContext.EmployeeAttendence.Where(m => m.EmployeeId == employeeId.Value && m.Date >= monthStartDate && m.Date <= monthEndDay).OrderBy(o=>o.Date).ToList();
              
            }
            else
            {
                attendence= _appContext.EmployeeAttendence.Where(m => m.Date >= monthStartDate && m.Date <= monthEndDay).ToList();

            }
            return attendence;
        }
        public List<KeyValuePair<string, string>> GetDates(int month, int year, int startDay)
        {
            // int startDay = 1;
            // int endDay = 0;
            var monthStartDate = new DateTime(year, month, startDay);
            DateTime monthEndDay;
            if (startDay == 1)
            {
                monthEndDay = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            }
            else
            {
                monthEndDay = monthStartDate.AddMonths(1).AddDays(-1);
            }
            var date = monthStartDate;
            var dateList = new List<KeyValuePair<string, string>>();
            while (date <= monthEndDay)
            {
                var datePair = new KeyValuePair<string, string>(date.ToShortDateString(), date.DayOfWeek.ToString());
                dateList.Add(datePair);
                date = date.AddDays(1);
            }
            return dateList;

            //for(int i= monthStartDay)
            //return Enumerable.Range(1, DateTime.DaysInMonth(year, month))  // Days: 1, 2 ... 31 etc.
            //                 .Select(day => new DateTime(year, month, day)) // Map each day to a date
            //                 .ToList(); // Load dates into a list
        }
    }
}
