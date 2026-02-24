
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites.Maintenance
{
   public class SalaryComponent : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsEarnings { get; set; }
        public bool IsMonthly { get; set; }
        public bool IsAllYearly { get; set; }
       
        #region Navigation Property
        public List<CtcOtherComponent> CtcOtherComponent { get; set; }
      
        #endregion
    }
}
