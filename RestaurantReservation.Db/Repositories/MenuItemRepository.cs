using RestaurantReservation.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace RestaurantReservation.Db.Repositories;

public class MenuItemRepository : Repository<Employee>
{
    private RestaurantReservationDbContext _context;

    public MenuItemRepository(RestaurantReservationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<MenuItem>> ListOrderedMenuItems (int reservationId)
    {
        return await _context.OrderItems
                    .Where(oi => oi.Order.ReservationId == reservationId)
                    .Include(oi => oi.MenuItem)
                    .Select(oi => oi.MenuItem)
                    .Distinct()
                    .ToListAsync();
    }
}
