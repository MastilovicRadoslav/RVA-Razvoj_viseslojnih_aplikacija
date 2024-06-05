using Common.Contracts;
using Common.DomainModels;
using Server.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.ServiceModel;

namespace Server.ServiceProviders
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	public class StationServiceProvider : IStationService   //Klasa pruža razne operacije nad entitetom Station.
	{
		private ILogging logger; // Logger za beleženje događaja
		private IDatabaseContextFactory factory; // Fabrika za kreiranje konteksta baze podataka

		/// <summary>
		/// Konstruktor klase StationServiceProvider
		/// </summary>
		/// <param name="logger">Instanca loggera</param>
		/// <param name="factory">Fabrika za kreiranje konteksta baze podataka</param>
		public StationServiceProvider(ILogging logger, IDatabaseContextFactory factory)
		{
			this.logger = logger;
			this.factory = factory;
		}

		/// <summary>
		/// Dodaje novu stanicu u bazu podataka
		/// </summary>
		/// <param name="station">Instanca stanice</param>
		/// <returns>Vraća true ako je stanica uspešno dodata</returns>
		public bool AddStation(Station station)
		{
			try
			{
				logger.LogNewMessage($"Adding new station to database", LogType.INFO);
				using (var dbContext = factory.GetContext())
				{
					// Povezivanje svih pruga sa stanicom
					foreach (Track track in station.Tracks)
					{
						dbContext.Track.Attach(track);
						track.StationId = station.Id;
					}
					dbContext.Stations.Attach(station);
					dbContext.Places.Attach(station.Place);
					dbContext.Stations.Add(station);
					dbContext.SaveChanges();
				}
				logger.LogNewMessage($"Station added successfully.", LogType.INFO);
				return true;
			}
			catch (Exception ex)
			{
				logger.LogNewMessage($"Error occured, station couldn't be added. Message {ex.Message}", LogType.ERROR);
				throw new FaultException(ex.Message);
			}
		}

		/// <summary>
		/// Menja podatke o stanici
		/// </summary>
		/// <param name="newData">Nova instanca stanice</param>
		/// <returns>Vraća true ako su podaci uspešno promenjeni</returns>
		public bool ChangeStation(Station newData)
		{
			try
			{
				logger.LogNewMessage($"Changing station with id {newData.Id}", LogType.INFO);
				using (var dbContext = factory.GetContext())
				{
					var station = dbContext.Stations.Where(p => p.Id == newData.Id)
													.Include(p => p.Tracks)
													.Include(p => p.Place)
													.SingleOrDefault();
					dbContext.Entry(station).CurrentValues.SetValues(newData);

					// Brisanje postojećih pruga koje nisu u novim podacima
					foreach (var existingTrack in station.Tracks.ToList())
					{
						if (!newData.Tracks.Any(c => c.Id == existingTrack.Id))
							station.Tracks.Remove(existingTrack);
					}

					// Dodavanje novih pruga koje nisu u postojećim podacima
					foreach (var track in newData.Tracks)
					{
						if (!station.Tracks.Any(x => x.Id == track.Id))
						{
							dbContext.Track.Attach(track);
							station.Tracks.Add(track);
						}
					}

					// Ažuriranje mesta
					if (station.Place != null && station.Place.Id != newData.Place.Id)
					{
						dbContext.Places.Attach(newData.Place);
						station.Place = newData.Place;
					}

					dbContext.SaveChanges();
				}
				logger.LogNewMessage($"Station updated successfully.", LogType.INFO);
				return true;
			}
			catch (Exception ex)
			{
				logger.LogNewMessage($"Error occured, station couldn't be updated. Message {ex.Message}", LogType.ERROR);
				throw new FaultException(ex.Message);
			}
		}

		/// <summary>
		/// Briše stanicu iz baze podataka po ID-ju
		/// </summary>
		/// <param name="stationID">ID stanice</param>
		/// <returns>Vraća true ako je stanica uspešno obrisana</returns>
		public bool DeleteStation(int stationID)
		{
			try
			{
				logger.LogNewMessage($"Deleting station with id {stationID} from database", LogType.INFO);
				using (var dbContext = factory.GetContext())
				{
					Station stationToDelete = dbContext.Stations.Where(x => x.Id == stationID).Include("Tracks").Single();
					dbContext.Stations.Attach(stationToDelete);
					dbContext.Stations.Remove(stationToDelete);
					dbContext.SaveChanges();
				}
				logger.LogNewMessage($"Station deleted successfully.", LogType.INFO);
				return true;
			}
			catch (Exception ex)
			{
				logger.LogNewMessage($"Error occured, station couldn't be deleted. Message {ex.Message}", LogType.ERROR);
				throw new FaultException(ex.Message);
			}
		}

		/// <summary>
		/// Dohvata sve stanice iz baze podataka
		/// </summary>
		/// <returns>Lista stanica</returns>
		public List<Station> GetAllStations()
		{
			try
			{
				logger.LogNewMessage($"Getting all stations from the database.", LogType.INFO);
				using (var dbContext = factory.GetContext())
				{
					return dbContext.Stations.Include("Place").Include("Tracks").ToList();
				}
			}
			catch (Exception ex)
			{
				logger.LogNewMessage($"Error occured, stations couldn't be retrieved. Message {ex.Message}", LogType.ERROR);
				throw new FaultException(ex.Message);
			}
		}

		/// <summary>
		/// Dohvata stanicu po ID-ju
		/// </summary>
		/// <param name="id">ID stanice</param>
		/// <returns>Instanca stanice ako je pronađena</returns>
		public Station GetStationByID(int id)
		{
			try
			{
				logger.LogNewMessage($"Getting station with id {id} from database", LogType.INFO);
				using (var dbContext = factory.GetContext())
				{
					return dbContext.Stations.Find(id);
				}
			}
			catch (Exception ex)
			{
				logger.LogNewMessage($"Error occured, station couldn't be retrieved. Message {ex.Message}", LogType.ERROR);
				throw new FaultException(ex.Message);
			}
		}
	}
}
