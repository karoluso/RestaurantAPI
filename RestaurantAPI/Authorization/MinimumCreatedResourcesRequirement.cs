using Microsoft.AspNetCore.Authorization;

namespace RestaurantAPI.Authorization
{
    public class MinimumCreatedResourcesRequirement : IAuthorizationRequirement
    {

        public int MinimumRequired { get; }


        public MinimumCreatedResourcesRequirement(int requiredNumber)
        {
            MinimumRequired = requiredNumber;

        }
    }
}
