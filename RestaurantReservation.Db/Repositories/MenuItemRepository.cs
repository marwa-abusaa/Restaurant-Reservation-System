using RestaurantReservation.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace RestaurantReservation.Db.Repositories;

public class MenuItemRepository : Repository<MenuItem>
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

    public async Task<bool> IsMenuItemExists(int id)
    {
        return await _context.MenuItems.AnyAsync(m => m.MenuItemId == id);
    }
}
