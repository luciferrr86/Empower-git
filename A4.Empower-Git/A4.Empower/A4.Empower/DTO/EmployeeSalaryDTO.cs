using A4.DAL.Entites.Maintenance;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.BAL.DTO
{
    public class EmployeeSalaryDTO
    {
        public string EmployeeName { get; set; }
        public Guid EmployeeId { get; set; }
        public int EmployeeCode { get; set; }
        public List<SalaryComponent> SalaryComponents { get; set; }
        public EmployeeCtc EmployeeCtc { get; set; }
        public EmployeeSalary EmployeeSalary { get; set; }
    }
}
