using TestAPI.Models;

namespace TestAPI.Services
{
	public interface ICustomerService
	{
		Task<Customer> CreateCustomerAsync(Customer customer);
		Task<Customer> UpdateCustomerAsync(Customer customer);
		Task<Customer> DeleteCustomerAsync(Customer customer);
		Task<Customer> GetCustomerByOibAsync(string oib);
        Task<Customer> GetCustomerByIdAsync(int id);
        Task<List<Customer>> GetAllCustomersAsync();
	}
}
