using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Core
{
    public static class CustomClaimTypes
    {
        ///<summary>A claim that specifies the permission of an entity</summary>
        public const string Permission = "permission";

        ///<summary>A claim that specifies the full name of an entity</summary>
        public const string FullName = "fullname";

        ///<summary>A claim that specifies the job title of an entity</summary>
        //public const string JobTitle = "jobtitle";

        ///<summary>A claim that specifies the email of an entity</summary>
        public const string Email = "email";

        ///<summary>A claim that specifies the phone number of an entity</summary>
        public const string Phone = "phone";
        public const string Type = "type";
        public const string Leave = "leave";
        public const string Recruitment = "recruitment";
        public const string Timesheet = "timesheet";
        public const string Performance = "performance";
        public const string SalesMarketing = "salesMarketing";
        public const string ExpanseManagement ="expanseManagement";

        ///<summary>A claim that specifies the configuration/customizations of an entity</summary>
        //public const string Configuration = "configuration";
    }
}
