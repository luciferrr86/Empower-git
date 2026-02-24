using A4.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class ExpenseBookingRequestViewModel
    {
        public ExpenseBookingRequestViewModel()
        {
            ExpenseBookingListModel = new List<ExpenseBookingListModel>();
        }
        public List<ExpenseBookingListModel> ExpenseBookingListModel { get; set; }

        public int ExpenseBookingCount { get; set; }
    }


    public class ExpenseBookingListModel
    {

        public string Id { get; set; }

        public string ExpensePeriod { get; set; }

        public string BookingId { get; set; }

        public string Amount { get; set; }

        public string Department { get; set; }

        public string EmployeeName { get; set; }

        public string RequestedDate { get; set; }

        public string ApprovedOrRejectedDate { get; set; }

        public string SubCategoryItem { get; set; }

        public string Remarks { get; set; }

        public string Status { get; set; }

        public bool IsInvite { get; set; }

        public string File { get; set; }


    }
    public class ExpenseBookingExcel
    {

        public string Name { get; set; }

        public string ExpensePeriod { get; set; }

        public string Amount { get; set; }

        public string Department { get; set; }

        public string RequestDate { get; set; }

        public string BookingId { get; set; }

        public string ApprovedDate { get; set; }

    }
    public class ExpenseBookingExcelViewModel
    {
        public ExpenseBookingExcelViewModel()
        {
            ExpenseBookingExcel = new List<ExpenseBookingExcel>();
        }
        public List<ExpenseBookingExcel> ExpenseBookingExcel { get; set; }



    }
    public class ExpenseBookingApproverModal
    {
        public ExpenseBookingApproverModal()
        {
            ExpenseBookingDetailApproverList = new List<ExpenseBookingDetail>();
            ExpenseBookingIviteApproverList = new List<ExpenseBookingIviteApprover>();
        }
        public string Id { get; set; }

        public int Level { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        public List<ExpenseBookingDetail> ExpenseBookingDetailApproverList { get; set; }

        public List<ExpenseBookingIviteApprover> ExpenseBookingIviteApproverList { get; set; }
    }

    public class ExpenseBookingIviteApprover
    {
        public ExpenseBookingIviteApprover()
        {
            ExpenseBookingDetailIviteApproverList = new List<ExpenseBookingDetail>();
        }
        public string Id { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        public List<ExpenseBookingDetail> ExpenseBookingDetailIviteApproverList { get; set; }
    }

    public class ExpenseBookingDetail
    {
        public string Id { get; set; }
        public string ManagerComment { get; set; }
        public string EmployeeComment { get; set; }
    }

    public class ExpenseBookingModel
    {
        public ExpenseBookingModel()
        {
            ExpenseBookingDetail = new List<ExpenseBookingApproverModal>();
            SelectedEmployeeId = new List<string>();
            File = new List<string>();
            ExpenseDocumentList = new List<ExpenseDocumentList>();
            IsInviteApproved = true;
        }
        public string Id { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string Amount { get; set; }

        public string DepartmentId { get; set; }

        public string Department { get; set; }


        public string Category { get; set; }

        public string SubCategory { get; set; }

        public string SubCategoryItem { get; set; }

        public string SubCategoryItemId { get; set; }

        public string Remarks { get; set; }

        public string ApproverRemarks { get; set; }

        public List<string> File { get; set; }

        public bool IsSubmitted { get; set; }

        public string ApproverId { get; set; }
        public bool IsInviteApproved { get; set; }

        public string Status { get; set; }

        public List<DropDownList> SubCategoryItems { get; set; }

        public List<DropDownList> InviteEmployeeList { get; set; }

        public List<string> SelectedEmployeeId { get; set; }

        public List<DropDownList> DepartmentList { get; set; }


        public List<ExpenseBookingApproverModal> ExpenseBookingDetail { get; set; }

        public List<ExpenseDocumentList> ExpenseDocumentList { get; set; }

    }

    public class ExpenseBookingStatusModel
    {
        public string Comment { get; set; }
        public ExpenseBookingStatusModel()
        {
            ExpenseBookingDetail = new List<ExpenseBookingDetail>();
        }

        public int ButtonType { get; set; }

        public List<ExpenseBookingDetail> ExpenseBookingDetail { get; set; }
    }

    public class InviteApproverModel
    {
        public InviteApproverModel()
        {
            SelectedApprover = new List<string>();
        }

        public List<String> SelectedApprover { get; set; }

    }
    public class ExpenseDocumentList
    {
        public string ExpenseDocumentId { get; set; }
        public string FileUrl { get; set; }
        public string FileName { get; set; }
    }

}
