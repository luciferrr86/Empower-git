using A4.DAL.Entites.Maintenance;
using DAL;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;


namespace A4.DAL.Repositories
{
    public class SalaryPartRepository : Repository<SalaryPart>, ISalaryPartRepository
    {
        public SalaryPartRepository(DbContext context):base(context)
        {

        }

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

       

    }
}
