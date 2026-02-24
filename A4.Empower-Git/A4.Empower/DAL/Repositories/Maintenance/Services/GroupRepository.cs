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
   public class GroupRepository : Repository<FunctionalGroup>, IGroupRepository
    {
        public GroupRepository(DbContext context) : base(context)
        {

        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        //public override IEnumerable<FunctionalGroup> GetAll()
        //{
        //    var listGroup = _appContext.FunctionalGroup.AsQueryable();         
        //    return listGroup;
        //}

        public override FunctionalGroup Get(Guid id)
        {
            var group = _appContext.FunctionalGroup.FirstOrDefault(e => e.Id == id && e.IsActive == true);
            return group;
        }
    

        public override void Add(FunctionalGroup entity)
        {
            base.Add(entity);
            _context.SaveChanges();
        }

        public override void Update(FunctionalGroup entity)
        {
            base.Update(entity);
            _context.SaveChanges();
        }

        public override void Remove(FunctionalGroup entity)
        {
            base.Remove(entity);
            _context.SaveChanges();
        }

        public PagedList<FunctionalGroup> GetAllGroup(string name = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var listGroup = _appContext.FunctionalGroup.AsQueryable().Where(c => c.IsActive);
            if (!string.IsNullOrWhiteSpace(name) && name != "undefined")
                listGroup = listGroup.Where(c => c.Name.Contains(name));
            listGroup = listGroup.OrderBy(c => c.Name).ThenBy(c => c.Id);
            return new PagedList<FunctionalGroup>(listGroup, pageIndex, pageSize);
        }


    }
}
