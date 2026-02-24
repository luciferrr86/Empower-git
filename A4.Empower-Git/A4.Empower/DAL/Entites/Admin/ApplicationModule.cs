using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Entites
{
   public class ApplicationModule : AuditableEntity
    {
        public ApplicationModule()
        {
            Id = Guid.NewGuid();
        }

        #region Children 

        private List<ApplicationModuleDetail> _applicationModuleDetail = new List<ApplicationModuleDetail>();

        public List<ApplicationModuleDetail> ApplicationModuleDetail
        {
            get => _applicationModuleDetail;
            set => _applicationModuleDetail = (value ?? new List<ApplicationModuleDetail>()).Where(p => p != null).ToList();
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
        public string ModuleName { get; set; }

        public string Type { get; set; }

        #endregion
    }
}
