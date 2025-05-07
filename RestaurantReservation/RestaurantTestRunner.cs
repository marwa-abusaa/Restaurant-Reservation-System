using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Models.Enum;
using RestaurantReservation.Db.Repositories;

namespace RestaurantReservation;

public class RestaurantTestRunner
{
    private readonly CustomerRepository _customerRepo;
    private readonly EmployeeRepository _employeeRepo;
    private readonly ReservationRepository _reservationRepo;
    private readonly OrderRepository _orderRepo;
    private readonly OrderItemRepository _orderItemRepo;
    private readonly MenuItemRepository _menuItemRepo;
    private readonly RestaurantRepository _restaurantRepo;
    private readonly TableRepository _tableRepo;

    public RestaurantTestRunner(
        CustomerRepository customerRepo,
        EmployeeRepository employeeRepo,
        ReservationRepository reservationRepo,
        OrderRepository orderRepo,
        OrderItemRepository orderItemRepo,
        MenuItemRepository menuItemRepo,
        RestaurantRepository restaurantRepo,
        TableRepository tableRepo)
        
    {
        _customerRepo = customerRepo;
        _employeeRepo = employeeRepo;
        _reservationRepo = reservationRepo;
        _orderRepo = orderRepo;
        _orderItemRepo = orderItemRepo;
        _menuItemRepo = menuItemRepo;
        _restaurantRepo = restaurantRepo;
        _tableRepo = tableRepo;
    }

    public async Task RunAllTests()
    {
        await TestCustomerCRUD();
        await TestEmployeeCRUD();
        await TestMenuItemCRUD();
        await TestOrderCRUD();
        await TestOrderItemCRUD();
        await TestReservationCRUD();
        await TestRestaurantCRUD();
        await TestTableCRUD();

        await TestGetCustomersWithReservationsAbovePartySize();

        await TestListManagers();
        await TestGetEmployeesWithRestaurantDetails();

        await TestListOrderedMenuItems();

        await TestCalculateAverageOrderAmount();
        await TestListOrdersAndMenuItems();

        await TestGetReservationsByCustomer();
        await TestGetReservationDetails();

        await TestCalculateRestaurantRevenue();
    }

    private async Task TestCustomerCRUD()
    {
        var newCustomer = new Customer { FirstName = "Marwa", LastName = "AbuSaa", Email = "marwa@gmail.com", PhoneNumber = "1234567890" };
        await _customerRepo.Create(newCustomer);
        Console.WriteLine("Customer created!");

        newCustomer.LastName = "Nasir";
        await _customerRepo.Update(newCustomer);
        Console.WriteLine("Customer updated!");

        var retrievedCustomer = await _customerRepo.GetById(newCustomer.CustomerId);
        Console.WriteLine($"Retrieved Customer: {retrievedCustomer.FirstName} {retrievedCustomer.LastName}");

        await _customerRepo.DeleteById(newCustomer.CustomerId);
        Console.WriteLine("Customer deleted! \n");
    }

    private async Task TestEmployeeCRUD()
    {
        var newEmployee = new Employee { RestaurantId = 1, FirstName = "Aya", LastName = "Baara", Position = EmployeePosition.Manager };
        await _employeeRepo.Create(newEmployee);
        Console.WriteLine("Employee created!");

        newEmployee.LastName = "Jamal";
        await _employeeRepo.Update(newEmployee);
        Console.WriteLine("Employee updated!");

        var retrievedEmployee = await _employeeRepo.GetById(newEmployee.EmployeeId);
        Console.WriteLine($"Retrieved Employee: {retrievedEmployee.FirstName} {retrievedEmployee.LastName}");

        await _employeeRepo.DeleteById(newEmployee.EmployeeId);
        Console.WriteLine("Employee deleted! \n");
    }

