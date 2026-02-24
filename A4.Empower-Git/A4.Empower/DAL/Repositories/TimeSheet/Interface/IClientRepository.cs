using A4.DAL.Entites;
using DAL.Repositories.Interfaces;


namespace A4.DAL.Repositories
{
   public interface IClientRepository : IRepository<TimesheetClient>
    {
        PagedList<TimesheetClient> GetAllClient(string name = "", int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
