using System;
using System.Collections.Generic;
using System.Text;

namespace A4.BAL
{
   public class MailList
    {
        public Guid EmpID { get; set; }
        public string MailID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Subject { get; set; }
        public string From { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string FeedbackForName { get; set; }
    }
}