    private async Task TestMenuItemCRUD()
    {
        var newMenuItem = new MenuItem { RestaurantId = 1, Name = "Pasta", Description = "Delicious spaghetti", Price = 12.99M };
        await _menuItemRepo.Create(newMenuItem);
        Console.WriteLine("MenuItem created!");

        newMenuItem.Price = 14.99M;
        await _menuItemRepo.Update(newMenuItem);
        Console.WriteLine("MenuItem updated!");

        var retrievedMenuItem = await _menuItemRepo.GetById(newMenuItem.MenuItemId);
        Console.WriteLine($"Retrieved MenuItem: {retrievedMenuItem.Name} - {retrievedMenuItem.Description} - ${retrievedMenuItem.Price}");

        await _menuItemRepo.DeleteById(newMenuItem.MenuItemId);
        Console.WriteLine("MenuItem deleted! \n");
    }

    private async Task TestOrderCRUD()
    {
        var newOrder = new Order { EmployeeId = 2, ReservationId = 2, OrderDate = new DateTime(2025, 5, 30), TotalAmount = 50 };
        await _orderRepo.Create(newOrder);
        Console.WriteLine("Order created!");

        newOrder.TotalAmount = 75;
        await _orderRepo.Update(newOrder);
        Console.WriteLine("Order updated!");

        var retrievedOrder = await _orderRepo.GetById(newOrder.OrderId);
        Console.WriteLine($"Retrieved Order ID: {retrievedOrder.OrderId} - Total: ${retrievedOrder.TotalAmount}");

        await _orderRepo.DeleteById(newOrder.OrderId);
        Console.WriteLine("Order deleted! \n");
    }

    private async Task TestOrderItemCRUD()
    {
        var newOrderItem = new OrderItem { MenuItemId = 1, OrderId = 1, Quantity = 2 };
        await _orderItemRepo.Create(newOrderItem);
        Console.WriteLine("OrderItem created!");

        newOrderItem.Quantity = 3;
        await _orderItemRepo.Update(newOrderItem);
        Console.WriteLine("OrderItem updated!");

        var retrievedOrderItem = await _orderItemRepo.GetById(newOrderItem.OrderItemId);
        Console.WriteLine($"Retrieved OrderItem: Item ID: {retrievedOrderItem.MenuItemId}, Quantity: {retrievedOrderItem.Quantity}");

        await _orderItemRepo.DeleteById(newOrderItem.OrderItemId);
        Console.WriteLine("OrderItem deleted! \n");
    }

    private async Task TestReservationCRUD()
    {
        var newReservation = new Reservation { CustomerId = 1, RestaurantId = 2, TableId = 1, PartySize = 4, ReservationDate = new DateTime(2025, 5, 31) };
        await _reservationRepo.Create(newReservation);
        Console.WriteLine("Reservation created!");

        newReservation.PartySize = 5;
        await _reservationRepo.Update(newReservation);
        Console.WriteLine("Reservation updated!");

        var retrievedReservation = await _reservationRepo.GetById(newReservation.ReservationId);
        Console.WriteLine($"Retrieved Reservation: Party Size: {retrievedReservation.PartySize}");

        await _reservationRepo.DeleteById(newReservation.ReservationId);
        Console.WriteLine("Reservation deleted! \n");
    }

    private async Task TestRestaurantCRUD()
    {
        var newRestaurant = new Restaurant { Name = "The Great Restaurant", Address = "123 Food St.", PhoneNumber = "123-456-7890", OpeningHours = "9 AM - 10 PM" };
        await _restaurantRepo.Create(newRestaurant);
        Console.WriteLine("Restaurant created!");

        newRestaurant.Name = "The Amazing Restaurant";
        await _restaurantRepo.Update(newRestaurant);
        Console.WriteLine("Restaurant updated!");

        var retrievedRestaurant = await _restaurantRepo.GetById(newRestaurant.RestaurantId);
        Console.WriteLine($"Retrieved Restaurant: {retrievedRestaurant.Name}, Address: {retrievedRestaurant.Address}");

        await _restaurantRepo.DeleteById(newRestaurant.RestaurantId);
        Console.WriteLine("Restaurant deleted! \n");
    }

