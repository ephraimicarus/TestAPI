using TestAPI.Models;

namespace BaseApi.Interfaces
{
    public interface ICustomerDueService
    {
        Task<List<Customer>> GetOverdueCustomers();
        Task<Customer> SetCustomersAsDue();
        Task<Customer> SetACustomerAsDue(Customer customer);
        Task<Customer> ResetCustomerDueStatus(int customerId);
        Task<bool> IsCustomerDue(Customer customer);
        Task<List<StockDelivery>> GetCustomerDueItems(string oib);
        Task<int> GetCustomerDaysDue(int customerId);
    }
}
