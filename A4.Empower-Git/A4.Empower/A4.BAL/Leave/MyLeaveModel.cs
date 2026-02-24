using A4.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class MyLeaveModel
    {
        public MyLeaveModel()
        {
            ddlleaveType = new List<DropDownList>();
        }
        public List<DropDownList> ddlleaveType{get; set;}

        public bool IsSet { get; set; }

     
    }
}
