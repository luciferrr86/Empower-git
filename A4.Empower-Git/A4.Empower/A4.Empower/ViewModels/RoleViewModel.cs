using A4.Empower.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4.Empower.ViewModels
{
    public class RoleViewModel
    {
        public RoleViewModel()
        {
            RoleModel = new List<RoleModel>();
        }
        public List<RoleModel> RoleModel { get; set; }

        public int TotalCount { get; set; }

    }

}
