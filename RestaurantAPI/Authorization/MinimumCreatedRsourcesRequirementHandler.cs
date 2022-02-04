using Microsoft.AspNetCore.Authorization;
using NLog.LayoutRenderers;
using RestaurantAPI.Entities;
using RestaurantAPI.Services;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestaurantAPI.Authorization
{
    public class MinimumCreatedRsourcesRequirementHandler : AuthorizationHandler<MinimumCreatedResourcesRequirement>
    {
        private readonly RestaurantDbContext _dbContext;

        public MinimumCreatedRsourcesRequirementHandler(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumCreatedResourcesRequirement requirement)
        {
            var userId = int.Parse(context.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var createdResourceCount=_dbContext.Restaurants.Count(r => r.CreatedByUserId == userId);

            if (createdResourceCount >= requirement.MinimumRequired)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
