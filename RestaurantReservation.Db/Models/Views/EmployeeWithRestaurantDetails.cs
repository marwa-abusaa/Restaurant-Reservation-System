﻿
namespace RestaurantReservation.Db.Models.Views;

public class EmployeeWithRestaurantDetails
{
    public int EmployeeId { get; set; }
    public string EmployeeFirstName { get; set; }
    public string EmployeeLastName { get; set; }
    public int RestaurantId { get; set; }
    public string RestaurantName { get; set; }
    public string RestaurantAddress { get; set; }
}
