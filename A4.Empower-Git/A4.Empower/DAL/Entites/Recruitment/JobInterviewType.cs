using A4.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Entites
{
    public class JobInterviewType: AuditableEntity
    {
        #region Constructor

        public JobInterviewType()
        {
            Id = Guid.NewGuid();
        }

        #endregion

     

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        #endregion
    }
}
