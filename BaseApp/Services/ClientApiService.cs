using BaseApp.Models;
using Newtonsoft.Json;

namespace BaseApp.Services
{
	public class ClientApiService : IClientApiService
	{
		private readonly HttpClient _httpClient;
		public ClientApiService(HttpClient httpClient)
		{
			_httpClient = httpClient;
			//_httpClient.BaseAddress = new Uri("https://praonaperla.azurewebsites.net/api/");
			_httpClient.BaseAddress = new Uri("https://localhost:44398/api/");
		}

		public async Task<List<Customer>> GetCustomers()
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
