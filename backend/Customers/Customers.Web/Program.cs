using Customers.Data;
using Customers.Web.Customers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IRepositoryFactory, RepositoryFactory>();
builder.Services.AddScoped<ICustomerMapper, CustomerMapper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//For demonstration purposes only. Not safe in a production App
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseHttpsRedirection();
app.UseRouting();

app.MapGet("/customers", (IRepositoryFactory repositoryFactory) =>
{
    var repository = repositoryFactory.CreateCustomersRepository();
    return repository.GetAll();
});

app.MapPost("/customers", (IRepositoryFactory repositoryFactory, ICustomerMapper mapper, CustomersDto customer) =>
{
    var repository = repositoryFactory.CreateCustomersRepository();
    var mappedCustomer = mapper.Map(customer);
    
    repository.Add(mappedCustomer);
    repository.Save();
    
    return Results.Created($"/customers/{mappedCustomer.Id}", mappedCustomer);
});

app.MapPut("customers/{id}", (IRepositoryFactory repositoryFactory, ICustomerMapper mapper, long id, CustomersDto dto) =>
{
    var repository = repositoryFactory.CreateCustomersRepository();
    var customer = repository.GetById(id);
    if (customer is null)
    {
        return Results.BadRequest("Customer not found");
    }
    
    var mappedCustomer = mapper.Map(dto);
    
    customer.FirstName = mappedCustomer.FirstName;
    customer.LastName = mappedCustomer.LastName;
    customer.Email = mappedCustomer.Email;
    customer.PhoneNumber = mappedCustomer.PhoneNumber;
    customer.Address = mappedCustomer.Address;
    
    repository.Update(customer);
    repository.Save();
    
    return Results.Ok(customer);
});

app.MapDelete("/customers/{id}", (IRepositoryFactory repositoryFactory, long id) =>
{
    var repository = repositoryFactory.CreateCustomersRepository();
    var customer = repository.GetById(id);
    if (customer is null)
    {
        return Results.BadRequest("Customer not found");
    }
    
    repository.Delete(customer);
    repository.Save();
    
    return Results.Ok();
});

app.Run();