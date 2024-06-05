using Client.Commands.Station_commands;
using Common.Contracts;
using Common.DomainModels;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Client.ViewModels
{
	// ViewModel za unos i ažuriranje podataka o stanicama, koristi MVVM obrazac
	public class StationInputViewModel : INotifyPropertyChanged
	{
		#region Constructor
		// Konstruktor za inicijalizaciju StationInputViewModel sa potrebnim zavisnostima
		public StationInputViewModel(Station newStation, IStationService stationService, ITrackService trackService, IPlaceService placeService, ILogging logger, Window window)
		{
			NewStation = newStation;
			StationService = stationService;
			TrackService = trackService;
			PlaceService = placeService;
			Logger = logger;
			Window = window;
			if (NewStation.IsValid())
			{
				SaveStationCommand = new UpdateStationCommand(this);
			}
			else
			{
				SaveStationCommand = new AddStationCommand(this);
			}
			AttachTrackToStationCommand = new AttachTrackToStationCommand(this);
			RemoveAttachedTrackCommand = new RemoveAttachedTrackCommand(this);
			InitLists();
		}
		#endregion

		#region property changed
		// Implementacija INotifyPropertyChanged interfejsa
		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		#endregion

		#region Commands
		// Komande za čuvanje stanice, povezivanje i uklanjanje pruga
		public ICommand SaveStationCommand { get; set; }
		public ICommand AttachTrackToStationCommand { get; set; }
		public ICommand RemoveAttachedTrackCommand { get; set; }
		#endregion

		#region Get Set
		// Svojstva za novu stanicu
		private Station newStation;
		public Station NewStation
		{
			get
			{
				return newStation;
			}
			set
			{
				newStation = value;
				OnPropertyChanged("NewStation");
			}
		}
		#endregion

		#region Binding Lists
		// Binding liste za mesta, sve pruge i izabrane pruge
		private BindingList<Place> places;
		public BindingList<Place> Places
		{
			get
			{
				return places;
			}
			set
			{
				places = value;
				OnPropertyChanged("Places");
			}
		}

		private BindingList<Track> allTracks;
		public BindingList<Track> AllTracks
		{
			get
			{
				return allTracks;
			}
			set
			{
				allTracks = value;
				OnPropertyChanged("AllTracks");
			}
		}

		private BindingList<Track> selectedTracks;
		public BindingList<Track> SelectedTracks
		{
			get
			{
				return selectedTracks;
			}
			set
			{
				selectedTracks = value;
				OnPropertyChanged("SelectedTracks");
			}
		}
		#endregion

		#region Proxies
		// Proksiji za servise
		public IStationService StationService { get; set; }
		public ITrackService TrackService { get; set; }
		public IPlaceService PlaceService { get; set; }
		#endregion

		#region Logger
		// Logger za logovanje poruka
		public ILogging Logger { get; set; }
		#endregion

		#region Window
		// Referenca na prozor
		public Window Window { get; set; }
		#endregion

		#region Functionalities
		// Funkcionalnosti za dodavanje, ažuriranje, povezivanje i uklanjanje stanica sa prugama
		public void AddStation(Station station)
		{
			try
			{
				Logger.LogNewMessage($"Trying to add new station with name {station.Name}", LogType.INFO);
				station.Tracks = SelectedTracks;
				StationService.AddStation(station);
				Window.Close();
			}
			catch (Exception ex)
			{
				Logger.LogNewMessage($"Error occured adding station with name {station.Name}. Message {ex.Message}", LogType.ERROR);
			}
		}

		public void UpdateStation(Station station)
		{
			try
			{
				Logger.LogNewMessage($"Trying to update station with id {station.Id}", LogType.INFO);
				station.Tracks = SelectedTracks;
				StationService.ChangeStation(station);
				Window.Close();
			}
			catch (Exception ex)
			{
				Logger.LogNewMessage($"Error occured updating station with id {station.Id}. Message {ex.Message}", LogType.ERROR);
			}
		}

		public void AttachTrackToStation(Track track)
		{
			SelectedTracks.Add(track);
			AllTracks.Remove(track);
		}

		public void RemoveTrackFromStation(Track track)
		{
			SelectedTracks.Remove(track);
			AllTracks.Add(track);
		}
		#endregion

		#region Initializers
		// Inicijalizacija lista za binding sa UI-jem
		public void InitLists()
		{
			SelectedTracks = new BindingList<Track>(NewStation.Tracks.ToList());
			Places = new BindingList<Place>(PlaceService.GetAllPlaces());
			AllTracks = new BindingList<Track>(TrackService.GetUnattachedTracks());
			if (NewStation.IsValid())
			{
				NewStation.Place = Places.First(x => x.Id == NewStation.Place.Id); // Neophodno za pravilnu inicijalizaciju vrednosti u combo box-u
			}
		}
		#endregion
	}
}
