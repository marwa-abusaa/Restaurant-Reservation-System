using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Models.Enum;

namespace RestaurantReservation.Db.Repositories;

public class EmployeeRepository : Repository<Employee>
{
    private RestaurantReservationDbContext _context;

    public EmployeeRepository (RestaurantReservationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Employee>> ListManagers()
    {
        return await _context.Employees
                    .Where(e => e.Position == EmployeePosition.Manager)
                    .ToListAsync();
    }
}
