using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories;

public class CustomerRepository : Repository<Customer>
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

    public async Task<bool> IsCustomerExists(int id)
    {
        return await _context.Customers.AnyAsync(c => c.CustomerId == id);
    }
}
