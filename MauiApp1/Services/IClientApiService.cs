using MauiApp1.Models;

namespace MauiApp1.Services
{
	public interface IClientApiService
	{
		Task<Customer> CreateCustomerAsync(Customer customer);
		Task<List<Customer>> GetAllCustomersAsync();
	}
}
