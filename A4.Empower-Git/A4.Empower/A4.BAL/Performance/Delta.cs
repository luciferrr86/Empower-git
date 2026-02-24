using System;
using System.Collections.Generic;
using System.Text;

namespace A4.BAL
{
   public class Delta
    {
        public int DeltaId { get; set; }
        public bool IsNewTemplate { get; set; }
        //[Required(ErrorMessage = "Please Enter Delta Properly")]
        public string DeltaTextEmp { get; set; }
        //[Required(ErrorMessage = "Please Enter Delta Properly")]
        public string DeltaTextMgr { get; set; }
    }
}
