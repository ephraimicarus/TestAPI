using MauiApp1.Views;

namespace MauiApp1
{
	public partial class App : Application
	{
		public App(HomePage homePage)
		{
			InitializeComponent();

			MainPage = homePage;
		}
	}
}
