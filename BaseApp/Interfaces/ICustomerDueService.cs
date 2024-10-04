using BaseApp.Models;

namespace BaseApp.Interfaces
{
    public interface ICustomerDueService
    {
        Task<List<Customer>> GetOverdueCustomers();
        Task<Customer> SetCustomersAsDue();
        Task<Customer> SetACustomerAsDue(Customer customer);
        Task<Customer> ResetCustomerDueStatus(int customerId);
    }
}
