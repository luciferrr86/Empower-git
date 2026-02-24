using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class BandViewModel
    {
        public BandViewModel()
        {
            BandModel = new List<BandModel>();
        }

        public List<BandModel> BandModel { get; set; }
        public int TotalCount { get; set; }
    }
    public class BandModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
