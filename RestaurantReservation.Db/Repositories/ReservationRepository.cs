using RestaurantReservation.Db.Models;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Models.Views;


namespace RestaurantReservation.Db.Repositories;

public class ReservationRepository : Repository<Reservation>
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

    public async Task<List<ReservationDetails>> GetReservationDetails()
    {
        return await _context.ReservationDetails.ToListAsync();
    }
}
