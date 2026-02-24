using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class EmployeeViewModel
    {
        public EmployeeViewModel()
        {
            EmployeeModel = new List<EmployeeModel>();
        }

        public List<EmployeeModel> EmployeeModel { get; set; }
        public int TotalCount { get; set; }
    }
    public class EmployeeModel
    {
        public int EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string DOJ { get; set; }

        public string UserId { get; set; }

        public string GroupId { get; set; }

        public int DesignationId { get; set; }

        public int ManagerId { get; set; }

        public int TitleId { get; set; }

        public int BandId { get; set; }

    }
}
