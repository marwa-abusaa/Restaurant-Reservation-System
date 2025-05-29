using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories;

public class RestaurantRepository : Repository<Restaurant>
{
    private RestaurantReservationDbContext _context;

    public RestaurantRepository(RestaurantReservationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<decimal> CalculateRestaurantRevenue(int restaurantId)
    {

        var result = await _context.Database
        .SqlQuery<RevenueResult>($"SELECT dbo.fn_CalculateRestaurantRevenue({restaurantId}) AS TotalRevenue")
        .AsNoTracking()
        .FirstOrDefaultAsync();

        return result?.TotalRevenue ?? 0;
    }

    public async Task<bool> IsRestaurantExists(int id)
    {
        return await _context.Restaurants.AnyAsync(r => r.RestaurantId == id);
    }
}
