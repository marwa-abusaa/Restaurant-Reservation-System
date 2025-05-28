namespace RestaurantReservation.API.Models.Orders;

public class OrderUpdateDto
{
    public decimal TotalAmount { get; set; }
    public int ReservationId { get; set; }
    public int EmployeeId { get; set; }
}
