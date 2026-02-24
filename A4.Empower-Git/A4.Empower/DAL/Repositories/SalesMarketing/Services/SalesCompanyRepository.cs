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
   public  class SalesCompanyRepository : Repository<SalesCompany>, ISalesCompanyRepository
    {
        public SalesCompanyRepository(DbContext context) : base(context)
        {

        }

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public PagedList<SalesCompany> GetAllSalesCompany(string name = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var listSalesCompany = _appContext.SalesCompany.AsQueryable().Where(c => c.IsActive );
            if (!string.IsNullOrWhiteSpace(name) && name != "undefined")
                listSalesCompany = listSalesCompany.Where(c => c.ComapnyName.Contains(name));
            listSalesCompany = listSalesCompany.OrderBy(c => c.ComapnyName).ThenBy(c => c.Id);
            return new PagedList<SalesCompany>(listSalesCompany, pageIndex, pageSize);
        }

    }
}
