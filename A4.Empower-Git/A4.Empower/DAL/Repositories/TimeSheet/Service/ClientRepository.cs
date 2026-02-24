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
    public class ClientRepository : Repository<TimesheetClient>, IClientRepository
    {
        public ClientRepository(DbContext context) : base(context)
        {
        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public PagedList<TimesheetClient> GetAllClient(string name = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var listClient = _appContext.TimesheetClient.AsQueryable().Where(c => c.IsActive);
            if (!string.IsNullOrWhiteSpace(name) && name != "undefined")
                listClient = listClient.Where(c => c.Name.Contains(name));
            listClient = listClient.OrderBy(c => c.Name).ThenBy(c => c.Id);
            return new PagedList<TimesheetClient>(listClient, pageIndex, pageSize);
        }

    }
}
