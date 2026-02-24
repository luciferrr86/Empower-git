using A4.DAL.Core;
using DAL.Core;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.Empower.Authorization
{
    public class ExpenseBookingAuthorizationRequirement: IAuthorizationRequirement
    {
    }
    public class ManageExpenseBookingAuthorizationHandler : AuthorizationHandler<ExpenseBookingAuthorizationRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ExpenseBookingAuthorizationRequirement requirement, string resource)
        {
            if (context.User == null)
                return Task.CompletedTask;
            if (context.User.HasClaim(CustomClaimTypes.Permission, ApplicationPermissions.ManageExpenseBooking))
                context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
    public class ManageExpenseApprovedAuthorizationHandler : AuthorizationHandler<ExpenseBookingAuthorizationRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ExpenseBookingAuthorizationRequirement requirement, string resource)
        {
            if (context.User == null)
                return Task.CompletedTask;

            if (context.User.HasClaim(CustomClaimTypes.Permission, ApplicationPermissions.ManageApprovedBooking))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
