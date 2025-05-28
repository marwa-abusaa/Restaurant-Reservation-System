namespace RestaurantReservation.API.Models.MenuItems;

public class MenuItemDto
{
    public int MenuItemId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int RestaurantId { get; set; }
}
