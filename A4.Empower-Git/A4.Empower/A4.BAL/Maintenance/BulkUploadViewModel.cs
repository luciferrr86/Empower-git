using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class BulkUploadViewModel
    {
        public BulkUploadViewModel()
        {
            BulkUploadModel = new List<BulkUpload>();
        }        

        public List<BulkUpload> BulkUploadModel { get; set; }
        public int TotalCount { get; set; }
    }

    public class BulkUpload
    {
        public string FullName { get; set; }
        public string WorkEmailAddress { get; set; }
        public string FunctionalDepartment { get; set; }
        public string FunctionalGroup { get; set; }
        public string Designation { get; set; }
        public string PersonalEmailID { get; set; }
        public string Title { get; set; }
        public string ReportingHeadEmailId { get; set; }
        public string RollAccess { get; set; }
        public string Location { get; set; }
        public string DateofJoining { get; set; }        
        public string ReportingHeadName { get; set; }
        public string Band { get; set; }
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
    }
    
}
