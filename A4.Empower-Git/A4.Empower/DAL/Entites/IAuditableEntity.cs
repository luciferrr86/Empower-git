
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{
    public interface IAuditableEntity
    {
        string CreatedBy { get; set; }
        string UpdatedBy { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime UpdatedDate { get; set; }
        bool IsActive { get; set; }
    }
}
