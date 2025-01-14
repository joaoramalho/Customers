namespace Customers.Data;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    T? GetById(long id);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    void Save();
}