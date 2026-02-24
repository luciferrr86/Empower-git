using A4.DAL.Entites;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Repositories
{
   public interface IDepartmentRepository: IRepository<FunctionalDepartment>
    {

      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PagedList<FunctionalDepartment> GetAllDepartment(string name = "",int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
