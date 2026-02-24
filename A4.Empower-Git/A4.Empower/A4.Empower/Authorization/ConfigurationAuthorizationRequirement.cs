using A4.DAL.Core;
using DAL.Core;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.Empower.Authorization
{
    public class ConfigurationAuthorizationRequirement: IAuthorizationRequirement
    {
    }
    public class ManageConfigurationAuthorizationHandler : AuthorizationHandler<ConfigurationAuthorizationRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ConfigurationAuthorizationRequirement requirement, string targetUserId)
        {
            if (context.User == null)
                return Task.CompletedTask;

            if (context.User.HasClaim(CustomClaimTypes.Permission, ApplicationPermissions.ManageConfiguration))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
