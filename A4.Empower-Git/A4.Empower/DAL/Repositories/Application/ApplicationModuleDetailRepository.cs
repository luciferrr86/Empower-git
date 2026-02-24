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
    public class ApplicationModuleDetailRepository : Repository<ApplicationModuleDetail>, IApplicationModuleDetailRepository
    {
        public ApplicationModuleDetailRepository(DbContext context) : base(context)
        {

        }

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public override ApplicationModuleDetail Get(Guid id)
        {
            var appSubModuleDetail = _appContext.ApplicationModuleDetail.Where(e => e.Id == id && e.IsActive == true).FirstOrDefault();
            return appSubModuleDetail;
        }

        public void SwitchSubModuleSetting(Guid id, bool togval)
        {
            var applicationModuleDetail = _appContext.ApplicationModuleDetail.Where(x => x.Id == id).FirstOrDefault();
            if (applicationModuleDetail != null)
            {
                applicationModuleDetail.IsActive = togval;
                _appContext.ApplicationModuleDetail.Update(applicationModuleDetail);
                var appModule = _appContext.ApplicationModule.Where(c => c.Id == applicationModuleDetail.ApplicationModuleId).FirstOrDefault();
                if (appModule!=null)
                {
                    if (!appModule.IsActive)
                    {
                        appModule.IsActive = togval;
                        _appContext.ApplicationModule.Update(appModule);
                    }
                }
                _appContext.SaveChanges();
            }
        }

    }
}
