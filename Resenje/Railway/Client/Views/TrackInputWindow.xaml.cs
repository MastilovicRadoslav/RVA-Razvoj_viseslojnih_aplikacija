using Client.Contracts;
using Client.ViewModels;
using Common.Contracts;
using Common.DomainModels;
using System.Windows;

namespace Client.Views
{
	/// <summary>
	/// Interaction logic for TrackInputWindow.xaml
	/// </summary>
	public partial class TrackInputWindow : Window
	{
		// Konstruktor za inicijalizaciju TrackInputWindow sa potrebnim zavisnostima
		public TrackInputWindow(IRailwayServiceProxyCreationFacade facade, User principal, ILogging logger, Track track)
		{
			// Kreiranje proxy servisa za rad sa prugama
			ITrackService trackService = facade.GetTrackServiceProxy(principal.UserName, principal.Password);

			// Kreiranje ViewModel-a za TrackInputWindow
			TrackInputViewModel viewModel = new TrackInputViewModel(track, trackService, logger, this);

			// Postavljanje DataContext-a prozora na kreirani ViewModel
			DataContext = viewModel;

			// Inicijalizacija komponenti XAML-a
			InitializeComponent();
		}
	}
}
