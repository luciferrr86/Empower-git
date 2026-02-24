using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class ApplicationFormViewModel
    {
        public ApplicationFormViewModel()
        {
            QuestionListModel = new List<JobScreeningQuestions>();
        }
        public string JobType { get; set; }

        public string JobTitle { get; set; }

        public string JobLocation { get; set; }

        public string PublishedDate { get; set; }

        public List<JobScreeningQuestions> QuestionListModel { get; set; }
    }

    //public class QuestionModel
    //{
    //    public QuestionModel()
    //    {
    //        OptionListModel = new List<OptionModel>();
    //    }
    //    public string QuestionId { get; set; }
    //    public string Question { get; set; }
    //    public List<OptionModel> OptionListModel { get; set; }
    //}

    //public class OptionModel
    //{
    //    public string OptionId { get; set; }

    //    public string Options { get; set; }
    //}

    //public class QuestionModel
    //{
    //    public QuestionModel()
    //    {
    //        OptionListModel = new List<OptionModel>();
    //    }
    //    public List<OptionModel> OptionListModel { get; set; }
    //}

    //public class OptionModel
    //{
    //    public string OptionId { get; set; }
    //    public string Question { get; set; }

    //    public string Options { get; set; }
    //}

}
