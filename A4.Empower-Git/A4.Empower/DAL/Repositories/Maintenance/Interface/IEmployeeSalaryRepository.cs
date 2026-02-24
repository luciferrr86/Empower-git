using A4.BAL.Maintenance;
using A4.DAL.Entites;
using A4.DAL.Entites.Maintenance;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Repositories
{
    public interface IEmployeeSalaryRepository : IRepository<EmployeeSalary>
    {
        List<EmployeeSalary> GetEmpMonthlySalaryDetail(Guid? employeeId, int month, int year);
      
    }
}
