using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantReservation.Db.Migrations
{
    /// <inheritdoc />
    public partial class CreateCalculateRestaurantRevenueFunction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                CREATE FUNCTION fn_CalculateRestaurantRevenue (
                	@RestaurantId INT
                )
                returns DECIMAL(10, 2) 
                AS
                BEGIN
                	DECLARE @TotalRevenue INT;

                	SET @TotalRevenue = (
                	select SUM(oi.Quantity * mi.Price)
                    FROM OrderItems oi
                    INNER JOIN Orders o ON o.OrderId = oi.OrderId
                    INNER JOIN MenuItems mi ON oi.MenuItemId = mi.MenuItemId
                    WHERE mi.RestaurantId = @RestaurantId
                	);
                    RETURN ISNULL(@TotalRevenue, 0) 

                END;
              
                """
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION fn_CalculateRestaurantRevenue");
        }
    }
}
