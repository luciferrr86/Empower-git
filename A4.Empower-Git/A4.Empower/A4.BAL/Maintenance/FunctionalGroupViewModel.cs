using A4.BAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class FunctionalGroupViewModel
    {
        public FunctionalGroupViewModel()
        {
            FunctionalGroupModel = new List<FunctionalGroupModel>();
        }

        public List<FunctionalGroupModel> FunctionalGroupModel { get; set; }
        public List<DropDownList> DepartmentList { get; set; }
        public int TotalCount { get; set; }
    }
    public class FunctionalGroupModel
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string DepartmentId { get; set; }

    }

}
