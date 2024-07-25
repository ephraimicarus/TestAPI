using BaseApp.Interfaces;
using BaseApp.Models;
using Newtonsoft.Json;

namespace BaseApp.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient _httpClient;
        public CustomerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:44398/api");
        }
        public Task<Customer> CreateCustomerAsync(Customer customer)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> DeleteCustomerAsync(Customer customer)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Customer");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var customers = JsonConvert.DeserializeObject<List<Customer>>(content);
                    return customers;
                }
                else
                {
                    // Handle error (e.g., log, show a message to the user)
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., network issues)
                Console.WriteLine($"Error fetching customers: {ex.Message}");
                return null;
            }
        }

        public Task<Customer> GetCustomerByOibAsync(string oib)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> UpdateCustomerAsync(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
