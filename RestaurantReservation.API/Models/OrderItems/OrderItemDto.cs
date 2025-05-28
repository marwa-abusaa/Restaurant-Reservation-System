namespace RestaurantReservation.API.Models.OrderItems;

public class OrderItemDto
{
    public int OrderItemId { get; set; }
    public int Quantity { get; set; }
    public int OrderId { get; set; }
    public int MenuItemId { get; set; }
}
