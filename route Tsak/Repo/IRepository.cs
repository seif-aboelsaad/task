namespace route_Tsak;
public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity> GetById(int id);
    List<TEntity> GetAll();
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
}
