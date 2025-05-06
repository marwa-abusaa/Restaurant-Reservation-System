using RestaurantReservation.Db.Models;
using Microsoft.EntityFrameworkCore;


namespace RestaurantReservation.Db.Repositories;

public class ReservationRepository : Repository<Employee>
{
    private RestaurantReservationDbContext _context;

    public ReservationRepository(RestaurantReservationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Reservation>> GetReservationsByCustomer(int customerId)
    {
        return await _context.Reservations
                    .Where(r => r.CustomerId == customerId)
                    .ToListAsync();
    }
}
