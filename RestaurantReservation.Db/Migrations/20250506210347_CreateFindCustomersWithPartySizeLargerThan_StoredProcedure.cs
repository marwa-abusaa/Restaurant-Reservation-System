using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantReservation.Db.Migrations
{
    /// <inheritdoc />
    public partial class CreateFindCustomersWithPartySizeLargerThan_StoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                CREATE PROCEDURE sp_FindCustomersWithPartySizeLargerThan (
                	@MinPartySize INT
                )
                AS BEGIN 
                	SELECT DISTINCT c.*
                    FROM Customers c
                    JOIN Reservations r ON c.CustomerId = r.CustomerId
                    WHERE r.PartySize > @MinPartySize;
                END;                  
                """
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE sp_FindCustomersWithPartySizeLargerThan");
        }
    }
}
