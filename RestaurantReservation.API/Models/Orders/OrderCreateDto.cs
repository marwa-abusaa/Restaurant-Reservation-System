namespace RestaurantReservation.API.Models.Orders;

public class OrderCreateDto
{
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public int ReservationId { get; set; }
    public int EmployeeId { get; set; }
}
