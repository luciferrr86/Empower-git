using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{
    public class JobScreeningQuestion:AuditableEntity
    {
        #region Constructor

        public JobScreeningQuestion()
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

        #region Children

        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }

        public string Questions { get; set; }

        public int Weightage { get; set; }

        public string ControlType { get; set; }

        public string Option1 { get; set; }

        public string Option2 { get; set; }

        public string Option3 { get; set; }

        public string Option4 { get; set; }


        public bool ChkOption1 { get; set; }


        public bool ChkOption2 { get; set; }


        public bool ChkOption3 { get; set; }


        public bool ChkOption4 { get; set; }

        public bool bIsRequired { get; set; }

        public ICollection<JobApplicationScreeningQuestion> JobApplicationScreeningQuestions { get; set; }

        #endregion
    }
}
