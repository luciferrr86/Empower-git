using A4.DAL.Core;
using DAL.Core;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.Empower.Authorization
{
    public class PerformanceAuthorizationRequirement : IAuthorizationRequirement
    {
    }

    public class ManageSetGoalAuthorizationHandler : AuthorizationHandler<PerformanceAuthorizationRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PerformanceAuthorizationRequirement requirement, string resource)
        {
            if (context.User == null)
                return Task.CompletedTask;
            if (context.User.HasClaim(CustomClaimTypes.Permission, ApplicationPermissions.ManageSetGoal))
                context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }

    public class ManageHrViewAuthorizationHandler : AuthorizationHandler<PerformanceAuthorizationRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PerformanceAuthorizationRequirement requirement, string resource)
        {
            if (context.User == null)
                return Task.CompletedTask;
            if (context.User.HasClaim(CustomClaimTypes.Permission, ApplicationPermissions.ManageHrView))
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }

    public class ManageMyGoalAuthorizationHandler : AuthorizationHandler<PerformanceAuthorizationRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PerformanceAuthorizationRequirement requirement, string resource)
        {
            if (context.User == null)
                return Task.CompletedTask;
            if (context.User.HasClaim(CustomClaimTypes.Permission, ApplicationPermissions.ManageMyGoal))
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }

    public class ManageReviewGoalAuthorizationHandler : AuthorizationHandler<PerformanceAuthorizationRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PerformanceAuthorizationRequirement requirement, string resource)
        {
            if (context.User == null)
                return Task.CompletedTask;
            if (context.User.HasClaim(CustomClaimTypes.Permission, ApplicationPermissions.ManageReviewGoal))
                context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
