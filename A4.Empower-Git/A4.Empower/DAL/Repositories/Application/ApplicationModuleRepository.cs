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
    public class ApplicationModuleRepository : Repository<ApplicationModule>, IApplicationModuleRepository
    {
        public ApplicationModuleRepository(DbContext context) : base(context)
        {

        }

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public override ApplicationModule Get(Guid id)
        {
            var appModule = _appContext.ApplicationModule.Where(e => e.Id == id && e.IsActive == true).FirstOrDefault();
            return appModule;
        }

        public List<ApplicationModule> GetModule()
        {
            var query = _appContext.ApplicationModule.OrderBy(b => b.Id).Select(b => new ApplicationModule
            {
                Id = b.Id,
                ModuleName = b.ModuleName,
                IsActive = b.IsActive,
                ApplicationModuleDetail = b.ApplicationModuleDetail.Select(st => new ApplicationModuleDetail { Id = st.Id, SubModuleName = st.SubModuleName, IsActive = st.IsActive }).ToList()
            }).ToList();
            return query.ToList();           

        }

        public void SwitchModuleSetting(Guid id, bool togval)
        {
            var applicationModule = _appContext.ApplicationModule.Where(x => x.Id == id).FirstOrDefault();
            if (applicationModule != null)
            {
                applicationModule.IsActive = togval;
                _appContext.ApplicationModule.Update(applicationModule);
                if (!togval)
                {
                    var appModuleDetail = _appContext.ApplicationModuleDetail.Where(l => l.ApplicationModuleId == id);
                    if (appModuleDetail.Count() > 0)
                    {
                        foreach (var item in appModuleDetail)
                        {
                            item.IsActive = togval;
                            _appContext.ApplicationModuleDetail.Update(item);
                        }                       
                    }
                }                
                _appContext.SaveChanges();
            }
        }
       
        public Employee GetClientDetails()
        {
            var query = from designation in _appContext.FunctionalDesignation.Where(m => m.Name == "CEO" && m.IsActive == true)
                        join employee in _appContext.Employee on designation.Id equals employee.DesignationId
                        join user in _appContext.Users on employee.UserId equals user.Id
                        select new Employee
                        {
                            ApplicationUser = new ApplicationUser { FullName = user.FullName, Email = user.Email, PhoneNumber = user.PhoneNumber },
                            FunctionalGroup = new FunctionalGroup { Name = employee.FunctionalGroup.Name, FunctionalDepartment = new FunctionalDepartment { Name = employee.FunctionalGroup.FunctionalDepartment.Name } }
                        };

            return query.FirstOrDefault();
        }
    }
}
