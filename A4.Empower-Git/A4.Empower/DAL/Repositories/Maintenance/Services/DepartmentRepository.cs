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
   public class DepartmentRepository : Repository<FunctionalDepartment>, IDepartmentRepository
    {
        public DepartmentRepository(DbContext context) : base(context)
        {

        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

    
        public override FunctionalDepartment Get(Guid id)
        {
            var department = _appContext.FunctionalDepartment.Where(e => e.Id == id && e.IsActive == true).FirstOrDefault();
            return department;
        }

        public override IEnumerable<FunctionalDepartment> GetAll()
        {
            var listDepartment = _appContext.FunctionalDepartment.AsQueryable().Where(c => c.IsActive);
            return listDepartment;
        }

        public override void Add(FunctionalDepartment entity)
        {
          
                base.Add(entity);
                _context.SaveChanges();
           
        }

        public override void Update(FunctionalDepartment entity)
        {
            base.Update(entity);
            _context.SaveChanges();
        }

        public override void Remove(FunctionalDepartment entity)
        {
            base.Remove(entity);
            _context.SaveChanges();
        }

        public PagedList<FunctionalDepartment> GetAllDepartment(string name = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var listDepartment = _appContext.FunctionalDepartment.AsQueryable().Where(c => c.IsActive);
            if (!string.IsNullOrWhiteSpace(name) && name != "undefined")
                listDepartment = listDepartment.Where(c => c.Name.Contains(name));
            listDepartment = listDepartment.OrderBy(c => c.Name).ThenBy(c => c.Id);
            return new PagedList<FunctionalDepartment>(listDepartment, pageIndex, pageSize);
        }


     
    }
}
