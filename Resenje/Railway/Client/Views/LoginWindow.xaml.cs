using Client.Connectors;
using Client.Loggers;
using Client.ViewModels;
using System.Windows;

namespace Client
{
	/// <summary>
	/// Interaction logic for LoginWindow.xaml
	/// </summary>
	public partial class LoginWindow : Window
	{
		public LoginWindow()
		{
			InitializeComponent();
			DataContext = new LoginViewModel(this, ClientLogger.GetOrCreate(), new RailwayServiceConnector());
		}
	}
}
