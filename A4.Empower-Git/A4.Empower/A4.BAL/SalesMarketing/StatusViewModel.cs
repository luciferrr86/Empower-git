using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace A4.BAL
{
    public class StatusViewModel
    {
        public StatusViewModel()
        {
            DailyCallModel = new List<DailyCallModel>();
            DdlSaleStatus = new List<DropDownList>();
            StatusCompanyModel = new StatusCompanyModel();
        }
        public StatusCompanyModel StatusCompanyModel { get; set; }
        public List<DailyCallModel> DailyCallModel { get; set; }
        public List<DropDownList> DdlSaleStatus { get; set; }
    }

    public class StatusCompanyModel
    {
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string EmailId { get; set; }
        public string CompanyTelePhoneNo { get; set; }

    }

    public class DailyCallModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter date & Time")]
        public DateTime CallDateTime { get; set; }

        [Required(ErrorMessage = "Please enter Description")]
        public string Description { get; set; }

        public string SalesStatusId { get; set; }

        public string SalesCompanyId { get; set; }
    }
}
