using A4.DAL.Entites.Maintenance;
using DAL;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;


namespace A4.DAL.Repositories
{
    public class CtcOtherComponentRepository : Repository<CtcOtherComponent>, ICtcOtherComponentRepository
    {
        public CtcOtherComponentRepository(DbContext context):base(context)
        {

        }

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        

        //public void AddRange(IEnumerable<CtcOtherComponent> entities)
        //{
        //    throw new NotImplementedException();
        //}

        //public IEnumerable<CtcOtherComponent> Find(Expression<Func<CtcOtherComponent, bool>> predicate)
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
