using A4.DAL.Entites;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Repositories
{
   public  interface ISalesStatusRepository : IRepository<SalesStatus>
    {
        //PagedList<LeavePeriod> GetAllLeavePeriod(string name = "", int pageIndex = 0, int pageSize = int.MaxValue);
        //Guid GetLeavePeriodId();
        //LeavePeriod GetleavePeriodRecord();
        //bool CheckLeavePeriod();
    }
}
