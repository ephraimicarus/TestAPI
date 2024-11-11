using BaseApi.Interfaces;
using Microsoft.EntityFrameworkCore;
using TestAPI.Data;
using TestAPI.Models;
using TestAPI.Services;

namespace BaseApi.Services
{
    public class CustomerDueService : ICustomerDueService
    {
        private readonly ApplicationContext _context;
        private readonly ICustomerService _customerService;

        public CustomerDueService(ApplicationContext context, ICustomerService customerService)
        {
            _context = context;
            _customerService = customerService;
        }
        public async Task<List<Customer>> GetOverdueCustomers() {
            List<Customer> customerList = new();
            var customers=  await _context.Customers.ToListAsync();
            foreach(var c in customers)
            {
                if(await IsCustomerDue(c))
                {
                    c.Overdue = true;
                    await _customerService.UpdateCustomerAsync(c);
                    var days = await GetCustomerDaysDue(c.CustomerId);
                    c.DaysOverdue = days;
                    await _customerService.UpdateCustomerAsync(c);
                    customerList.Add(c);
                }
                else
                {
                    await ResetCustomerDueStatus(c.CustomerId);
                }
            }
            return customerList;
        } 

        public async Task<Customer> ResetCustomerDueStatus(int customerId)
        {
            var customerToReset = await _context.Customers.SingleOrDefaultAsync(c => c.CustomerId == customerId);
            customerToReset!.Overdue = false;
            customerToReset!.DaysOverdue = 0;
            await _customerService.UpdateCustomerAsync(customerToReset);
            return customerToReset;
        }

        public async Task<Customer> SetCustomersAsDue()
        {
            var overdueInfo = await _context.Deliveries
                .Where(d => d.QuantityToReturn != 0 && d.TransactionInfo!.DateDue.Day >= DateTime.Now.Day)
                .Select(d => new
                {
                    d.Inventory!.Customer!.CustomerId,
                    d.Inventory.Customer!.Overdue,
                })
                .Distinct()
                .ToListAsync();
            foreach (var c in overdueInfo)
            {
                var customer = await _context.Customers.SingleOrDefaultAsync(cus => cus.CustomerId == c.CustomerId);
                customer!.Overdue = true;
                await _customerService.UpdateCustomerAsync(customer);
            }
            return new Customer() { Overdue = true };
        }

        public async Task<Customer> SetACustomerAsDue(Customer customer)
        {
            var customerToSetDue = await _context.Customers.SingleOrDefaultAsync(c => c.CustomerId == customer.CustomerId);
            customerToSetDue!.Overdue = true;
            await _customerService.UpdateCustomerAsync(customerToSetDue);
            return customerToSetDue;
        }

        public async Task<bool> IsCustomerDue(Customer customer)
        {
            var customerDeliveries = await _context.Deliveries
                .Where(d => d.Inventory!.Customer!.CustomerId == customer.CustomerId)
                .Include(d => d.TransactionInfo)
                .ToListAsync();
            foreach (var c in customerDeliveries)
            {
                if (c.QuantityToReturn != 0 && c.TransactionInfo!.DateDue.Day <= DateTime.Now.Day)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<List<StockDelivery>> GetCustomerDueItems(string oib)
        {
            var customerDeliveries = await _context.Deliveries
                .Where(d => d.Inventory!.Customer!.Oib == oib
                && d.TransactionInfo!.DateDue.Day <= DateTime.Now.Day
                && d.QuantityToReturn != 0)
                .Include(d => d.Inventory!.Item)
                .Include(d => d.Inventory!.Customer)
                .Include(d => d.TransactionInfo)
                .ToListAsync();
            return customerDeliveries;
        }

        public async Task<int> GetCustomerDaysDue(int customerId)
        {
            List<int> daysOverdue = new();
            var today = DateTime.Now.Day;
            var customerDeliveries = await _context.Deliveries
                .Where(d => d.Inventory!.Customer!.CustomerId == customerId
                && d.TransactionInfo!.DateDue.Day <= DateTime.Now.Day
                && d.QuantityToReturn != 0)
                .Include(d => d.TransactionInfo)
                .ToListAsync();
            foreach (var item in customerDeliveries)
            {
                int delay =  today - item.TransactionInfo!.DateDue.Day;
                daysOverdue.Add(delay);
            }
            return daysOverdue.Max();
        }
    }
}
