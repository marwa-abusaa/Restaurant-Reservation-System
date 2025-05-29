using FluentValidation;
using RestaurantReservation.API.Models.OrderItems;

namespace RestaurantReservation.API.Validators.OrderItems;

public class OrderItemCreationValidator : AbstractValidator<OrderItemCreateDto>
{
    public OrderItemCreationValidator()
    {
        RuleFor(x => x.MenuItemId)
            .NotEmpty().WithMessage("Item ID is required and must be greater than zero.");

        RuleFor(x => x.OrderId)
            .NotEmpty().WithMessage("Order ID is required must be greater than zero.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero.");
    }
}
