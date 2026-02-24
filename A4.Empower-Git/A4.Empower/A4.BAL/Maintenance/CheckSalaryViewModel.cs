using System;
using System.Collections.Generic;
using System.Text;

namespace A4.BAL.Maintenance
{
  public class CheckSalaryViewModel
    {
        public int Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string UserId { get; set; }
        public string EmployeeName { get; set; }
        public decimal LeaveTaken { get; set; }
        public decimal Total { get; set; }
        public decimal NetPayable { get; set; }
        public decimal Tds { get; set; }
        public decimal TotalDeductions { get; set; }
        public decimal ProfessionalTaxes { get; set; }
        public int EmployeeCode { get; set; }
        public int NoOfPages { get; set; }
    }
}
