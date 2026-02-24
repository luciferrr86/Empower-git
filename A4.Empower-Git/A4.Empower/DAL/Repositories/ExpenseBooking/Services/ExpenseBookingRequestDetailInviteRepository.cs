using A4.DAL.Entites;
using DAL;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;


namespace A4.DAL.Repositories
{
    public class ExpenseBookingRequestDetailInviteRepository : Repository<ExpenseBookingRequestDetailInvite>, IExpenseBookingRequestDetailInviteRepository
    {
        public ExpenseBookingRequestDetailInviteRepository(DbContext context) : base(context)
        {

        }

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

    }
}
