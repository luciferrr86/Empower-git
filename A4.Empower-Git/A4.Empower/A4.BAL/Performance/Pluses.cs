using System;
using System.Collections.Generic;
using System.Text;

namespace A4.BAL
{
   public class Pluses
    {
        public int PlusId { get; set; }
        public bool IsNewTemplate { get; set; }
        //[Required(ErrorMessage = "Please Enter Pluses Properly")]
        public string PlusTextEmp { get; set; }
        //[Required(ErrorMessage = "Please Enter Pluses Properly")]
        public string PlusTextMgr { get; set; }
    }
}
