namespace RestaurantReservation.API.Models.OrderItems;

public class OrderItemCreateDto
{
    public int Quantity { get; set; }
    public int OrderId { get; set; }
    public int MenuItemId { get; set; }
}
