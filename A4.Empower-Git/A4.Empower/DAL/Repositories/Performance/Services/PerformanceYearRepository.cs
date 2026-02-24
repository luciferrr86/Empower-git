using A4.BAL;
using A4.DAL.Entites;
using DAL;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Repositories
{
    public class PerformanceYearRepository : Repository<PerformanceYear>, IPerformanceYearRepository
    {

        public PerformanceYearRepository(DbContext context) : base(context)
        {

        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        private bool IsInitailRatingEnabled()
        {
            bool flag = false;
            var performanceConfig = _appContext.ApplicationModuleDetail.Where(s => s.ConfigType == "EnableIntialRating").FirstOrDefault();
            if (performanceConfig != null)
            {
                flag = performanceConfig.IsActive;
            }
            return flag;
        }
        public List<MailList> PerformanceInvitation()
        {
            var mailList = new List<MailList>();
            var performanceYearId =new Guid();
            bool isActiveInitialRating = IsInitailRatingEnabled();
            var activeEmployee = _appContext.Employee.Where(k => k.IsActive == true);
            var isYearExist = _appContext.PerformanceYear.Where(a => a.IsCompleted == false && a.IsYearActive == false).FirstOrDefault();
            if (isYearExist != null)
            {
                var performanceYear = new PerformanceYear();
                DateTime dt = new DateTime(Convert.ToInt32(isYearExist.Year), DateTime.Now.Month, 1);
                performanceYear.Year = dt.AddMonths(12).Year.ToString();
                performanceYear.IsYearActive = false;
                performanceYear.IsCompleted = false;
                //performanceYear.NextYearId = new Guid();
                performanceYearId = performanceYear.Id;
                _appContext.PerformanceYear.Add(performanceYear);

                isYearExist.IsYearActive = true;
                isYearExist.NextYearId = performanceYear.Id;
                _appContext.PerformanceYear.Update(isYearExist);
               
            }
            else
            {
                var performanceYear = new PerformanceYear();
                performanceYear.Year = DateTime.Now.Year.ToString();
                performanceYear.IsYearActive = true;
                performanceYear.IsCompleted = false;
                performanceYearId = performanceYear.Id;                

                var performanceNextYear = new PerformanceYear();
                performanceNextYear.Year = DateTime.Now.AddMonths(12).Year.ToString();
                performanceNextYear.IsYearActive = false;
                performanceNextYear.IsCompleted = false;

                performanceYear.NextYearId = performanceNextYear.Id;

                _appContext.PerformanceYear.Add(performanceYear);
                _appContext.PerformanceYear.Add(performanceNextYear);                

                if (activeEmployee.Count() > 0)
                {
                    foreach (var item in activeEmployee)
                    {
                        var manager = _appContext.Employee.Where(j => j.ManagerId == item.Id && j.IsActive == true);
                        if (manager.Count() > 0)
                        {
                            var goalMainNextYear = new PerformanceGoalMain();
                            goalMainNextYear.ManagerId = item.Id;
                            if (isActiveInitialRating)
                            {
                                goalMainNextYear.PerformanceStatusId = GetStatus("NotStarted");
                            }
                            else
                            {
                                goalMainNextYear.PerformanceStatusId = GetStatus("AwaitingToReleaseGoal");
                            }
                            goalMainNextYear.PerformanceYearId = performanceNextYear.Id;
                            goalMainNextYear.IsManagerActive = true;
                            _appContext.PerformanceGoalMain.Add(goalMainNextYear); 
                        }
                        
                    }
                }
            }
            if (activeEmployee.Count() > 0)
            {
                foreach (var item in activeEmployee)
                {
                    var manager = _appContext.Employee.Where(j => j.ManagerId == item.Id && j.IsActive == true);
                    if (manager.Count() > 0)
                    {
                        var goalMain = new PerformanceGoalMain();
                        goalMain.ManagerId = item.Id;
                        if (isActiveInitialRating)
                        {
                            goalMain.PerformanceStatusId = GetStatus("NotStarted");
                        }
                        else
                        {
                            goalMain.PerformanceStatusId = GetStatus("AwaitingToReleaseGoal");
                        }
                        goalMain.PerformanceYearId = performanceYearId;
                        goalMain.IsManagerActive = true;
                        _appContext.PerformanceGoalMain.Add(goalMain);
                    }
                }
            }
            var performanceConfig = _appContext.PerformanceConfig.FirstOrDefault();
            if (performanceConfig!=null)
            {
                performanceConfig.IsPerformanceStart = true;
                _appContext.PerformanceConfig.Update(performanceConfig);
            }
            //Mail will be sent to all manager to start the performance process
            _appContext.SaveChanges();
            if (activeEmployee.Count() > 0)
            {
                foreach (var item in activeEmployee)
                {
                    var userDetails = _appContext.Users.Where(m => m.Id == item.UserId && m.IsActive == true).FirstOrDefault();
                    mailList.Add(new MailList { MailID = userDetails.Email,Name=userDetails.FullName });
                    //string message = PerformanceEmailTemplate.Invitation(userDetails.FullName, userDetails.Email, "year", "lastDate");
                    //(bool success, string errorMsg) response = await _emailer.SendEmailAsync(userDetails.FullName, userDetails.Email, "Email from A4.Empower", message);
                }
            }
            return mailList;
        }

        private Guid GetStatus(string type)
        {
            var statusId = new Guid();
            var status = _appContext.PerformanceStatus.Where(u => u.Type == type).FirstOrDefault();
            if (status != null)
            {
                statusId = status.Id;
            }
            return statusId;
        }

       
    }
}
