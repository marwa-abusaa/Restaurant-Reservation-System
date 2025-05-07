using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories;

class OrderItemRepository : Repository<OrderItem>
{
    private RestaurantReservationDbContext _context;

    public OrderItemRepository(RestaurantReservationDbContext context) : base(context)
    {
        _context = context;
    }

}