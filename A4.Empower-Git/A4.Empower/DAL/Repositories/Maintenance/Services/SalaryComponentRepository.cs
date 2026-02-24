using A4.DAL.Entites.Maintenance;
using DAL;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;


namespace A4.DAL.Repositories
{
    public class SalaryComponentRepository : Repository<SalaryComponent>, ISalaryComponentRepository
    {
        public SalaryComponentRepository(DbContext context):base(context)
        {

        }

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        //public override void Add(SalaryComponent entity)
        //{
        //    base.Add(entity);
        //    _context.SaveChanges();
        //}

        //public override void Update(SalaryComponent entity)
        //{
        //    base.Add(entity);
        //    _context.SaveChanges();
        //}

        //public void AddRange(IEnumerable<EmployeeCtc> entities)
        //{
        //    throw new NotImplementedException();
        //}

        //public IEnumerable<EmployeeCtc> Find(Expression<Func<EmployeeCtc, bool>> predicate)
        //{
        //    throw new NotImplementedException();
        //}

        //public List<EmployeeCtc> Get(Expression<Func<EmployeeCtc, bool>> filter = null, Func<IQueryable<EmployeeCtc>, IOrderedQueryable<EmployeeCtc>> orderBy = null, string includeProperties = "")
        //{
        //    throw new NotImplementedException();
        //}

        //public List<EmployeeCtc> GetIncludePath(Expression<Func<EmployeeCtc, bool>> filter = null, Func<IQueryable<EmployeeCtc>, IOrderedQueryable<EmployeeCtc>> orderBy = null, Expression<Func<EmployeeCtc, object>> includeFilter = null)
        //{
        //    throw new NotImplementedException();
        //}

        //public EmployeeCtc GetSingleOrDefault(Expression<Func<EmployeeCtc, bool>> predicate)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Remove(EmployeeCtc entity)
        //{
        //    throw new NotImplementedException();
        //}

        //public void RemoveRange(IEnumerable<EmployeeCtc> entities)
        //{
        //    throw new NotImplementedException();
        //}

        

        //public void UpdateRange(IEnumerable<EmployeeCtc> entities)
        //{
        //    throw new NotImplementedException();
        //}

        //EmployeeCtc IRepository<EmployeeCtc>.Get(Guid id)
        //{
        //    throw new NotImplementedException();
        //}

        //IEnumerable<EmployeeCtc> IRepository<EmployeeCtc>.GetAll()
        //{
        //    throw new NotImplementedException();
        //}

        //EmployeeCtc IRepository<EmployeeCtc>.GetById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public override EmployeeCtc Get(Guid id)
        //{
        //    var employeeCTC = _appContext.EmployeeCtc.Where(e => e.Id == id && e.IsActive == true).FirstOrDefault();
        //    return band;
        //}





        //public override void Remove(Band entity)
        //{
        //    base.Remove(entity);
        //    _context.SaveChanges();
        //}




    }
}
