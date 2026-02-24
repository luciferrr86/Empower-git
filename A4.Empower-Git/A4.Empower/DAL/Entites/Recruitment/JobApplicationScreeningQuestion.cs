using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace A4.DAL.Entites
{
    public class JobApplicationScreeningQuestion:AuditableEntity
    {
        #region Constructor

        public JobApplicationScreeningQuestion()
        {
            Id = Guid.NewGuid();
        }

        #endregion

        #region ForeignKeyRelaton

        /// <summary>
        /// 
        /// </summary>
        public Guid JobApplicationId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public JobApplication JobApplication { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public Guid JobScreeningQuestionId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public JobScreeningQuestion JobScreeningQuestions { get; set; }


        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }


        public string ObtainedWeightage { get; set; }

        public string Weightage { get; set; }

        [NotMapped]
        public string Question { get; set; }
        #endregion
    }
}
