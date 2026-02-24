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
    public class DesignationRepository : Repository<FunctionalDesignation>, IDesignationRepository
    {
        public DesignationRepository(DbContext context) : base(context)
        {

        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;



        public override FunctionalDesignation Get(Guid id)
        {
            var designation = _appContext.FunctionalDesignation.Where(e => e.Id == id && e.IsActive == true).FirstOrDefault();
            return designation;
        }
        public override void Add(FunctionalDesignation entity)
        {
            base.Add(entity);
            _context.SaveChanges();
        }

        public override void Update(FunctionalDesignation entity)
        {
            base.Update(entity);
            _context.SaveChanges();
        }

        public override void Remove(FunctionalDesignation entity)
        {
            base.Remove(entity);
            _context.SaveChanges();
        }

        public PagedList<FunctionalDesignation> GetAllDesignation(string name = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var listDesignation = _appContext.FunctionalDesignation.AsQueryable().Where(c => c.IsActive);
            if (!string.IsNullOrWhiteSpace(name) && name != "undefined")
                listDesignation = listDesignation.Where(c => c.Name.Contains(name));
            listDesignation = listDesignation.OrderBy(c => c.Name).ThenBy(c => c.Id);
            return new PagedList<FunctionalDesignation>(listDesignation, pageIndex, pageSize);
        }
    }
}
