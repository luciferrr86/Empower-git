using A4.DAL.Core;
using DAL.Core;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.Empower.Authorization
{
    public class RecruitmentAuthorizationRequirement : IAuthorizationRequirement
    {

    }
    public class ManageRecruitmentDashboardAuthorizationHandler : AuthorizationHandler<RecruitmentAuthorizationRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RecruitmentAuthorizationRequirement requirement, string targetUserId)
        {
            if (context.User == null)
                return Task.CompletedTask;

            if (context.User.HasClaim(CustomClaimTypes.Permission, ApplicationPermissions.ManageRecruitmentDasboard))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
    public class ManageJobVaccancyAuthorizationHandler : AuthorizationHandler<RecruitmentAuthorizationRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RecruitmentAuthorizationRequirement requirement, string targetUserId)
        {
            if (context.User == null)
                return Task.CompletedTask;

            if (context.User.HasClaim(CustomClaimTypes.Permission, ApplicationPermissions.ManageJobVaccancy))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }


    public class ManageInterviewAuthorizationHandler : AuthorizationHandler<RecruitmentAuthorizationRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RecruitmentAuthorizationRequirement requirement, string targetUserId)
        {
            if (context.User == null)
                return Task.CompletedTask;

            if (context.User.HasClaim(CustomClaimTypes.Permission, ApplicationPermissions.ManageInterview))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }

    public class ManageBulkSchedulingAuthorizationHandler : AuthorizationHandler<RecruitmentAuthorizationRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RecruitmentAuthorizationRequirement requirement, string targetUserId)
        {
            if (context.User == null)
                return Task.CompletedTask;

            if (context.User.HasClaim(CustomClaimTypes.Permission, ApplicationPermissions.ManageBulkScheduling))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
    public class ManageJobVaccancyListAuthorizationHandler : AuthorizationHandler<RecruitmentAuthorizationRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RecruitmentAuthorizationRequirement requirement, string targetUserId)
        {
            if (context.User == null)
                return Task.CompletedTask;

            if (context.User.HasClaim(CustomClaimTypes.Permission, ApplicationPermissions.ManageJobVaccancyList))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
