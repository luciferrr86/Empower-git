using A4.DAL.Core;
using A4.Empower.Helpers;
using DAL.Core;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace A4.Empower.Authorization
{
    public class MaintenanceAuthorizationRequirement: IAuthorizationRequirement
    {
    }

    public class ManageEmployeeAuthorizationHandler : AuthorizationHandler<MaintenanceAuthorizationRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MaintenanceAuthorizationRequirement requirement, string targetUserId)
        {
            if (context.User == null)
                return Task.CompletedTask;

            if (context.User.HasClaim(CustomClaimTypes.Permission, ApplicationPermissions.ManageEmployee) || GetIsSameUser(context.User, targetUserId))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }


        private bool GetIsSameUser(ClaimsPrincipal user, string targetUserId)
        {
            if (string.IsNullOrWhiteSpace(targetUserId))
                return false;

            return Utilities.GetUserId(user) == targetUserId;
        }
    }

    public class ManageDepartmentAuthorizationHandler : AuthorizationHandler<MaintenanceAuthorizationRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MaintenanceAuthorizationRequirement requirement, string targetUserId)
        {
            if (context.User == null)
                return Task.CompletedTask;

            if (context.User.HasClaim(CustomClaimTypes.Permission, ApplicationPermissions.ManageDepartment))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }

    public class ManageDesignationAuthorizationHandler : AuthorizationHandler<MaintenanceAuthorizationRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MaintenanceAuthorizationRequirement requirement, string targetUserId)
        {
            if (context.User == null)
                return Task.CompletedTask;

            if (context.User.HasClaim(CustomClaimTypes.Permission, ApplicationPermissions.ManageDesignation))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }

    public class ManageGroupAuthorizationHandler : AuthorizationHandler<MaintenanceAuthorizationRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MaintenanceAuthorizationRequirement requirement, string targetUserId)
        {
            if (context.User == null)
                return Task.CompletedTask;

            if (context.User.HasClaim(CustomClaimTypes.Permission, ApplicationPermissions.ManageGroup))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }

    public class ManageTitleAuthorizationHandler : AuthorizationHandler<MaintenanceAuthorizationRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MaintenanceAuthorizationRequirement requirement, string targetUserId)
        {
            if (context.User == null)
                return Task.CompletedTask;

            if (context.User.HasClaim(CustomClaimTypes.Permission, ApplicationPermissions.ManageTitle))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }

    public class ManageRoleAuthorizationHandler : AuthorizationHandler<MaintenanceAuthorizationRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MaintenanceAuthorizationRequirement requirement, string targetUserId)
        {
            if (context.User == null)
                return Task.CompletedTask;

            if (context.User.HasClaim(CustomClaimTypes.Permission, ApplicationPermissions.ManageRole))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }

    public class ManageBandAuthorizationHandler : AuthorizationHandler<MaintenanceAuthorizationRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MaintenanceAuthorizationRequirement requirement, string targetUserId)
        {
            if (context.User == null)
                return Task.CompletedTask;

            if (context.User.HasClaim(CustomClaimTypes.Permission, ApplicationPermissions.ManageBand))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
    public class ManageProcessSalaryAuthorizationHandler : AuthorizationHandler<MaintenanceAuthorizationRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MaintenanceAuthorizationRequirement requirement, string targetUserId)
        {
            if (context.User == null)
                return Task.CompletedTask;

            if (context.User.HasClaim(CustomClaimTypes.Permission, ApplicationPermissions.ManageProcessSalary))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
    public class ManageCheckSalaryAuthorizationHandler : AuthorizationHandler<MaintenanceAuthorizationRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MaintenanceAuthorizationRequirement requirement, string targetUserId)
        {
            if (context.User == null)
                return Task.CompletedTask;

            if (context.User.HasClaim(CustomClaimTypes.Permission, ApplicationPermissions.ManageCheckSalary))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }

    public class ManageSalaryComponentAuthorizationHandler : AuthorizationHandler<MaintenanceAuthorizationRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MaintenanceAuthorizationRequirement requirement, string targetUserId)
        {
            if (context.User == null)
                return Task.CompletedTask;

            if (context.User.HasClaim(CustomClaimTypes.Permission, ApplicationPermissions.ManageSalaryComponent))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }

    public class ManageAllEmployeeSalaryComponentAuthorizationHandler : AuthorizationHandler<MaintenanceAuthorizationRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MaintenanceAuthorizationRequirement requirement, string targetUserId)
        {
            if (context.User == null)
                return Task.CompletedTask;

            if (context.User.HasClaim(CustomClaimTypes.Permission, ApplicationPermissions.ManageAllEmployeeSalary))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
    public class ManageSalaryComponentListAuthorizationHandler : AuthorizationHandler<MaintenanceAuthorizationRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MaintenanceAuthorizationRequirement requirement, string targetUserId)
        {
            if (context.User == null)
                return Task.CompletedTask;

            if (context.User.HasClaim(CustomClaimTypes.Permission, ApplicationPermissions.ManageSalaryComponentList))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
