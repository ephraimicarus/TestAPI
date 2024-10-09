using BaseAppPerla.Interfaces;
using BaseAppPerla.Models;
using Newtonsoft.Json;

namespace BaseAppPerla.Services
{
    public class CustomerDueService : ICustomerDueService
    {
        private readonly HttpClient _httpClient;
        public CustomerDueService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:44398/api");
        }

        public async Task<List<Customer>> GetOverdueCustomers()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/CustomerDue/overdue");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var customers = JsonConvert.DeserializeObject<List<Customer>>(content);
                    return customers!;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<StockDelivery>> GetCustomerDueItems(string oib)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/CustomerDue/items?oib={oib}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var customers = JsonConvert.DeserializeObject<List<StockDelivery>>(content);
                    return customers!;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Customer> ResetCustomerDueStatus(int customerId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/CustomerDue/overdue");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var customers = JsonConvert.DeserializeObject<Customer>(content);
                    return customers!;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Customer> SetACustomerAsDue(Customer customer)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Customer");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var customers = JsonConvert.DeserializeObject<Customer>(content);
                    return customers!;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Customer> SetCustomersAsDue()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Customer");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var customers = JsonConvert.DeserializeObject<Customer>(content);
                    return customers!;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
