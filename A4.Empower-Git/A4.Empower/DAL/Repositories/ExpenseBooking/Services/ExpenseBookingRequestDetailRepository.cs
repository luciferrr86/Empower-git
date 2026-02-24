using A4.DAL.Entites;
using DAL;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;


namespace A4.DAL.Repositories
{
    public class ExpenseBookingRequestDetailRepository : Repository<ExpenseBookingRequestDetail>, IExpenseBookingRequestDetailRepository
    {
        public ExpenseBookingRequestDetailRepository(DbContext context) : base(context)
        {

        }

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

    }
}
