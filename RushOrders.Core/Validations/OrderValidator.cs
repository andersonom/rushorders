using System;
using FluentValidation;
using RushOrders.Core.Models;

namespace RushOrders.Core.Validations
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.CreationDate).Must(BeAValidDate).WithMessage("CreationDate is required");
        }
        private static bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}
