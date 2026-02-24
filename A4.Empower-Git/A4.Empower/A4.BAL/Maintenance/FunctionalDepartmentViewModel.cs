using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
   
    public class FunctionalDepartmentViewModel
    {
        public FunctionalDepartmentViewModel()
        {
            FunctionalDepartmentModel = new List<FunctionalDepartmentModel>();
        }
        public List<FunctionalDepartmentModel> FunctionalDepartmentModel { get; set; }
        public int TotalCount { get; set; }
    }

    public class FunctionalDepartmentModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
