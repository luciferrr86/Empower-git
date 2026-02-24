using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{
   public class Professional:AuditableEntity
    {
        public Professional()
        {
            Id = new Guid();
        }
        #region Properties
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string Designation { get; set; }
        public string ProfileDesc { get; set; }
        public DateTime DOJ { get; set; }
        public DateTime DOR { get; set; }
        #endregion

        #region ForeignKeyRelation
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
        #endregion
    }
}
