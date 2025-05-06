using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Models.Enum;
using RestaurantReservation.Db.Models.Views;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories;

public class CustomerRepository : Repository<Employee>
{
    private RestaurantReservationDbContext _context;

    public CustomerRepository(RestaurantReservationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Customer>> GetCustomersWithReservationsAbovePartySize(int partySize)
    {
        return await _context.Customers.FromSql($"EXEC sp_FindCustomersWithPartySizeLargerThan {partySize}").ToListAsync();
    }
}
