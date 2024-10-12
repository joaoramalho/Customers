using Customers.Data;
using Customers.Data.Models;
using Microsoft.AspNetCore.Http.HttpResults;
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.MapGet("/customers", async (IRepositoryFactory repositoryFactory) =>
{
    var repository = repositoryFactory.CreateCustomersRepository();
    return repository.GetAll();
});

app.MapPost("/customers", async (IRepositoryFactory repositoryFactory, Customer customer) =>
{
    var repository = repositoryFactory.CreateCustomersRepository();
    repository.Add(customer);
    repository.Save();
    
    return Results.Created($"/customers/{customer.Id}", customer);
});

app.MapPut("customers/{id}", async (IRepositoryFactory repositoryFactory, long id) =>
{
    var repository = repositoryFactory.CreateCustomersRepository();
    var customer = repository.GetById(id);
    if (customer is null)
    {
        return Results.BadRequest("Customer not found");
    }
    
    repository.Update(customer);
    repository.Save();
    
    return Results.Ok(customer);
});

app.MapDelete("/customers/{id}", async (IRepositoryFactory repositoryFactory, long id) =>
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