using AutoMapper.Configuration.Annotations;
using FluentValidation;
using RestaurantAPI.Entities;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantAPI.Models.Validators
{
    public class RestaurantQueryValidator:AbstractValidator<RestaurantQuery>
    {
        private int[] allowedPageSizes = { 5, 10, 30 };

        private string[] sortableProperties = 
        {
            nameof(Restaurant.Name),
            nameof(Restaurant.Description),
            nameof(Restaurant.Category),
            "City"                                      //Address.City for dynamic linq
        };

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

            RuleFor(_ => _.SortBy)
                .Must(value => string.IsNullOrEmpty(value) || sortableProperties.Contains(value))
                .WithMessage($"SortBy is optional or acceptable sorting by : [{string.Join(",", sortableProperties)}]");
        }
    }
}