    private async Task TestTableCRUD()
    {
        var newTable = new Table { RestaurantId = 2, Capacity = 10 };
        await _tableRepo.Create(newTable);
        Console.WriteLine("Table created!");

        newTable.Capacity = 8;
        await _tableRepo.Update(newTable);
        Console.WriteLine("Table updated!");

        var retrievedTable = await _tableRepo.GetById(newTable.TableId);
        Console.WriteLine($"Retrieved Table: Capacity: {retrievedTable.Capacity}");

        await _tableRepo.DeleteById(newTable.TableId);
        Console.WriteLine("Table deleted!\n");
    }

    private async Task TestGetCustomersWithReservationsAbovePartySize()
    {
        int minSize = 2;
        var customers = await _customerRepo.GetCustomersWithReservationsAbovePartySize(minSize);
        if(customers.Any())
        {
            foreach (var c in customers)
                Console.WriteLine($"Customer: {c.FirstName} {c.LastName} - Email: {c.Email}");
        }
        else
            Console.WriteLine($"No customers found with reservations larger than {minSize}.");

    }

    private async Task TestListManagers()
    {
        var managers = await _employeeRepo.ListManagers();
        foreach (var m in managers)
            Console.WriteLine($"Manager: {m.FirstName} {m.LastName}");
    }

    private async Task TestGetEmployeesWithRestaurantDetails()
    {
        var result = await _employeeRepo.GetEmployeesWithRestaurantDetails();
        foreach (var e in result)
            Console.WriteLine($"Employee: {e.EmployeeFirstName}, Restaurant: {e.RestaurantName}, Address: {e.RestaurantAddress}");
    }

    private async Task TestListOrderedMenuItems()
    {
        int reservationId = 1;
        var items = await _menuItemRepo.ListOrderedMenuItems(reservationId);
        if (items != null && items.Any())
        {
            foreach (var item in items)
                Console.WriteLine($"Menu Items: {item.Name} - {item.Price}$"); 
        }
        else
        {
            Console.WriteLine($"\nNo menu items found for Reservation ID {reservationId}");
        }
    }

    private async Task TestListOrdersAndMenuItems()
    {
        int reservationId = 1;
        var result = await _orderRepo.ListOrdersAndMenuItems(reservationId);
        foreach (var item in result)
        {
            Console.WriteLine($"Order ID: {item.OrderId}");

            foreach(var orderItem in item.OrderItems)
                Console.WriteLine($"Menu Item: {orderItem.MenuItem.Name} - {orderItem.MenuItem.Price}$");
        }            
    }

    private async Task TestCalculateAverageOrderAmount()
    {
        int employeeId = 1;
        var avg = await _orderRepo.CalculateAverageOrderAmount(employeeId);
        Console.WriteLine($"Average Order Amount for employee {employeeId} : {avg}");
    }

    private async Task TestGetReservationsByCustomer()
    {
        int customerId = 1;
        var reservations = await _reservationRepo.GetReservationsByCustomer(customerId);
        if (reservations != null && reservations.Any())
        {
            foreach (var r in reservations)
                Console.WriteLine($"Reservation: {r.ReservationId}, ReservationDate: {r.ReservationDate}, PartySize: {r.PartySize} for Customer {r.CustomerId}");

        } 
        else
        {
            Console.WriteLine($"No reservations found for Customer ID {customerId}");
        }
    }

    private async Task TestGetReservationDetails()
    {
        var data = await _reservationRepo.GetReservationDetails();
        foreach (var r in data)
            Console.WriteLine($"Reservation: {r.ReservationId}, Customer: {r.CustomerFirstName} {r.CustomerLastName}, Restaurant: {r.RestaurantName}, ReservationDate: {r.ReservationDate}");
    }

    private async Task TestCalculateRestaurantRevenue()
    {
        int restaurantId = 1;
        var total = await _restaurantRepo.CalculateRestaurantRevenue(restaurantId);
        Console.WriteLine($"Total Revenue for Restaurant {restaurantId}: {total}");
    }
}
