using Customers.Data.Models;

namespace Customers.Web.Customers;

public interface ICustomerMapper
{
    CustomersDto Map(Customer customer);
    Customer Map(CustomersDto customerDto);
}