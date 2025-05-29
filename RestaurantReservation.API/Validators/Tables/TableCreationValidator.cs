using FluentValidation;
using RestaurantReservation.API.Models.Tables;

namespace RestaurantReservation.API.Validators.Tables;

public class TableCreationValidator : AbstractValidator<TableCreateDto>
{
    public TableCreationValidator()
    {
        RuleFor(x => x.RestaurantId)
            .NotEmpty().WithMessage("Restaurant ID is required and must be greater than zero.");

        RuleFor(x => x.Capacity)
            .NotEmpty().WithMessage("Capacity is required.")
            .GreaterThan(0).WithMessage("Capacity must be greater than zero.");
    }
}
