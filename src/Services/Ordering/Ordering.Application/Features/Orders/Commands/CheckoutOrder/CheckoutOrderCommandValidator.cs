using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandValidator: AbstractValidator<CheckoutOrderCommand>
    {
        public CheckoutOrderCommandValidator()
        {
            RuleFor(p => p.UserName)
                .NotEmpty().WithMessage("UserName is requred")
                .NotNull()
                .MaximumLength(50).WithMessage("Username Must not be less than 50 characters");

            RuleFor(o => o.EmailAddress)
                .NotEmpty().WithMessage("Email address is required");

            RuleFor(o => o.TotalPrice)
                .NotEmpty().WithMessage("Total Price is required")
                .GreaterThan(0).WithMessage("Total Price should be greater than zero");
        }
    }
}
