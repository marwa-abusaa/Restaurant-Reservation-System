using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db;
using RestaurantReservation.Db.Repositories;
using Microsoft.Extensions.Configuration;
using RestaurantReservation;

var services = new ServiceCollection();


var config = new ConfigurationBuilder()
    .AddJsonFile("C:\\Users\\hp\\source\\repos\\RestaurantReservation\\RestaurantReservation\\AppSettings.json")
    .Build();

services.AddDbContext<RestaurantReservationDbContext>(options =>
    options.UseSqlServer(config.GetConnectionString("RestaurantReservationCore")));


services.AddScoped<CustomerRepository>();
services.AddScoped<EmployeeRepository>();
services.AddScoped<ReservationRepository>();
services.AddScoped<OrderRepository>();
services.AddScoped<OrderItemRepository>();
services.AddScoped<MenuItemRepository>();
services.AddScoped<RestaurantRepository>();
services.AddScoped<TableRepository>();
services.AddScoped<RestaurantTestRunner>();

var provider = services.BuildServiceProvider();

var runner = provider.GetRequiredService<RestaurantTestRunner>();
await runner.RunAllTests();
