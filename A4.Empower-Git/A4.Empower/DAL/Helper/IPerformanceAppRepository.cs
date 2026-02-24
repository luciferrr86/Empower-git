using A4.BAL;
using A4.DAL.Entites;
using A4.DAL.Repositories;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL
{
   public interface IPerformanceAppRepository 
    {

        List<DropDownList> GetQuadrantList();

        List<DropDownList> GetPerformanceGoalNames(string userId, Guid yearId);

        bool CheckManagerRelease(string empId, Guid yearId);

        bool CheckEmployeePerformanceStart(string userId);

        bool CheckPerformanceStart();

        bool CheckIsCEO(string userId);

        PerformanceFeatures GetFeatures();

        bool CheckIsRatingSignedOff(string id,string userId, Guid currentYearId);

        EmployeeGoalDetail EmployeeGoalDetail(string userId, Guid currentYearId);
        
        List<DropDownList> GetHistoryList(Guid employeeId);

        EmployeeDetail GetEmployeeDetail(string userId);

        Employee GetEmployeeByUserId(string userId);

        PerformanceConfig GetPerformanceConfig();

        CheckSaveSubmit GetSaveSubmit(string empId, Guid yearId, bool isMidYearEnabled);

        bool CheckIsMidYearReviewCompleted(Guid currentYearId);

        List<TaskListModel> GetPerformanceTaskList(string id, Guid YearId);
    }
}
