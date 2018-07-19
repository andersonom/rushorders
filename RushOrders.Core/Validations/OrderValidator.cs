using FluentValidation;
using RushOrders.Core.Models;

namespace RushOrders.Core.Validations
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(x => x.Price).GreaterThan(0);
        }    
    }
}
