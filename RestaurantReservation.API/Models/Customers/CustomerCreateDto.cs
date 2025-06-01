namespace RestaurantReservation.API.Models.Customers;

public class CustomerCreateDto
{
    public string FirstName { set; get; } = String.Empty;
    public string LastName { set; get; } = String.Empty;
    public string Email { set; get; } = String.Empty;
    public string? PhoneNumber { set; get; }
}