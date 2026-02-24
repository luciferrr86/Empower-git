using A4.DAL.Core;
using DAL.Core;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace A4.Empower.Authorization
{
    public class BlogAuthorizationRequirement: IAuthorizationRequirement
    {
    }

    public class ManageBlogAuthorizationHandler :
        AuthorizationHandler<BlogAuthorizationRequirement,string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, BlogAuthorizationRequirement requirement, string targetUserId)
        {
            if (context.User == null)
                return Task.CompletedTask;

            if (context.User.HasClaim(CustomClaimTypes.Permission, ApplicationPermissions.ManageBlog))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
