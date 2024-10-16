using Customers.Data.Models;

namespace Customers.Web.Customers;

public class CustomerMapper : ICustomerMapper
{
    public CustomersDto Map(Customer customer)
    {
        return new CustomersDto(
            customer.Id,
            customer.FirstName,
            customer.LastName,
            customer.Email,
            customer.PhoneNumber,
            customer.Address
        );
    }

    public Customer Map(CustomersDto customerDto)
    {
        return new Customer
        {
            FirstName = customerDto.FirstName,
            LastName = customerDto.LastName,
            Email = customerDto.Email,
            Address = customerDto.Address,
            PhoneNumber = customerDto.PhoneNumber
        };
    }
}