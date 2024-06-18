using MauiApp1.Models;
using MauiApp1.Services;

namespace MauiApp1
{
	public partial class MainPage : ContentPage
	{
		private readonly IClientApiService _clientApiService;
		public MainPage(IClientApiService clientApiService)
		{
			InitializeComponent();
			_clientApiService = clientApiService;
		}

		private async void SaveCustomerBtn_Clicked(object sender, EventArgs e)
		{
			Customer customer = new()
			{
				Name = CustomerNameEntry.Text,
				Oib = CustomerOibEntry.Text,
			};
			await _clientApiService.CreateCustomerAsync(customer);
		}
	}
}
