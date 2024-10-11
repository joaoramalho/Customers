namespace Customers.Data;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}