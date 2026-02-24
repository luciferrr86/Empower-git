using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{
    public class JobHRQuestion:AuditableEntity
    {
        #region Constructor

        public JobHRQuestion()
        {
            Id = Guid.NewGuid();
        }

        #endregion

        #region ForeignKeyRelaton
        /// <summary>
        /// 
        /// </summary>
        public Guid JobVacancyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public JobVacancy JobVacancy { get; set; }

        #endregion

        #region Properties

        public Guid Id { get; set; }

        public string Question { get; set; }

        public int Weightage { get; set; }

        public ICollection<JobApplicationHRQuestions> JobApplicationHRQuestions { get; set; }

        #endregion
    }
}
