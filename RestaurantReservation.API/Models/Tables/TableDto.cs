using RestaurantReservation.Db.Models;

namespace RestaurantReservation.API.Models.Tables;

public class TableDto
{
    public int TableId { set; get; }
    public int Capacity { set; get; }
    public int RestaurantId { set; get; }
}
