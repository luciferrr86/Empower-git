using A4.DAL.Entites;
using A4.DAL.Entites.Leave;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Repositories
{
    public interface IEmployeeAttendenceRepository : IRepository<EmployeeAttendence>
    {
        List<EmployeeAttendence> GetEmployeeMonthlyDetail(Guid? employeeId, int month, int year);
        //List<EmployeeAttendence> GetAllEmployeeMonthlyDetail(int month, int year);
    }
}

