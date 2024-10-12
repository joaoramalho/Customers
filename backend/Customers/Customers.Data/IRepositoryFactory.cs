using Customers.Data.Models;

namespace Customers.Data;

public interface IRepositoryFactory
{
    IRepository<Customer> CreateCustomersRepository();
}