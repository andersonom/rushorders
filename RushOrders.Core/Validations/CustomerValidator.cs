using FluentValidation;
using RushOrders.Core.Models;

namespace RushOrders.Core.Validations
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(1, 50);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }
}
