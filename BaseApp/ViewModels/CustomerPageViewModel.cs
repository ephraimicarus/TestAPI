using BaseApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BaseApp.ViewModels
{
	public class CustomerPageViewModel : ObservableObject
	{
		private readonly IClientApiService _clientApiService;
		public CustomerPageViewModel(IClientApiService clientApiService)
		{
			_clientApiService = clientApiService;
		}
	}
}
