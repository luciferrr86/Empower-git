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
    public class PerformanceGoalMeasureRepository : Repository<PerformanceGoalMeasure>, IPerformanceGoalMeasureRepository
    {
        public PerformanceGoalMeasureRepository(DbContext context) : base(context)
        {

        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public bool DeleteGoalMeasure(string measureGoalId, bool isNextYearGoal)
        {
            bool flag = false;
            var objDelMeasure = _appContext.PerformanceGoalMeasure.Find(Guid.Parse(measureGoalId));
            if (objDelMeasure != null)
            {
               
                _appContext.PerformanceGoalMeasure.Remove(objDelMeasure);

                var enpYearGoalDetail = _appContext.PerformanceEmpYearGoalDetail.Where(m => m.PerformanceGoalMeasureId == objDelMeasure.Id).ToList();
                if (enpYearGoalDetail.Count > 0)
                {
                    _appContext.PerformanceEmpYearGoalDetail.RemoveRange(enpYearGoalDetail);
                }

                //if (isNextYearGoal)
                //{
                //    var objNextYearGoal = _appContext.PerformanceEmpGoalNextYear.Find(" objPerformanceGoalMeasure = " + objDelMeasure.iID + " and bIsActive = " + true).FirstOrDefault();
                //    if (objNextYearGoal != null)
                //    {
                //        objNextYearGoal.bIsActive = false;
                //        objNextYearGoal.Save();
                //    }
                //}
                flag = true;
            }
            _appContext.SaveChanges();
            return flag;
        }
    }
}
