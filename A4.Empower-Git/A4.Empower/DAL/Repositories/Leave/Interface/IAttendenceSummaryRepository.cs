using A4.DAL.Entites;
using A4.DAL.Entites.Leave;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Repositories
{
    public interface IAttendenceSummaryRepository : IRepository<AttendenceSummary>
    {

        AttendenceSummary GetEmployeeSummary(Guid employeeId, int month, int year);
        List<AttendenceSummary> GetAttSummary(int month, int year);
    }
}

