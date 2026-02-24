using A4.BAL.ExpenseBooking;
using A4.DAL.Entites;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Repositories
{
   public interface IExpenseBookingTitleAmountRepository : IRepository<ExpenseBookingTitleAmount>
    {
        PagedList<ExpenseBookingTitleModel> GetAllTitle(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
