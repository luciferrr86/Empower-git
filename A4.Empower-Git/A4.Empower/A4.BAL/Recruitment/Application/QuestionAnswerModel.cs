using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class QuestionAnswerModel
    {
        public QuestionAnswerModel()
        {
            ScreeningQuestionList = new List<QuestionAnswer>();
            JobHRKpiList = new List<HRKpiModel>();
        }
        public List<QuestionAnswer> ScreeningQuestionList { get; set; }
        public List<HRKpiModel> JobHRKpiList { get; set; }
    }
    public class QuestionAnswer {
        public string JobQuestionId { get; set; }
        public string Question { get; set; }
        public string Weightage { get; set; }
        public string ObtainedWeightage { get; set; }
        public string LevelSkillQuestionId { get; set; }
    }
    public class QuestionAnswerSkillModel
    {
        public QuestionAnswerSkillModel()
        {
            QuestionAnswerSkillList = new List<QuestionAnswerSkill>();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public List<QuestionAnswerSkill> QuestionAnswerSkillList { get; set; }
    }
    public class QuestionAnswerSkill: QuestionAnswer
    {
        public string ManagerName { get; set; }
    }

}
