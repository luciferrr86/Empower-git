using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.Empower.ViewModels
{
    public class ApplicationModuleViewModel
    {
        public ApplicationModuleViewModel()
        {
            ApplicationModuleDetailModel = new List<ApplicationModuleDetailModel>();
        }

        public List<ApplicationModuleDetailModel> ApplicationModuleDetailModel { get; set; }
        public string Id { get; set; }
        public string ModuleName { get; set; }
        public bool IsActive { get; set; }
    }

    public class ApplicationModuleDetailModel
    {
        public string Id { get; set; }
        public string SubModuleName { get; set; }
        public bool IsActive { get; set; }
    }
}
