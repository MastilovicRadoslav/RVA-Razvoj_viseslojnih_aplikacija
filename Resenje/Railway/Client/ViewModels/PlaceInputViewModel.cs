using Client.Commands.Place_commands;
using Common.Contracts;
using Common.DomainModels;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Client.ViewModels
{
	public class PlaceInputViewModel : INotifyPropertyChanged
	{
		#region constructor
		public PlaceInputViewModel(Place newPlace, IPlaceService placeService, ICountryService countryService, ILogging logger, Window window)
		{
			PlaceService = placeService;
			CountryService = countryService;
			Logger = logger;
			Window = window;
			NewPlace = newPlace;
			if (NewPlace.IsValid())
			{
				SavePlaceCommand = new UpdatePlaceCommand(this);
			}
			else
			{
				SavePlaceCommand = new AddPlaceCommand(this);
			}
			InitCountries();
		}
		#endregion

		#region Get set
		private Place newPlace;

		// Svojstvo za novi objekat Place sa obaveštavanjem o promenama
		public Place NewPlace
		{
			get
			{
				return newPlace;
			}
			set
			{
				newPlace = value;
				OnPropertyChanged("NewPlace");
			}
		}

		private BindingList<Country> countries;

		// Svojstvo za listu država sa obaveštavanjem o promenama
		public BindingList<Country> Countries
		{
			get
			{
				return countries;
			}
			set
			{
				countries = value;
				OnPropertyChanged("Countries");
			}
		}

		// Referenca na prozor
		public Window Window { get; set; }
		#endregion

		#region Proxies
		// Servisi za rad sa mestima i državama
		public IPlaceService PlaceService { get; set; }
		public ICountryService CountryService { get; set; }
		#endregion

		#region Commands
		// Komanda za čuvanje mesta (dodavanje ili ažuriranje)
		public ICommand SavePlaceCommand { get; set; }
		#endregion

		#region Logger
		// Logger za logovanje poruka
		public ILogging Logger { get; set; }
		#endregion

		#region Functionalities
		// Metoda za dodavanje novog mesta
		public void AddPlace(Place place)
		{
			try
			{
				Logger.LogNewMessage($"Trying to add new place with name {place.Name}", LogType.INFO);
				PlaceService.AddPlace(place);
				Window.Close();
			}
			catch (Exception ex)
			{
				Logger.LogNewMessage($"Error occured adding place with name {place.Name}. Message {ex.Message}", LogType.ERROR);
			}
		}

		// Metoda za ažuriranje postojećeg mesta
		public void UpdatePlace(Place place)
		{
			try
			{
				Logger.LogNewMessage($"Trying to update place with id {place.Id}", LogType.INFO);
				PlaceService.UpdatePlace(place);
				Window.Close();
			}
			catch (Exception ex)
			{
				Logger.LogNewMessage($"Error occured updating place with id {place.Id}. Message {ex.Message}", LogType.ERROR);
			}
		}
		#endregion

		#region Initializers
		// Metoda za inicijalizaciju liste država
		public void InitCountries()
		{
			Countries = new BindingList<Country>(CountryService.GetAll());
			if (NewPlace.IsValid())
			{
				NewPlace.Country = Countries.First(x => x.Id == NewPlace.Country.Id); // Neophodno jer početna vrednost u combo box-u nije postavljena.
																					  // Početna vrednost mora da referencira jednog od članova kolekcije
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