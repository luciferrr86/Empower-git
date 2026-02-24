using A4.DAL.Core;
using DAL.Core;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.Empower.Authorization
{
    public class TimesheetAuthorizationRequirement : IAuthorizationRequirement
    {
    }

    public class ManageMyTimesheetAuthorizationHandler : AuthorizationHandler<TimesheetAuthorizationRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TimesheetAuthorizationRequirement requirement, string resource)
        {
            if (context.User == null)
                return Task.CompletedTask;
            if (context.User.HasClaim(CustomClaimTypes.Permission, ApplicationPermissions.ManageMyTimesheet))
                context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }

    public class ManageTimesheetAuthorizationHandler : AuthorizationHandler<TimesheetAuthorizationRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TimesheetAuthorizationRequirement requirement, string resource)
        {
            if (context.User == null)
                return Task.CompletedTask;
            if (context.User.HasClaim(CustomClaimTypes.Permission, ApplicationPermissions.ManageTimesheet))
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
