using A4.Empower.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.Empower.ViewModels
{
    public class AdminClientModel
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string Department { get; set; }

        public string FunctionalGroup { get; set; }

        public string EmailId { get; set; }

        public string ContactNo { get; set; }

        public string[] Roles { get; set; }

        public string Title { get; set; }

        public string Designation { get; set; }

        public string Band { get; set; }

        public string Password { get; set; }
    }
}
