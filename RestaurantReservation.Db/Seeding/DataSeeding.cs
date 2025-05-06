using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Seeding;

public class DataSeeding
{
    public static Customer[] GetSeedCustomers()
    {
        return new Customer[]
        {
            new Customer { CustomerId = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", PhoneNumber = "123456789" },
            new Customer { CustomerId = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", PhoneNumber = "987654321" },
            new Customer { CustomerId = 3, FirstName = "Bob", LastName = "Brown", Email = "bob.brown@example.com", PhoneNumber = "555666777" },
            new Customer { CustomerId = 4, FirstName = "Alice", LastName = "Green", Email = "alice.green@example.com", PhoneNumber = "999888777" },
            new Customer { CustomerId = 5, FirstName = "Charlie", LastName = "White", Email = "charlie.white@example.com", PhoneNumber = "111222333" },
        };
    }

    public static Employee[] GetSeedEmployees()
    {
        return new Employee[]
        {
                new Employee { EmployeeId = 1, RestaurantId = 1, FirstName = "Mark", LastName = "Johnson", Position = "Manager" },
                new Employee { EmployeeId = 2, RestaurantId = 2, FirstName = "Sara", LastName = "Williams", Position = "Waiter" },
                new Employee { EmployeeId = 3, RestaurantId = 1, FirstName = "Tom", LastName = "Lee", Position = "Chef" },
                new Employee { EmployeeId = 4, RestaurantId = 3, FirstName = "Nancy", LastName = "Davis", Position = "Waiter" },
                new Employee { EmployeeId = 5, RestaurantId = 2, FirstName = "Jake", LastName = "Wilson", Position = "Manager" },
        };
    }

    public static MenuItem[] GetSeedMenuItems()
    {
        return new MenuItem[]
        {
                new MenuItem { MenuItemId = 1, RestaurantId = 1, Name = "Pizza", Description = "Cheese Pizza", Price = 10.0m },
                new MenuItem { MenuItemId = 2, RestaurantId = 1, Name = "Burger", Description = "Beef Burger", Price = 8.0m },
                new MenuItem { MenuItemId = 3, RestaurantId = 2, Name = "Pasta", Description = "Spaghetti Bolognese", Price = 12.0m },
                new MenuItem { MenuItemId = 4, RestaurantId = 3, Name = "Salad", Description = "Caesar Salad", Price = 7.0m },
                new MenuItem { MenuItemId = 5, RestaurantId = 2, Name = "Soup", Description = "Tomato Soup", Price = 6.0m },
        };
    }

    public static Restaurant[] GetSeedRestaurants()
    {
        return new Restaurant[]
        {
                new Restaurant { RestaurantId = 1, Name = "Italian Bistro", Address = "123 Main St", PhoneNumber = "555-1234", OpeningHours = "9 AM - 9 PM" },
                new Restaurant { RestaurantId = 2, Name = "American Grill", Address = "456 Oak St", PhoneNumber = "555-5678", OpeningHours = "11 AM - 10 PM" },
                new Restaurant { RestaurantId = 3, Name = "French Cafe", Address = "789 Pine St", PhoneNumber = "555-9876", OpeningHours = "8 AM - 8 PM" },
        };
    }

    public static Table[] GetSeedTables()
    {
        return new Table[]
        {
                new Table { TableId = 1, RestaurantId = 1, Capacity = 4 },
                new Table { TableId = 2, RestaurantId = 1, Capacity = 2 },
                new Table { TableId = 3, RestaurantId = 2, Capacity = 6 },
                new Table { TableId = 4, RestaurantId = 2, Capacity = 4 },
                new Table { TableId = 5, RestaurantId = 3, Capacity = 4 },
        };
    }

    public static Reservation[] GetSeedReservations()
    {
        return new Reservation[]
        {
                new Reservation { ReservationId = 1, CustomerId = 1, RestaurantId = 1, TableId = 1, ReservationDate = new DateTime(2025, 1, 1), PartySize = 4 },
                new Reservation { ReservationId = 2, CustomerId = 2, RestaurantId = 2, TableId = 3, ReservationDate = new DateTime(2025, 8, 10), PartySize = 6 },
                new Reservation { ReservationId = 3, CustomerId = 3, RestaurantId = 3, TableId = 5, ReservationDate = new DateTime(2025, 11, 17), PartySize = 4 },
                new Reservation { ReservationId = 4, CustomerId = 4, RestaurantId = 1, TableId = 2, ReservationDate = new DateTime(2025, 10, 2), PartySize = 2 },
                new Reservation { ReservationId = 5, CustomerId = 5, RestaurantId = 2, TableId = 4, ReservationDate = new DateTime(2025, 5, 21), PartySize = 4 },
        };
    }

    public static Order[] GetSeedOrders()
    {
        return new Order[]
        {
                new Order { OrderId = 1, ReservationId = 1, EmployeeId = 1, OrderDate = new DateTime(2025, 5, 21), TotalAmount = 50 },
                new Order { OrderId = 2, ReservationId = 2, EmployeeId = 2, OrderDate = new DateTime(2025, 5, 21), TotalAmount = 60 },
                new Order { OrderId = 3, ReservationId = 3, EmployeeId = 3, OrderDate = new DateTime(2025, 5, 21), TotalAmount = 70 },
                new Order { OrderId = 4, ReservationId = 4, EmployeeId = 4, OrderDate = new DateTime(2025, 5, 21), TotalAmount = 40 },
                new Order { OrderId = 5, ReservationId = 5, EmployeeId = 5, OrderDate = new DateTime(2025, 5, 21), TotalAmount = 30 },
        };
    }

    public static OrderItem[] GetSeedOrderItems()
    {
        return new OrderItem[]
        {
                new OrderItem { OrderItemId = 1, OrderId = 1, MenuItemId = 1, Quantity = 2 },
                new OrderItem { OrderItemId = 2, OrderId = 1, MenuItemId = 2, Quantity = 1 },
                new OrderItem { OrderItemId = 3, OrderId = 2, MenuItemId = 3, Quantity = 3 },
                new OrderItem { OrderItemId = 4, OrderId = 3, MenuItemId = 4, Quantity = 2 },
                new OrderItem { OrderItemId = 5, OrderId = 4, MenuItemId = 5, Quantity = 2 },
        };
    }
}
