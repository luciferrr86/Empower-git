using A4.BAL;
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
    public class EmailDirectoryRepository : Repository<EmailDirectory>, IEmailDirectoryRepository
    {
        public EmailDirectoryRepository(DbContext context) : base(context)
        {
        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public override EmailDirectory Get(Guid id)
        {
            var data = _appContext.EmailDirectory.Where(e => e.Id == id && e.IsActive == true).FirstOrDefault();
            return data;
        }
        public override void Update(EmailDirectory entity)
        {
            base.Update(entity);
            _context.SaveChanges();
        }

        public override void Remove(EmailDirectory entity)
        {
            base.Remove(entity);
            _context.SaveChanges();
        }

       

        public void CreateEmail(EmailDirectory entity)
        {
            _appContext.EmailDirectory.Add(entity);
            
            _appContext.SaveChanges();
        }


        public EmailDirectory GetById(Guid id)
        {
            var dir = _appContext.EmailDirectory.Where(e => e.Id == id && e.IsActive == true).ToList().FirstOrDefault();
            return dir;
            
        }
        public PagedList<EmailDirectory> GetAllDirectory(string name = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var listDirectory = _appContext.EmailDirectory.AsQueryable().Where(c => c.IsActive);
            if (!string.IsNullOrWhiteSpace(name) && name != "undefined")
                listDirectory = listDirectory.Where(c => c.Name.Contains(name));
            listDirectory = listDirectory.OrderBy(c => c.Name).ThenBy(c => c.Id);
            return new PagedList<EmailDirectory>(listDirectory, pageIndex, pageSize);
        }
    }
}
