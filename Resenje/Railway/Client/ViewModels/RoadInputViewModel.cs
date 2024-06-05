using Client.Commands.Road_commands;
using Client.Contracts;
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
	public class RoadInputViewModel : INotifyPropertyChanged
	{
		#region constructor
		public RoadInputViewModel(IRoadService roadsService, IStationService stationService, Road newRoad, ILogging logger, Window window, IPrimaryEntityCommandManagement manager)
		{
			RoadsService = roadsService;
			StationService = stationService;
			NewRoad = newRoad;
			Logger = logger;
			Window = window;
			if (newRoad.IsValid())
			{
				SaveRoadCommand = new UpdateRoadCommand(this, manager);
			}
			else
			{
				SaveRoadCommand = new AddRoadCommand(this, manager);
			}
			AttachStationsToRoadCommand = new AttachStationsToRoadCommand(this);
			RemoveAttachedStationsCommand = new RemoveAttachedStationsCommand(this);
			InitBindingLists();
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

		#region get set
		// Svojstva za servise, logger, komandu i prozor
		public IRoadService RoadsService { get; set; }
		public IStationService StationService { get; set; }
		public ILogging Logger { get; set; }
		public IPrimaryEntityCommandManagement PrimaryEntityCommandManager { get; set; }
		public Window Window { get; set; }
		#endregion

		#region commands
		// Komande za čuvanje puta, povezivanje i uklanjanje stanica
		public IPrimaryEntityCommand SaveRoadCommand { get; set; } // Ovo će biti zamenjeno sa komandom za dodavanje ili ažuriranje puta
		public ICommand AttachStationsToRoadCommand { get; set; }
		public ICommand RemoveAttachedStationsCommand { get; set; }
		#endregion

		#region binding 
		// Svojstva za binding sa UI-jem
		public Road newRoad;
		public Road NewRoad
		{
			get
			{
				return newRoad;
			}
			set
			{
				newRoad = value;
				OnPropertyChanged("NewRoad");
			}
		}

		private BindingList<Station> stations;
		public BindingList<Station> AllStations
		{
			get
			{
				return stations;
			}
			set
			{
				stations = value;
				OnPropertyChanged("AllStations");
			}
		}

		private BindingList<Station> selectedStations;
		public BindingList<Station> SelectedStations
		{
			get
			{
				return selectedStations;
			}
			set
			{
				selectedStations = value;
				OnPropertyChanged("SelectedStations");
			}
		}
		#endregion

		#region Functionalities
		// Funkcionalnosti za dodavanje i uklanjanje stanica sa puta
		public void AddStationsToRoad(Station station)
		{
			AllStations.Remove(station);
			SelectedStations.Add(station);
			if (NewRoad.Stations != null)
			{
				var stations = NewRoad.Stations.ToList();
				stations.Add(station);
				NewRoad.Stations = stations;
			}
			else
			{
				NewRoad.Stations = new List<Station>() { station };
			}
		}

		public void RemoveStationsFromRoad(Station station)
		{
			SelectedStations.Remove(station);
			AllStations.Add(station);
			var stations = NewRoad.Stations.ToList();
			stations.Remove(NewRoad.Stations.First(x => x.Id == station.Id));
			NewRoad.Stations = stations;
		}

		// Funkcionalnosti za dodavanje, ažuriranje, brisanje i dobijanje podataka o putevima
		public Road AddNewRoad(Road road)
		{
			try
			{
				Logger.LogNewMessage($"Adding new road with name {road.Name}", LogType.INFO);
				var road_added = RoadsService.AddRoad(road);
				Window.Close();
				return road_added;
			}
			catch (Exception ex)
			{
				Logger.LogNewMessage($"Error adding road {road.Name}. Message  {ex.Message}", LogType.ERROR);
				return null;
			}
		}

		public void UpdateRoad(Road road)
		{
			try
			{
				Logger.LogNewMessage($"Updating road with name {road.Name}", LogType.INFO);
				RoadsService.UpdateRoad(road);
				Window.Close();
			}
			catch (Exception ex)
			{
				Logger.LogNewMessage($"Error updating road {road.Name}. Message  {ex.Message}", LogType.ERROR);
			}
		}

		public Road GetRoadPreviousState(int id)
		{
			try
			{
				Logger.LogNewMessage($"Getting previous state for road with id {id}", LogType.INFO);
				return RoadsService.GetRoadById(id);
			}
			catch (Exception ex)
			{
				Logger.LogNewMessage($"Error getting road with id {id}. Message  {ex.Message}", LogType.ERROR);
				return null;
			}
		}

		public void DeleteRoad(Road road)
		{
			try
			{
				Logger.LogNewMessage($"Deleting road with id {road.Id}", LogType.INFO);
				RoadsService.DeleteRoad(road.Id);
			}
			catch (Exception ex)
			{
				Logger.LogNewMessage($"Error deleting road with id {road.Id}. Message  {ex.Message}", LogType.ERROR);
			}
		}
		#endregion

		#region data initialization
		// Inicijalizacija lista za binding sa UI-jem
		public void InitBindingLists()
		{
			SelectedStations = new BindingList<Station>(NewRoad.Stations.ToList());
			AllStations = new BindingList<Station>(StationService.GetAllStations());
			foreach (Station station in SelectedStations)
			{
				AllStations.Remove(AllStations.Where(s => s.Id == station.Id).SingleOrDefault());
			}
		}
		#endregion
	}
}