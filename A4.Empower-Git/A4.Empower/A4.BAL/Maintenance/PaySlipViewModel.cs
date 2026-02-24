using System;
using System.Collections.Generic;
using System.Text;

namespace A4.BAL.Maintenance
{
  public class PaySlipViewModel
    {
        public int Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmpDesignation { get; set; }
        public string EmpCode { get; set; }
        public string Location { get; set; }
        public DateTime DOJ { get; set; }
        public string Month { get; set; }
        public int Year { get; set; }
        public decimal TotalDaysOfMonth { get; set; }
        public decimal AllowedLeave { get; set; }
        public decimal LeaveTaken { get; set; }
        public decimal WorkedDays { get; set; }
        public decimal Ctc { get; set; }
        public decimal CtcForDecember { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal DA { get; set; }
        public decimal HRA { get; set; }
        public decimal Conveyance { get; set; }
        public decimal ConvWorking { get; set; }
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
        public decimal PfApplicable { get; set; }
        public decimal MedicalBillAmount { get; set; }
        public decimal Unpaid { get; set; }
        public decimal TotalEarning { get; set; }
        public decimal TotalDeduction { get; set; }
        public string PanNumber { get; set; }
    }
}
