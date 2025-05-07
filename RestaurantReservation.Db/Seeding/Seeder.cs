using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Seeding;

public static class Seeder
{
    public static void SeedDatabase(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>().HasData(DataSeeding.GetSeedCustomers());
        modelBuilder.Entity<Employee>().HasData(DataSeeding.GetSeedEmployees());
        modelBuilder.Entity<MenuItem>().HasData(DataSeeding.GetSeedMenuItems());
        modelBuilder.Entity<Order>().HasData(DataSeeding.GetSeedOrders());
        modelBuilder.Entity<OrderItem>().HasData(DataSeeding.GetSeedOrderItems());
        modelBuilder.Entity<Reservation>().HasData(DataSeeding.GetSeedReservations());
        modelBuilder.Entity<Restaurant>().HasData(DataSeeding.GetSeedRestaurants());
        modelBuilder.Entity<Table>().HasData(DataSeeding.GetSeedTables());
    }
}
