using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestaurantAPI.Authorization
{
    public class MinimumAgeRequirementHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        private readonly ILogger<MinimumAgeRequirementHandler> _logger;

        public MinimumAgeRequirementHandler( ILogger<MinimumAgeRequirementHandler> logger)
        {
            _logger = logger;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            var dateOfBirth = DateTime.Parse(context.User.FindFirst(c => c.Type == "DateOfBirth").Value.ToString());

            var name = context.User.FindFirst(c=>c.Type==ClaimTypes.Name).Value.ToString();

            _logger.LogInformation($"User: {name} with date of birth: [{dateOfBirth.ToShortDateString()}]");

            if (dateOfBirth.AddYears(requirement.MinimumAge) <= DateTime.Now)
            {
                context.Succeed(requirement);
                _logger.LogInformation($"Authorization succeeded");
            }
            else
            {
                context.Fail();
                _logger.LogInformation($"Authorization  failed");
            }
            return Task.CompletedTask;
        }
    }
}


