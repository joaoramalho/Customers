namespace Customers.Data;

public class Repository<T>(ApplicationDbContext context) : IRepository<T>
    where T : class
{
    public IEnumerable<T> GetAll()
    {
        return context.Set<T>().ToList();
    }

    public void Add(T entity)
    {
        context.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        context.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        context.Set<T>().Remove(entity);
    }
}