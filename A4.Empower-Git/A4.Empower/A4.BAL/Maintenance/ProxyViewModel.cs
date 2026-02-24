using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class ProxyViewModel
    {
        public int EmployeeId { get; set; }
        public string EmpProxyFor { get; set; }
        public bool IsActive { get; set; }
        public string ProxySettingType { get; set; }

    }
}
