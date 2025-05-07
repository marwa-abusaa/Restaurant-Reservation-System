
namespace RestaurantReservation.Db.Models;

public class Table
{
    public int TableId { set; get; }
    public int Capacity { set; get; }
    public int RestaurantId { set; get; }
    public Restaurant Restaurant { get; set; }
    public List<Reservation> Reservations { get; set; } = new List<Reservation>();
}
