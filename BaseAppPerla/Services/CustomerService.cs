using BaseAppPerla.ExceptionHandling;
using BaseAppPerla.Interfaces;
using BaseAppPerla.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace BaseAppPerla.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient _httpClient;
        public CustomerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:44398/api");
        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}/Customer", customer);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var createdCustomer = JsonConvert.DeserializeObject<Customer>(responseContent);
                return createdCustomer!;
            }
            else
            {
                return null;
            }
        }

        public async Task<ServiceResult<Customer>> DeleteCustomerAsync(int customerId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_httpClient.BaseAddress}/Customer?customerId={customerId}");
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var deletedCustomer = JsonConvert.DeserializeObject<Customer>(responseContent);
                    return new ServiceResult<Customer> { Data = deletedCustomer!, ErrorMessage = string.Empty };
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    var errorMessage = JsonConvert.DeserializeObject<ErrorResponse>(errorResponse)?.Message;
                    return new ServiceResult<Customer> { ErrorMessage = errorMessage! };
                }
            }
            catch(Exception ex)
            {
                return new ServiceResult<Customer> { ErrorMessage = ex.Message };
            }
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
                    return customers!;
                }
                else
                {
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

        public async Task<Customer> GetCustomerByOibAsync(string oib)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Customer/oib?oib={oib}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var customer = JsonConvert.DeserializeObject<Customer>(content);
                    return customer!;
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
        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Customer/id?id={id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var customer = JsonConvert.DeserializeObject<Customer>(content);
                    return customer!;
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

        public async Task<Customer> UpdateCustomerAsync(Customer customer)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_httpClient.BaseAddress}/Customer", customer);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var customerToUpdate = JsonConvert.DeserializeObject<Customer>(content);
                    return customerToUpdate!;
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
