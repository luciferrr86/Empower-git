using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace A4.DAL.Entites
{
   public class EmployeeProxy : AuditableEntity
    {
        #region Constructor
        public EmployeeProxy()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region ForeignKeyRelation

        /// <summary>
        /// 
        /// </summary>
        /// 



    

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid ProxyForId { get; set; }

        public virtual Employee ProxyFor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }
        #endregion



    }
}
