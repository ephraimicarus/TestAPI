using BaseApi.Interfaces;
using Microsoft.EntityFrameworkCore;
using TestAPI.Data;
using TestAPI.Models;

namespace TestAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationContext _context;
        private readonly IInventoryService _inventoryService;
        public CustomerService(ApplicationContext context, IInventoryService inventoryService)
        {
            _context = context;
            _inventoryService = inventoryService;
        }
        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            try
            {
                Customer customerToCreate = new()
                {
                    Name = customer.Name,
                    Oib = customer.Oib,
                    Address = customer.Address,
                    Email = customer.Email,
                    Phone = customer.Phone,
                    Overdue = customer.Overdue,
                };

                _context.Add(customerToCreate);
                await _context.SaveChangesAsync();
                await _inventoryService.CreateInventory(customerToCreate.CustomerId);
                return customerToCreate;
            }
            catch
            {
                throw;
            }
        }
        public async Task<Customer> DeleteCustomerAsync(Customer customer)
        {
            var customerToDelete = _context.Customers
            .Single(c => c.CustomerId == customer.CustomerId);
            try
            {
                _context.Remove(customerToDelete);
                await _context.SaveChangesAsync();
                return customerToDelete;
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<Customer>> GetAllCustomersAsync() => await _context.Customers.ToListAsync();

        public async Task<Customer> GetCustomerByOibAsync(string oib) => await _context.Customers.SingleAsync(c => c.Oib == oib);

        public async Task<Customer> UpdateCustomerAsync(Customer customer)
        {
            var customerToUpdate = await _context.Customers.SingleOrDefaultAsync(c => c.CustomerId == customer.CustomerId);
            if (customerToUpdate == null)
            {
                throw new KeyNotFoundException($"Customer with ID {customer.CustomerId} not found.");
            }
            try
            {
                customerToUpdate!.Oib = customer.Oib;
                customerToUpdate.Name = customer.Name;
                customerToUpdate.Address = customer.Address;
                customerToUpdate.Email = customer.Email;
                customerToUpdate.Phone = customer.Phone;

                _context.Customers.Update(customerToUpdate);
                await _context.SaveChangesAsync();

                return customerToUpdate;
            }
            catch
            {
                throw;
            }
        }
    }
}
