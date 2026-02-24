using System;
using System.Collections.Generic;
using System.Text;

namespace A4.BAL.Maintenance
{
    public class CandidateBulkViewModel
    {
        public CandidateBulkViewModel()
        {
            CandidateBulkUploadModel = new List<CandidateBulkModel>();
        }

        public List<CandidateBulkModel> CandidateBulkUploadModel { get; set; }
        public int TotalCount { get; set; }
    }
    public class CandidateBulkModel
    {
        public string SerialNumber { get; set; }
        public string CandidateName { get; set; }
        public string JobName { get; set; }
        public string JobTitle { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Feedback { get; set; }
        public string Level1ManagerId { get; set; }
        public string Level1Result { get; set; }

        public string Level2ManagerId { get; set; }
        public string Level2Result { get; set; }
        public string Level3ManagerId { get; set; }
        public string Level3Result { get; set; }
       
    }
}
