using A4.BAL.Maintenance;
using A4.DAL.Entites;
using A4.DAL.Entites.Maintenance;
using DAL;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace A4.DAL.Repositories
{
   public class EmployeeSalaryRepository : Repository<EmployeeSalary>, IEmployeeSalaryRepository
    {
       

        public EmployeeSalaryRepository(DbContext context) : base(context)
        {

        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

       public List<EmployeeSalary> GetEmpMonthlySalaryDetail(Guid? employeeId, int month, int year)
        {
            var salary = new List<EmployeeSalary>();

            if (employeeId.HasValue)
            {
                salary = _appContext.EmployeeSalary.Where(m => m.EmployeeId == employeeId.Value && m.Month == month && m.Year == year).ToList();

            }
            else
            {
                salary = _appContext.EmployeeSalary.Where(m => m.Month == month && m.Year == year).ToList();

            }
            return salary;
        }

       


        //public override void Add(EmployeeSalary entity)
        //{
        //    base.Add(entity);
        //    _context.SaveChanges();
        //}

        //public override void Update(EmployeeSalary entity)
        //{
        //    base.Update(entity);
        //    _context.SaveChanges();
        //}

        //public override void Remove(EmployeeSalary entity)
        //{
        //    base.Remove(entity);
        //    _context.SaveChanges();
        //}

        //public PagedList<EmployeeSalary> GetAllGroup(string name = "", int pageIndex = 0, int pageSize = int.MaxValue)
        //{
        //    var listGroup = _appContext.EmployeeSalary.AsQueryable().Where(c => c.IsActive);
        //    if (!string.IsNullOrWhiteSpace(name) && name != "undefined")
        //        listGroup = listGroup.Where(c => c.Name.Contains(name));
        //    listGroup = listGroup.OrderBy(c => c.Name).ThenBy(c => c.Id);
        //    return new PagedList<EmployeeSalary>(listGroup, pageIndex, pageSize);
        //}


    }
}
