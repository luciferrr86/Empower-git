using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class ProjectViewModel
    {

        public ProjectViewModel()
        {
            ProjectList = new List<ProjectModel>();
        }

        public List<ProjectModel> ProjectList { get; set; }

        public int TotalCount { get; set; }

        public List<DropDownList> ClientList { get; set; }

        public List<DropDownList> ManagerList { get; set; }

    }
    public class ProjectModel
    {
        public string Id { get; set; }

        public string ProjectName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public string ClientId { get; set; }

        public string ManagerId { get; set; }
    }

    public class AssignProjectViewModel
    {
        public AssignProjectViewModel()
        {
            EmployeeList = new List<EmployeeListModel>();
        }

        public int TotalCount { get; set; }

        public List<DropDownList> ProjectList { get; set; }

        public List<EmployeeListModel> EmployeeList { get; set; }
    }

    public class AssignProjectModel
    {
        public string Id { get; set; }

        [Required]
        public string ProjectId { get; set; }
        [Required]
        public List<Employeelist> Employeelist { get; set; }
    }

    public class Employeelist
    {
        public string EmployeeId { get; set; }
    }
}
