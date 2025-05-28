namespace RestaurantReservation.API.Models.Reservations;

public class ReservationCreateDto
{
    public DateTime ReservationDate { set; get; }
    public int PartySize { set; get; }
    public int CustomerId { set; get; }
    public int RestaurantId { set; get; }
    public int TableId { set; get; }
}
