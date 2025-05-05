
namespace RestaurantReservation.Db.Models;

public class Customer
{
    public int CustomerId { set; get; }
    public string FirstName { set; get; }
    public string LastName { set; get; }
    public string Email { set; get; }
    public string PhoneNumber { set; get; }
    public List<Reservation> Reservations { set; get; } = new List<Reservation>();
}
