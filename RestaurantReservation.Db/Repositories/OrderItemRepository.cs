using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories;

class OrderItemRepository : Repository<Employee>
{
    private RestaurantReservationDbContext _context;

    public OrderItemRepository(RestaurantReservationDbContext context) : base(context)
    {
        _context = context;
    }

}