using Microsoft.EntityFrameworkCore;

namespace RestaurantReservation.Db;

public class RestaurantReservationDbContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
}
