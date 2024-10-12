using Customers.Data.Models;

namespace Customers.Data;

public class RepositoryFactory(ApplicationDbContext dbContext) : IRepositoryFactory
{
    public IRepository<Customer> CreateCustomersRepository()
    {
        return new Repository<Customer>(dbContext);
    }
}