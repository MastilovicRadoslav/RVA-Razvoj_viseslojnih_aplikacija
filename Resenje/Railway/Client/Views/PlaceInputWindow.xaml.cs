using Client.Contracts;
using Client.ViewModels;
using Common.Contracts;
using Common.DomainModels;
using System.Windows;

namespace Client.Views
{
	/// <summary>
	/// Interaction logic for PlaceInputWindow.xaml
	/// </summary>
	public partial class PlaceInputWindow : Window
	{
		// Konstruktor za inicijalizaciju PlaceInputWindow sa neophodnim zavisnostima
		public PlaceInputWindow(IRailwayServiceProxyCreationFacade facade, User principal, ILogging logger, Place place)
		{
			// Kreiranje proxy servisa za rad sa mestima
			IPlaceService placeService = facade.GetPlaceServiceProxy(principal.UserName, principal.Password);

			// Kreiranje proxy servisa za rad sa državama
			ICountryService countryService = facade.GetCountryServiceProxy(principal.UserName, principal.Password);

			// Kreiranje ViewModel-a za PlaceInputWindow
			PlaceInputViewModel viewModel = new PlaceInputViewModel(place, placeService, countryService, logger, this);

			// Postavljanje DataContext-a prozora na kreirani ViewModel
			DataContext = viewModel;

			// Inicijalizacija komponenti XAML-a
			InitializeComponent();
		}
	}
}
