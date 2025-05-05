
namespace RestaurantReservation.Db.Models;

public class Reservation
{
    public int ReservationId { set; get; }  
    public DateTime ReservationDate { set; get; }
    public int PartySize { set; get; }
    public int CustomerId { set; get; }
    public Customer Customer { get; set; }
    public int RestaurantId { set; get; }
    public Restaurant Restaurant { get; set; }
    public int TableId { set; get; }    
    public Table Table { get; set; }
    public List<Order> Orders { get; set; } = new List<Order>();
}
