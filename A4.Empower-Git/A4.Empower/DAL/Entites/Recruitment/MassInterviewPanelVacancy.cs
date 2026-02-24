using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{
    public class MassInterviewPanelVacancy 
    {
        #region Constructor
        public MassInterviewPanelVacancy()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region ForeignKeyRelation

     

        /// <summary>
        /// 
        /// </summary>
        public Guid MangerId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Employee Employee { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public Guid PanelId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public MassInterviewPanel MassInterviewPanel { get; set; }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }
        #endregion

    }
}
