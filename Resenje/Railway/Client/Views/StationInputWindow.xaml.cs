using Client.Contracts;
using Client.ViewModels;
using Common.Contracts;
using Common.DomainModels;
using System.Windows;

namespace Client.Views
{
	/// <summary>
	/// Interaction logic for StationInputWindow.xaml
	/// </summary>
	public partial class StationInputWindow : Window
	{
		// Konstruktor za inicijalizaciju StationInputWindow sa potrebnim zavisnostima
		public StationInputWindow(User principal, IRailwayServiceProxyCreationFacade facade, Station predicate, ILogging logger)
		{
			// Kreiranje proxy servisa za rad sa mestima
			IPlaceService placeService = facade.GetPlaceServiceProxy(principal.UserName, principal.Password);

			// Kreiranje proxy servisa za rad sa prugama
			ITrackService trackService = facade.GetTrackServiceProxy(principal.UserName, principal.Password);

			// Kreiranje proxy servisa za rad sa stanicama
			IStationService stationService = facade.GetStationServiceProxy(principal.UserName, principal.Password);

			// Kreiranje ViewModel-a za StationInputWindow
			StationInputViewModel viewModel = new StationInputViewModel(predicate, stationService, trackService, placeService, logger, this);

			// Postavljanje DataContext-a prozora na kreirani ViewModel
			DataContext = viewModel;

			// Inicijalizacija komponenti XAML-a
			InitializeComponent();
		}
	}
}
