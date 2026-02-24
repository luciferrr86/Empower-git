using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class FunctionalTitleViewModel
    {
        public FunctionalTitleViewModel()
        {
            FunctionalTitleModel = new List<FunctionalTitleModel>();
        }

        public List<FunctionalTitleModel> FunctionalTitleModel { get; set; }
        public int TotalCount { get; set; }
    }
    public class FunctionalTitleModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
