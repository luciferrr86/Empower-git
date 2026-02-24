using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class MassSchedulingListViewModel
    {
        public MassSchedulingListViewModel()
        {
            MassSchedulingList = new List<massSchedulingListModel>();
        }
        public List<massSchedulingListModel> MassSchedulingList { get; set; }
        public int TotalCount { get; set; }
    }
    public class massSchedulingListModel
    {
        public string Id { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public string venue { get; set; }
    }
}
