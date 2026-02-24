using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class FunctionalDesignationViewModel
    {
        public FunctionalDesignationViewModel()
        {
            FunctionalDesignationModel = new List<FunctionalDesignationModel>();
        }

        public List<FunctionalDesignationModel> FunctionalDesignationModel { get; set; }
        public int TotalCount { get; set; }
    }
    public class FunctionalDesignationModel {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
