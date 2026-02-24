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
   public class TimesheetConfigurationRepository  : Repository<TimesheetConfiguration>, ITimesheetConfigurationRepository
    {
        public TimesheetConfigurationRepository(DbContext context) : base(context)
        {
        }
        public ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public TimesheetConfiguration GetTimeSheetConfiguration()
        {
            var configuration = _appContext.TimesheetConfiguration.Where(m => m.IsActive == true).FirstOrDefault();
            return configuration;
        }
    }
}
