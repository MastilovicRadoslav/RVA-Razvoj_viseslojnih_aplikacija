using Client.Commands.Track_Commands;
using Common.Contracts;
using Common.DomainModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Client.ViewModels
{
	// ViewModel za unos i ažuriranje podataka o prugama, koristi MVVM obrazac
	public class TrackInputViewModel : INotifyPropertyChanged
	{
		#region constructor
		// Konstruktor za inicijalizaciju TrackInputViewModel sa potrebnim zavisnostima
		public TrackInputViewModel(Track newTrack, ITrackService trackService, ILogging logger, Window window)
		{
			NewTrack = newTrack;
			TrackService = trackService;
			Logger = logger;
			Window = window;
			if (NewTrack.IsValid())
			{
				SaveTrackCommand = new UpdateTrackCommand(this);
			}
			else
			{
				SaveTrackCommand = new AddTrackCommand(this);
			}
		}
		#endregion

		#region Get set
		// Svojstva za novu prugu
		private Track newTrack;

		public Track NewTrack
		{
			get { return newTrack; }
			set
			{
				newTrack = value;
				OnPropertyChanged("NewTrack");
			}
		}

		// Svojstvo za tipove ulaza
		public IList<EntranceType> EntranceTypes
		{
			get
			{
				return Enum.GetValues(typeof(EntranceType)).Cast<EntranceType>().ToList<EntranceType>();
			}
		}

		// Referenca na prozor
		public Window Window { get; set; }
		#endregion

		#region Proxies
		// Proksi za servis za pruge
		public ITrackService TrackService { get; set; }
		#endregion

		#region Commands
		// Komande za čuvanje pruge
		public ICommand SaveTrackCommand { get; set; }
		#endregion

		#region Logger
		// Logger za logovanje poruka
		public ILogging Logger { get; set; }
		#endregion

		#region Functionalities
		// Funkcionalnosti za dodavanje i ažuriranje pruge
		public void AddTrack(Track track)
		{
			try
			{
				Logger.LogNewMessage($"Trying to add new track with name {track.Name}", LogType.INFO);
				TrackService.AddTrack(track);
				Window.Close();
			}
			catch (Exception ex)
			{
				Logger.LogNewMessage($"Error occured adding track with name {track.Name}. Message {ex.Message}", LogType.ERROR);
			}
		}

		public void UpdateTrack(Track track)
		{
			try
			{
				Logger.LogNewMessage($"Trying to update track with id {track.Id}", LogType.INFO);
				TrackService.UpdateTrack(track);
				Window.Close();
			}
			catch (Exception ex)
			{
				Logger.LogNewMessage($"Error occured updating track with id {track.Id}. Message {ex.Message}", LogType.ERROR);
			}
		}
		#endregion

		// Implementacija INotifyPropertyChanged interfejsa
		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
