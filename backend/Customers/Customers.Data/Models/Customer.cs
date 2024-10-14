using System.ComponentModel.DataAnnotations;

namespace Customers.Data.Models;

public class Customer
{
    public long Id { get; init; }
    [StringLength(30)]
    [Required]
    public required string FirstName { get; set; }
    [StringLength(30)]
    [Required]
    public required string LastName { get; set; }
    [StringLength(50)]
    [EmailAddress]
    [Required]
    public required string Email { get; set; }
    [StringLength(50)]
    [Required]
    [Phone]
    public required string PhoneNumber { get; set; }
    [StringLength(100)]
    [Required]
    public required string Address { get; set; }
}