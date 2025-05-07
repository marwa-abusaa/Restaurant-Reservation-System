using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories
{
    class TableRepository : Repository<Employee>
    {
        private RestaurantReservationDbContext _context;

        public TableRepository(RestaurantReservationDbContext context) : base(context)
        {
            _context = context;
        }    
    }
}