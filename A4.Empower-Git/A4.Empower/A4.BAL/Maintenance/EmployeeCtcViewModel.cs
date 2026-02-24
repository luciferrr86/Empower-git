using System;
using System.Collections.Generic;
using System.Text;

namespace A4.BAL.Maintenance
{
    public class EmployeeCtcViewModel
    {
        public int Id { get; set; }
        public Guid EmployeeId { get; set; }
        public decimal Ctc { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal DA { get; set; }
        public decimal HRA { get; set; }
        public decimal Conveyance { get; set; }
        public decimal MedicalExpenses { get; set; }
        public decimal Special { get; set; }
        public decimal Bonus { get; set; }
        public decimal Total { get; set; }
        public string EmployeeName { get; set; }
        public int EmployeeCode { get; set; }
        public List<CtcOthercomponentViewModel> CtcOtherComponent { get; set; }
        public List<EmployeeSalaryViewModel> EmployeeSalary { get; set; }
    }
}
