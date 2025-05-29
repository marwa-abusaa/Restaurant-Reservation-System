using FluentValidation;
using RestaurantReservation.API.Models.Orders;

namespace RestaurantReservation.API.Validators.Orders;

public class OrderCreationValidator : AbstractValidator<OrderCreateDto>
{
    public OrderCreationValidator()
    {
        RuleFor(x => x.ReservationId)
            .NotEmpty().WithMessage("Reservation ID is required and must be greater than zero.");

        RuleFor(x => x.EmployeeId)
            .NotEmpty().WithMessage("Employee ID is required and must be greater than zero.");

        RuleFor(x => x.OrderDate)
            .GreaterThan(DateTime.Now).WithMessage("Order date must be in the future.");

        RuleFor(x => x.TotalAmount)
            .GreaterThan(0).WithMessage("Total amount must be greater than zero.");
    }
}
