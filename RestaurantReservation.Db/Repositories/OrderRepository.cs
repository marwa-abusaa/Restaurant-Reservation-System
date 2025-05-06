using RestaurantReservation.Db.Models;
using Microsoft.EntityFrameworkCore;


namespace RestaurantReservation.Db.Repositories;

public class OrderRepository : Repository<Employee>
{
    private RestaurantReservationDbContext _context;

    public OrderRepository(RestaurantReservationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Order>> ListOrdersAndMenuItems(int reservationId)
    {
        return await _context.Orders
                    .Where(o => o.ReservationId == reservationId)
                    .Include(o => o.OrderItems)
                        .ThenInclude(mi => mi.MenuItem)
                    .ToListAsync();
    }
}
