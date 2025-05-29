namespace RestaurantReservation.API.Models.Restaurants;

public class RestaurantUpdateDto
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string OpeningHours { get; set; } = string.Empty;
}
