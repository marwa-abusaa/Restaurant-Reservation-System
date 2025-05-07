
namespace RestaurantReservation.Db.Models.Views;

public class ReservationDetails
{
    public int ReservationId { get; set; }
    public DateTime ReservationDate { get; set; }
    public int TableId { get; set; }
    public int CustomerId { get; set; }
    public string CustomerFirstName { get; set; }
    public string CustomerLastName { get; set; }
    public int RestaurantId { get; set; }
    public string RestaurantName { get; set; }
}
