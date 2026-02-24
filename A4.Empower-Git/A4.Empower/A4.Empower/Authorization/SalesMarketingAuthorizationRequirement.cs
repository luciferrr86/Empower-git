using A4.DAL.Core;
using DAL.Core;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.Empower.Authorization
{
    public class SalesMarketingAuthorizationRequirement: IAuthorizationRequirement
    {
    }
    public class ManageSalesmarketingAuthorizationHandler : AuthorizationHandler<SalesMarketingAuthorizationRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SalesMarketingAuthorizationRequirement requirement, string resource)
        {
            if (context.User == null)
                return Task.CompletedTask;
            if (context.User.HasClaim(CustomClaimTypes.Permission, ApplicationPermissions.ManageSalesMarketing))
                context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
