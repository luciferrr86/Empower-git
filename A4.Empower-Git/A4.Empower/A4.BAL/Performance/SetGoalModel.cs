using System;
using System.Collections.Generic;

namespace A4.BAL
{
    public class SetGoal
    {
        public SetGoal()
        {
            SetIndividual = new List<string>();
            SetFunctionalGroupId = new List<string>();
        }
        public string PerformanceGoalId { get; set; }

        // public int EmployeeSetGoalId { get; set; }
        public List<string> SetIndividual { get; set; }
        public List<string> SetFunctionalGroupId { get; set; }


        // public int ID { get; set; }

        public string PerformanceGoalMeasureId { get; set; }

        public string FirstQuadrantId { get; set; }
        public string SecondQuadrantId { get; set; }


        public string GoalMeasure { get; set; }
        //public bool bIsReleased { get; set; }      
        public string LevelId { get; set; }


        

       // public int Counter { get; set; }
        //public Guid YearId { get; set; }
        //public bool IsNewTemplate { get; set; }

    }
    public class GetSetGoal:SetGoal
    {
        public List<DropDownList> SelectedPerformanceGoal { get; set; }
        public List<DropDownList> SelectedFunctionalGroup { get; set; }
        public List<DropDownList> SelectedQuadrant { get; set; }
        public List<DropDownList> SelectedLevel { get; set; }
        public List<DropDownList> SelectedIndividualManager { get; set; }
        
    }

  


    public class SetGoalModel
    {
        //public SetGoalModel()
        //{
        //    lstSetGoal = new List<SetGoal>();
        //}

        //public List<SetGoal> lstSetGoalNextYear { get; set; }
        public string SearchFunGroupId { get; set; }

        public string SearchLevelId { get; set; }

        public string SearchIndiGroupId { get; set; }

        //public Guid CurrentYearId { get; set; }
        //public int NextYearId { get; set; }

        //public int Count { get; set; }
        //public int EmpId { get; set; }
    }


    public class GetSetGoalModel: SetGoalModel
    {
        public List<DropDownList> SearchIndividualGroup { get; set; }
        public List<DropDownList> SearchLevel { get; set; }
        public List<DropDownList> SearchFunctionalGroup { get; set; }
        public bool IsCEO { get; set; }
        public bool IsManagerRealeased { get; set; }
        public bool IsGoalReleased { get; set; }
        public bool IsPerformanceStarted { get; set; }        
        public List<GetSetGoal> lstSetGoal { get; set; }
    }

    public class PostSetGoalModel: SetGoalModel
    {
        public PostSetGoalModel()
        {
            lstSetGoal = new List<SetGoal>();
        }
        public List<SetGoal> lstSetGoal { get; set; }
    }
   
   
    public class Goal
    {
        public string GoalId { get; set; }
        public string GoalName { get; set; }
    }
   

    public class EmployeeByLevel
    {
        public Guid EmpId { get; set; }
        public bool Active { get; set; }
        public Guid ManagerId { get; set; }
        public int EmpLevel { get; set; }
        public Guid Root { get; set; }
        public string Name { get; set; }
        public string FunctionalDesignation { get; set; }
        public string FunctionalGroup { get; set; }
    }

    public class EmpPerformanceGoalMeasure
    {      
        public Guid QuadID { get; set; }
    }
    public class ReleaseGoalMessage
    {
        public int Status { get; set; }
        public List<string> lstEmpName { get; set; }
        public List<MailList> mailList { get; set; }
        public List<MailList> mailListCEO { get; set; }
    }
}
