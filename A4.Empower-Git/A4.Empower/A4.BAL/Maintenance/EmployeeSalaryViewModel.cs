using System;
using System.Collections.Generic;
using System.Text;

namespace A4.BAL.Maintenance
{
    public class EmployeeSalaryViewModel
    {
       
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public Guid EmployeeId { get; set; }
        public int EmployeeCode { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal TotalDaysOfMonth { get; set; }
        public decimal AllowedLeave { get; set; }
        public decimal LeaveTaken { get; set; }
        public decimal WorkedDays { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal DA { get; set; }
        public decimal HRA { get; set; }
        public decimal Conveyance { get; set; }
        public decimal MedicalExpenses { get; set; }
        public decimal Special { get; set; }
        public decimal Bonus { get; set; }
        public decimal TA { get; set; }
        public decimal Total { get; set; }
        public decimal ContributionToPf { get; set; }
        public decimal ProfessionTax { get; set; }
        public decimal TDS { get; set; }
        public decimal SalaryAdvance { get; set; }
        public decimal SalaryTotal { get; set; }
        public decimal NetPayable { get; set; }
        public decimal MedicalBillAmount { get; set; }
        public decimal UnpaidDays { get; set; }
        public int EmployeeCtcId { get; set; }
        public EmployeeCtcViewModel EmployeeCtc { get; set; }
        public List<SalaryPartViewModel> SalaryPart { get; set; }

    }
}
