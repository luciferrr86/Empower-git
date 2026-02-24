using A4.DAL.Entites;
using A4.DAL.Entites.Recruitment;
using DAL;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Repositories
{ 
  public  class ExcelCandidateDataRepository : Repository<ExcelCandidateData>, IExcelCandidateDataRepository
    {
        public ExcelCandidateDataRepository(DbContext context) : base(context)
        {
        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        //public override ExcelCandidateData Get(int id)
        //{
        //    var excelCandidateData = _appContext.ExcelCandidateData.Where(e => e.Id == id).FirstOrDefault();
        //    return excelCandidateData;
        //}

        public override void Add(ExcelCandidateData entity)
        {
            base.Add(entity);
            _context.SaveChanges();
        }

        public override void Update(ExcelCandidateData entity)
        {
            base.Update(entity);
            _context.SaveChanges();
        }

        public override void Remove(ExcelCandidateData entity)
        {
            base.Remove(entity);
            _context.SaveChanges();
        }

        public IList<ExcelCandidateData> GetExcelCandidateData()
        {
            var excelCandidateList = _appContext.ExcelCandidateData.ToList();
            return excelCandidateList;
        }
        public override ExcelCandidateData GetById(int id)
        {
            var excelCandidate = _appContext.ExcelCandidateData.AsQueryable().Where(q => q.Id == id).FirstOrDefault();
            return excelCandidate;
        }
    }
}
