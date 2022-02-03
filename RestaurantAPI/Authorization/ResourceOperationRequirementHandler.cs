using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using RestaurantAPI.Entities;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestaurantAPI.Authorization
{
    public class ResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, Restaurant>
    {
       

      
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, Restaurant resource)
        {
            if(requirement.RequestedOperation== ResourceOperationEnum.Read ||
                requirement.RequestedOperation==ResourceOperationEnum.Create)
            {
                context.Succeed(requirement);
            }

            var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);  //check later with possible null


            if (userId== resource.CreatedByUserId)
            {
                context.Succeed(requirement);
            }

            //throw new NotAuthorizedException();
            return Task.CompletedTask;
        }
    }
}
