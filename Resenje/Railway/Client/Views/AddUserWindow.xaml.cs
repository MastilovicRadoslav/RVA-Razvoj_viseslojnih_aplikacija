using Client.Contracts;
using Client.ViewModels;
using Common.Contracts;
using Common.DomainModels;
using System.Windows;

namespace Client.Views
{
	/// <summary>
	/// Interaction logic for AddUserWindow.xaml
	/// </summary>
	public partial class AddUserWindow : Window
	{
		// Konstruktor za inicijalizaciju AddUserWindow sa glavnim korisnikom, konektorom i logger-om
		public AddUserWindow(User principal, IRailwayServiceProxyCreationFacade connector, ILogging logger)
		{
			// Dobija korisnički servis za glavnog korisnika preko konektora
			IUserService userService = connector.GetUserServiceProxy(principal.UserName, principal.Password);

			// Kreira ViewModel za AddUserWindow
			AddUserViewModel viewModel = new AddUserViewModel(userService, logger, principal, new User(), this);

			// Postavlja DataContext prozora na kreirani ViewModel
			DataContext = viewModel;

			// Inicijalizuje komponente XAML-a
			InitializeComponent();
		}
	}
}
