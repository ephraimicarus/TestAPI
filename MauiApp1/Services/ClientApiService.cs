using MauiApp1.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace MauiApp1.Services
{
	public class ClientApiService : IClientApiService
	{
		private readonly HttpClient _httpClient;
		public ClientApiService()
		{
			_httpClient = new HttpClient();
			//_httpClient.BaseAddress = new Uri("https://praonaperla.azurewebsites.net/api/");
			_httpClient.BaseAddress = new Uri("https://localhost:44398/api/");
		}

		public async Task<Customer> CreateCustomerAsync(Customer customer)
		{
			HttpResponseMessage response = await _httpClient.PostAsJsonAsync("Customer", customer);
			if (response.IsSuccessStatusCode)
				return customer;
			else
				return null;
		}

		public async Task<List<Customer>> GetAllCustomersAsync()
		{
			HttpResponseMessage response = await _httpClient.GetAsync("Customer");
			var content = await response.Content.ReadAsStringAsync();
			if (response.IsSuccessStatusCode)
				return JsonConvert.DeserializeObject<List<Customer>>(content) ?? new List<Customer>();
			else			
				return new List<Customer>();
		}
	}
}
