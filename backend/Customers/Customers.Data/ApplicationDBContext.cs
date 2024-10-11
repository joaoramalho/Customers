using Customers.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Customers.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Customer> Customers { get; init; }
}