using BaseApp.Models;
using BaseApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BaseApp.ViewModels
{
	public class HomePageViewModel : ObservableObject
	{
		private readonly IClientApiService _clientApiService;
		public List<Customer> Customers { get; private set; }
		public HomePageViewModel(IClientApiService clientApiService)
		{
			_clientApiService = clientApiService;
			Customers = new List<Customer>();
			LoadData();
		}
		private async void LoadData()
		{
			try
			{
				Customers = await _clientApiService.GetCustomers();
			}
			catch (Exception ex)
			{
				throw;
			}
		}
	}
}

