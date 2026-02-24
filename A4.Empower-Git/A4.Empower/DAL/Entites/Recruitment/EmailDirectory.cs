using A4.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Entites
{
    public class EmailDirectory: AuditableEntity
    {
        #region Constructor

        public EmailDirectory()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        
        #region Properties
        
        public Guid Id { get; set; }

        
        public string Name { get; set; }

        
        public string Designation{ get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        

        #endregion
    }
}
