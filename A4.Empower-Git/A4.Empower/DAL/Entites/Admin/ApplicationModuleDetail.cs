using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{
   public class ApplicationModuleDetail : AuditableEntity
    {
        public ApplicationModuleDetail()
        {
            Id = Guid.NewGuid();
        }

        #region ForeignKeyRelation
        /// <summary>
        /// 
        /// </summary>
        public Guid ApplicationModuleId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ApplicationModule ApplicationModule { get; set; }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SubModuleName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ConfigType { get; set; }

        #endregion
    }
}
