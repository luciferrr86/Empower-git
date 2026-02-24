using A4.BAL.ExpenseBooking;
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
    public class ExpenseBookingTitleAmountRepository : Repository<ExpenseBookingTitleAmount>, IExpenseBookingTitleAmountRepository
    {
        public ExpenseBookingTitleAmountRepository(DbContext context) : base(context)
        {

        }

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public PagedList<ExpenseBookingTitleModel> GetAllTitle(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = from data in _appContext.ExpenseBookingTitleAmount
                        from title in _appContext.FunctionalTitle where data.TitleID==title.Id
                        select new ExpenseBookingTitleModel
                        {
                            Id = data.Id.ToString(),
                            Amount = data.Amount,
                            TitleId = data.TitleID.ToString(),
                            TitleName=title.Name
                        };
            return new PagedList<ExpenseBookingTitleModel>(query, pageIndex, pageSize);

        }
    }
}
