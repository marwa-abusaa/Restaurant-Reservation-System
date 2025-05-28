using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Interfaces;

namespace RestaurantReservation.Db.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private RestaurantReservationDbContext _context;
    private DbSet<TEntity> _dbSet;

    public Repository(RestaurantReservationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }
    public async Task<TEntity> Create(TEntity model)
    {
        _dbSet.Add(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task DeleteById(int id)
    {
        var model = await _dbSet.FindAsync(id);
        if (model != null)
        {
            _dbSet.Remove(model);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new InvalidOperationException($"No record found with ID {id}");
        }
    }

    public async Task<TEntity> GetById(int id)
    {
        var model = await _dbSet.FindAsync(id);
        
        return model;       
    }

    public async Task Update(TEntity model)
    {
        _dbSet.Update(model);
        await _context.SaveChangesAsync();
    }
}
