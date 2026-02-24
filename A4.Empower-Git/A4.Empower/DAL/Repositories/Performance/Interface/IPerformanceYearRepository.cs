using A4.BAL;
using A4.DAL.Entites;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Repositories
{
   public interface IPerformanceYearRepository:IRepository<PerformanceYear>
    {
        List<MailList> PerformanceInvitation();
    }
}
