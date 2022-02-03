using Microsoft.AspNetCore.Authorization;

namespace RestaurantAPI.Authorization
{
    public enum ResourceOperationEnum
    {
        Create,
        Read,
        Update,
        Delete
    }

    public class ResourceOperationRequirement :IAuthorizationRequirement
    {
        public ResourceOperationEnum RequestedOperation { get; }
       

        public ResourceOperationRequirement(ResourceOperationEnum requestedOperation)
        {
            RequestedOperation = requestedOperation;
        }
    }
}
