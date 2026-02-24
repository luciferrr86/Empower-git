using A4.DAL.Entites;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Repositories
{
    public interface IApplicationModuleRepository : IRepository<ApplicationModule>
    {
        List<ApplicationModule> GetModule();

        void SwitchModuleSetting(Guid id, bool togval);

        Employee GetClientDetails();
    }
}
