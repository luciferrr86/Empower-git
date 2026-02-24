using System;
using System.Collections.Generic;
using System.Text;

namespace A4.BAL.ExpenseBooking
{
    public class ExpenseBookingTitleViewModel
    {
        public ExpenseBookingTitleViewModel()
        {
            ExpenseBookingTitleModel = new List<ExpenseBookingTitleModel>();
        }
        public List<ExpenseBookingTitleModel> ExpenseBookingTitleModel { get; set;}
        public List<DropDownList> TitleList { get; set; }

        public int TotalCount { get; set; }
    }
    public class ExpenseBookingTitleModel
    {
        public string Id { get; set; }

        public string TitleName { get; set; }

        public string TitleId { get; set; }

        public string Amount { get; set; }
    }
    public class ManagerLevel
    {
        public Guid ManagerId { get; set; }
        public int Level { get; set; }
        public Guid TitleId { get; set; }
        public int Amount { get; set; }

    }
    public class ExpenseApproveByLevel {
        public Guid EmpID { get; set; }
        public Guid ManagerID { get; set; }
        public int EmpLevel { get; set; }
        public Guid TitleId { get; set; }
    }
}
