using FluentValidation;
using System.Linq;

namespace RestaurantAPI.Models.Validators
{
    public class RestaurantQueryValidator:AbstractValidator<RestaurantQuery>
    {
        private int[] allowedPageSizes = { 5, 10, 30 };
        public RestaurantQueryValidator()
        {
            
            RuleFor(_ => _.PageNumber).GreaterThan(0);
            RuleFor(_ => _.PageSize).Custom((value, context) =>
            {
                if (!allowedPageSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"Acceptable page sizes : [{string.Join(",", allowedPageSizes)}]");
                }
            });

            
        }
    }
}
