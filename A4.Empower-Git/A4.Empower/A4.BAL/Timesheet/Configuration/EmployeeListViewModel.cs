using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class EmployeeListViewModel
    {
        public EmployeeListViewModel()
        {
            EmployeeList = new List<EmployeeListModel>();
        }
        public int TotalCount { get; set; }
        public List<EmployeeListModel> EmployeeList { get; set; }
    }

    public class EmployeeListModel
    {

        public string EmployeeId { get; set; }

        public string FullName { get; set; }

        public string Designation { get; set; }

        public string ProjectId { get; set; }

        public string TemplateId { get; set; }

        public bool IsConfig { get; set; }

    }
}
