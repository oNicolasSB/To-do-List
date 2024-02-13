namespace todolist.Domain;

public interface IBaseRepository<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllAsync(params string[] includes);
    Task<IEnumerable<TEntity>> GetAllReadOnlyAsync(params string[] includes);
    TEntity? GetById(int id);
    TEntity Add(TEntity entity);
    TEntity Update(TEntity entity);
    void Delete(TEntity entity);
}