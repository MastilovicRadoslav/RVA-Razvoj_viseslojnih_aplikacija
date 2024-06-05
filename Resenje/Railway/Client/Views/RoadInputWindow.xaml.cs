using Client.Contracts;
using Client.ViewModels;
using Common.Contracts;
using Common.DomainModels;
using System.Windows;

namespace Client.Views
{
	/// <summary>
	/// Interaction logic for RoadInputWindow.xaml
	/// </summary>
	public partial class RoadInputWindow : Window
	{
		// Konstruktor za inicijalizaciju RoadInputWindow sa potrebnim zavisnostima
		public RoadInputWindow(User user, IRailwayServiceProxyCreationFacade facade, Road predicate, ILogging logger, IPrimaryEntityCommandManagement manager)
		{
			// Kreiranje proxy servisa za rad sa putevima
			IRoadService roadService = facade.GetRoadServiceProxy(user.UserName, user.Password);

			// Kreiranje proxy servisa za rad sa stanicama
			IStationService stationService = facade.GetStationServiceProxy(user.UserName, user.Password);

			// Kreiranje ViewModel-a za RoadInputWindow
			RoadInputViewModel viewModel = new RoadInputViewModel(roadService, stationService, predicate, logger, this, manager);

			// Postavljanje DataContext-a prozora na kreirani ViewModel
			DataContext = viewModel;

			// Inicijalizacija komponenti XAML-a
			InitializeComponent();
		}
	}
}
