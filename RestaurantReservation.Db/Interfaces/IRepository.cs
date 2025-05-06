
namespace RestaurantReservation.Db.Interfaces;

public interface IRepository<TEntity> where TEntity: class
{
    public Task Create(TEntity model);
    public Task Update(TEntity model);
    public Task DeleteById(int id);
    public Task<TEntity> GetById(int id);
}
