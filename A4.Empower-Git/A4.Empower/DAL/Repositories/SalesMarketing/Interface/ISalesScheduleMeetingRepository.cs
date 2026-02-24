using A4.BAL;
using A4.DAL.Entites;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Repositories
{
   public  interface ISalesScheduleMeetingRepository : IRepository<SalesScheduleMeeting>
    {
        PagedList<SalesScheduleMeeting> GetAllSalesScheduleMeeting(Guid salesCompanyId,string name = "", int pageIndex = 0, int pageSize = int.MaxValue);

        List<ScheduleDropdownList> GetInternalPersonList();
    }
}
