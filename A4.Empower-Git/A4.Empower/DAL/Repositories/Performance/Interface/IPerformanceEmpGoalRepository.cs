using A4.BAL;
using A4.DAL.Entites;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Repositories
{
    public interface IPerformanceEmpGoalRepository : IRepository<PerformanceEmpGoal>
    {
        ReleaseGoalMessage ReleaseGoal(Guid empId, Guid yearId, PerformanceFeatures features);
        GoalViewModel GetCurrentYearGoal(EmployeeGoalDetail empGoal, bool isMidYearEnabled);
        //bool SaveGoalCurrentYear(GoalViewModel goalCurrentYear, EmployeeGoalDetail empDetail, Employee emp, bool isMidYearEnabled, string action);
        bool SaveCurrentYearGoal(GoalViewModel goalCurrentYear, EmployeeGoalDetail empDetail, Employee emp, bool isMidYearEnabled, bool isMidYearCompleted,string action);
        bool SaveMgrCurrentYearGoal(GoalViewModel goalCurrentYear, EmployeeGoalDetail empDetail, bool midYearEnabled, bool IsMidYearReviewCompleted, string button);
        RatingModel GetRating(EmployeeGoalDetail empGoal, bool isMidYearEnabled);
        bool SaveRating(RatingModel ratingModel, string name, EmployeeGoalDetail empDetail, bool midYearEnabled, bool isMidYearReviewCompleted);
        bool SaveMgrRating(RatingModel ratingModel, string name, EmployeeGoalDetail empDetail, Employee employee, bool midYearEnabled, bool isMidYearReviewCompleted);
    }
}
