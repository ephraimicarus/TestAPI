using BaseApp.Models;

namespace BaseApp.Services
{
	public interface IClientApiService
	{
		Task<List<Customer>> GetCustomers();
	}
}
