using A4.DAL.Core;
using DAL.Core;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.Empower.Authorization
{
    public class LeaveAuthorizationRequirement : IAuthorizationRequirement
    {
    }

    public class ManageMyLeaveAuthorizationHandler: AuthorizationHandler<LeaveAuthorizationRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, LeaveAuthorizationRequirement requirement, string resource)
        {
            if (context.User == null)
                return Task.CompletedTask;
            if (context.User.HasClaim(CustomClaimTypes.Permission, ApplicationPermissions.ManageMyLeave))
                context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }

    public class ManageLeaveAuthorizationHandler : AuthorizationHandler<LeaveAuthorizationRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, LeaveAuthorizationRequirement requirement, string resource)
        {
            if (context.User == null)
                return Task.CompletedTask;
            if (context.User.HasClaim(CustomClaimTypes.Permission, ApplicationPermissions.ManageLeave))
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }

    public class ManageHrLeaveAuthorizationHandler : AuthorizationHandler<LeaveAuthorizationRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, LeaveAuthorizationRequirement requirement, string resource)
        {
            if (context.User == null)
                return Task.CompletedTask;
            if (context.User.HasClaim(CustomClaimTypes.Permission, ApplicationPermissions.ManageHrLeave))
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }

    public class ManageMyAttendanceAuthorizationHandler : AuthorizationHandler<LeaveAuthorizationRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, LeaveAuthorizationRequirement requirement, string resource)
        {
            if (context.User == null)
                return Task.CompletedTask;
            if (context.User.HasClaim(CustomClaimTypes.Permission, ApplicationPermissions.ManageMyAttendance))
                context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }

    public class ManageAttendanceAuthorizationHandler : AuthorizationHandler<LeaveAuthorizationRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, LeaveAuthorizationRequirement requirement, string resource)
        {
            if (context.User == null)
                return Task.CompletedTask;
            if (context.User.HasClaim(CustomClaimTypes.Permission, ApplicationPermissions.ManageAttendance))
                context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
    public class ManageAttendanceDetailAuthorizationHandler : AuthorizationHandler<LeaveAuthorizationRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, LeaveAuthorizationRequirement requirement, string resource)
        {
            if (context.User == null)
                return Task.CompletedTask;
            if (context.User.HasClaim(CustomClaimTypes.Permission, ApplicationPermissions.ManageAttendanceDetail))
                context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
    public class ManageUploadAttendanceDetailAuthorizationHandler : AuthorizationHandler<LeaveAuthorizationRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, LeaveAuthorizationRequirement requirement, string resource)
        {
            if (context.User == null)
                return Task.CompletedTask;
            if (context.User.HasClaim(CustomClaimTypes.Permission, ApplicationPermissions.ManageUploadAttendanceDetail))
                context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
    public class ManageAttendanceSummaryAuthorizationHandler : AuthorizationHandler<LeaveAuthorizationRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, LeaveAuthorizationRequirement requirement, string resource)
        {
            if (context.User == null)
                return Task.CompletedTask;
            if (context.User.HasClaim(CustomClaimTypes.Permission, ApplicationPermissions.ManageAttendanceSummary))
                context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
    public class ManageUploadAttendanceSummaryAuthorizationHandler : AuthorizationHandler<LeaveAuthorizationRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, LeaveAuthorizationRequirement requirement, string resource)
        {
            if (context.User == null)
                return Task.CompletedTask;
            if (context.User.HasClaim(CustomClaimTypes.Permission, ApplicationPermissions.ManageUploadAttendanceSummary))
                context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
