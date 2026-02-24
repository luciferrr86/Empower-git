using A4.BAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class JobViewModel
    {

        public string Id { get; set; }

        public int NoOfvacancies { get; set; }

        public string JobTitle { get; set; }

        public string JobLocation { get; set; }

        public string Experience { get; set; }

        public string SalaryRange { get; set; }

        public string Currency { get; set; }

        public string JobDescription { get; set; }

        public string JobRequirements { get; set; }

        public string JobType { get; set; }

        public bool bIsClosed { get; set; }
        public bool bIsPublished { get; set; }
        public string PublishedDate { get; set; }

    }
    
}
