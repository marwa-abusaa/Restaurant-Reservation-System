using RestaurantReservation.Db.Models.Enum;

namespace RestaurantReservation.API.Models.Employees;

public class EmployeeDto
{
    public int EmployeeId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public EmployeePosition Position { get; set; }
    public int RestaurantId { get; set; }
}
