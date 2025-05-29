using FluentValidation;
using RestaurantReservation.API.Models.Reservations;

namespace RestaurantReservation.API.Validators.Reservations;

public class ReservationCreationValidator : AbstractValidator<ReservationCreateDto>
{
    public ReservationCreationValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("Customer ID is required and must be greater than zero.");

        RuleFor(x => x.RestaurantId)
            .NotEmpty().WithMessage("Restaurant ID is required and must be greater than zero.");

        RuleFor(x => x.TableId)
            .NotEmpty().WithMessage("Table ID is required and must be greater than zero.");

        RuleFor(x => x.ReservationDate)
            .GreaterThan(DateTime.Now).WithMessage("Reservation date must be in the future.");

        RuleFor(x => x.PartySize)
            .GreaterThan(0).WithMessage("Party size must be greater than zero.");
    }
}
