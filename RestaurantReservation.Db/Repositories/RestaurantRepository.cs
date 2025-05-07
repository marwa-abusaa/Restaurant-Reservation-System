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

        var revenue = await _context
        .Set<RevenueResult>()
        .FromSqlRaw($"SELECT dbo.GetTotalRevenueByRestaurant({0}) AS TotalRevenue", restaurantId)
        .FirstOrDefaultAsync();

        return revenue.TotalRevenue;
    }
}
