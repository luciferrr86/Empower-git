using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class ClientViewModel
    {
        public ClientViewModel()
        {
            ClientList = new List<ClientModel>();
        }
        public List<ClientModel> ClientList { get; set; }
        public int TotalCount { get; set; }
    }
    public class ClientModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string EmailId { get; set; }

        public string Address { get; set; }

        public string Contact { get; set; }
    }
}
