using A4.DAL.Entites;
using A4.DAL.Entites.Recruitment;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Repositories
{
  public interface IExcelCandidateDataRepository : IRepository<ExcelCandidateData>
    {
        IList<ExcelCandidateData> GetExcelCandidateData();
    }
}
