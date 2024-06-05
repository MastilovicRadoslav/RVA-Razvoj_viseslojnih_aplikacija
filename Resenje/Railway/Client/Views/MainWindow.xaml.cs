using Client.Connectors;
using Client.Loggers;
using Client.Managers;
using Client.ViewModels;
using Common.DomainModels;
using System.Windows;

namespace Client
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		// Konstruktor za inicijalizaciju MainWindow sa potrebnim zavisnostima
		public MainWindow(User user)
		{
			// Kreiranje instance MainViewModel-a sa svim potrebnim zavisnostima
			MainViewModel mainViewModel = new MainViewModel(user, this, new RailwayServiceConnector(), ClientLogger.GetOrCreate(), new PrimaryEntityCommandManager());

			// Postavljanje DataContext-a prozora na kreirani ViewModel
			DataContext = mainViewModel;

			// Registrovanje MainViewModel-a kao posmatrača događaja klijentskog događajnog menadžera
			ClientEventManager.GetOrCreate().RegisterEventObserver(mainViewModel);

			// Inicijalizacija komponenti definisanih u XAML-u
			InitializeComponent();
		}
	}
}
