using RestaurantReservation.Db.Models;
using Microsoft.EntityFrameworkCore;


namespace RestaurantReservation.Db.Repositories;

public class OrderRepository : Repository<Order>
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

    public async Task<decimal> CalculateAverageOrderAmount(int EmployeeId)
    {
        var orders = await _context.Orders
                    .Where(o => o.EmployeeId == EmployeeId)
                    .ToListAsync();

        if (orders.Count == 0) 
            return 0;
        else 
            return orders.Average(o => o.TotalAmount);
    }
}
