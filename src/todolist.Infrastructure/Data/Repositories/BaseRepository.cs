using System.Data.Entity;
using todolist.Domain;

namespace todolist.Infrastructure.Data.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    private readonly DataContext _context;

    public BaseRepository(DataContext context)
    {
        _context = context;
    }
    public TEntity Add(TEntity entity)
    {
        return _context.Set<TEntity>().Add(entity).Entity;
    }

    public void Delete(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(params string[] includes)
    {
        var query = _context.Set<TEntity>().AsQueryable();

        foreach (var include in includes)
            query = query.Include(include);

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllReadOnlyAsync(params string[] includes)
    {
        var query = _context.Set<TEntity>().AsNoTracking();

        foreach (var include in includes)
            query = query.Include(include);

        return await query.ToListAsync();
    }

    public TEntity? GetById(int id)
    {
        return _context.Set<TEntity>().Find(id);
    }

    public TEntity Update(TEntity entity)
    {
        return _context.Set<TEntity>().Update(entity).Entity;
    }
}