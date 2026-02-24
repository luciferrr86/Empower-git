using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
   
    public class DirectoryViewModel
    {
        public DirectoryViewModel()
        {
            DirectoryListModel = new List<DirectoryModel>();
        }
        public List<DirectoryModel> DirectoryListModel { get; set; }
        public int TotalCount { get; set; }
    }

    public class DirectoryModel
    {
        public Guid Id { get; set; }


        public string Name { get; set; }


        public string Designation { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
