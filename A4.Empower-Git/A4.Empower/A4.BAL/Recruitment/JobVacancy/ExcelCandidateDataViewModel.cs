using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class ExcelCandidateDataViewModel
    {
        public int Id { get; set; }
        public string CandidateName { get; set; }
        public string JobName { get; set; }
        public string JobTitle { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Feedback { get; set; }
        public int Level1ManagerId { get; set; }
        public string Level1Result { get; set; }
        public int Level2ManagerId { get; set; }
        public string Level2Result { get; set; }
        public int Level3ManagerId { get; set; }
        public string Level3Result { get; set; }
        public int Level4ManagerId { get; set; }
        public string Level4Result { get; set; }
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
    }
}
