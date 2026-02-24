using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


namespace A4.DAL.Entites
{
    public class ApplicationUser : IdentityUser, IAuditableEntity
    {
        public string FullName { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsLockedOut => this.LockoutEnabled && this.LockoutEnd >= DateTimeOffset.UtcNow;
        public bool IsActive { get; set; }
        public bool IsSuperAdmin { get; set; }
        public bool IsMailSent { get; set; }
        public int? PictureId { get; set; }
        public Picture Picture { get; set; }

        #region Children

        //public Employee ? Employee { get; set; }

        //public CandidateProfile? CandidateProfile { get; set; }
        #endregion

        /// <summary>
        /// Navigation property for the roles this user belongs to.
        /// </summary>
        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        /// <summary>
        /// Navigation property for the claims this user possesses.
        /// </summary>
        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }


    }
}
