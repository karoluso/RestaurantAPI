using FluentValidation;
using RestaurantAPI.Entities;
using System.Linq;

namespace RestaurantAPI.Models.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {


        public RegisterUserDtoValidator(RestaurantDbContext dBcontext)
        {
            RuleFor(_ => _.Email).NotEmpty().EmailAddress();

            RuleFor(_ => _.Password).MinimumLength(6);

            RuleFor(_ => _.Password).Equal(_ => _.ConfirmPassword);

            RuleFor(_ => _.Email).Custom((value, context) =>
            {
                var emailInUse = dBcontext.Users.Any(_ => _.Email == value);
                if (emailInUse)
                {
                    context.AddFailure("Email", "Email already in use");
                }
            });
        }
    }
}
