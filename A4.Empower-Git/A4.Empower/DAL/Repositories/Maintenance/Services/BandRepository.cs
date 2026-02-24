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
   public class BandRepository : Repository<Band>, IBandRepository
    {
        public BandRepository(DbContext context):base(context)
        {

        }

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public override Band Get(Guid id)
        {
            var band = _appContext.Band.Where(e => e.Id == id && e.IsActive == true).FirstOrDefault();
            return band;
        }

        public override void Add(Band entity)
        {
            base.Add(entity);
            _context.SaveChanges();
        }

        public override void Update(Band entity)
        {
            base.Update(entity);
            _context.SaveChanges();
        }

        public override void Remove(Band entity)
        {
            base.Remove(entity);
            _context.SaveChanges();
        }
     
        public PagedList<Band> GetAllBand(string name = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var listBand = _appContext.Band.AsQueryable().Where(c => c.IsActive); ;
            if (!string.IsNullOrWhiteSpace(name) && name != "undefined")
                listBand = listBand.Where(c => c.Name.Contains(name));
            listBand = listBand.OrderBy(c => c.Name).ThenBy(c => c.Id);
            return new PagedList<Band>(listBand, pageIndex, pageSize);
        }

      

    }
}
