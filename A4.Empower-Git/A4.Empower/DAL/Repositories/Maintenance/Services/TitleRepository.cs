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
  public  class TitleRepository : Repository<FunctionalTitle>, ITitleRepository
    {
        public TitleRepository(DbContext context) : base(context)
        {
                
        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;     

        public override FunctionalTitle Get(Guid id)
        {
            var title = _appContext.FunctionalTitle.Where(e => e.Id == id && e.IsActive == true).FirstOrDefault();
            return title;
        }    

        public override void Add(FunctionalTitle entity)
        {
            base.Add(entity);
            _context.SaveChanges();
        }

        public override void Update(FunctionalTitle entity)
        {
            base.Update(entity);
            _context.SaveChanges();
        }

        public override void Remove(FunctionalTitle entity)
        {
            base.Remove(entity);
            _context.SaveChanges();
        }

        public PagedList<FunctionalTitle> GetAllTitle(string name = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var listTitle = _appContext.FunctionalTitle.AsQueryable().Where(c => c.IsActive);
            if (!string.IsNullOrWhiteSpace(name) && name != "undefined")
                listTitle = listTitle.Where(c => c.Name.Contains(name));           
            listTitle = listTitle.OrderBy(c => c.Name).ThenBy(c => c.Id);
            return new PagedList<FunctionalTitle>(listTitle, pageIndex, pageSize);
        }

      

    }
}
