using Common.Contracts;
using Common.DomainModels;
using Server.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace Server.ServiceProviders
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	public class TrackServiceProvider : ITrackService
	{
		private ILogging logger; // Logger za beleženje događaja
		private IDatabaseContextFactory factory; // Fabrika za kreiranje konteksta baze podataka

		/// <summary>
		/// Konstruktor klase TrackServiceProvider
		/// </summary>
		/// <param name="logger">Instanca loggera</param>
		/// <param name="factory">Fabrika za kreiranje konteksta baze podataka</param>
		public TrackServiceProvider(ILogging logger, IDatabaseContextFactory factory)
		{
			this.logger = logger;
			this.factory = factory;
		}

		/// <summary>
		/// Dodaje novu prugu u bazu podataka
		/// </summary>
		/// <param name="track">Instanca pruge</param>
		/// <returns>Vraća true ako je pruga uspešno dodata</returns>
		public bool AddTrack(Track track)
		{
			try
			{
				logger.LogNewMessage($"Adding new track to the database", LogType.INFO);
				using (var dbContext = factory.GetContext())
				{
					dbContext.Track.Attach(track);
					dbContext.Track.Add(track);
					dbContext.SaveChanges();
				}
				logger.LogNewMessage($"Track added successfully.", LogType.INFO);
				return true;
			}
			catch (Exception ex)
			{
				logger.LogNewMessage($"Error occured, track couldn't be added. Message {ex.Message}", LogType.ERROR);
				throw new FaultException(ex.Message);
			}
		}

		/// <summary>
		/// Briše prugu iz baze podataka po ID-ju
		/// </summary>
		/// <param name="trackID">ID pruge</param>
		/// <returns>Vraća true ako je pruga uspešno obrisana</returns>
		public bool DeleteTrack(int trackID)
		{
			try
			{
				logger.LogNewMessage($"Deleting track with id {trackID} from the database", LogType.INFO);
				using (var dbContext = factory.GetContext())
				{
					Track track = new Track() { Id = trackID };
					dbContext.Track.Attach(track);
					dbContext.Track.Remove(track);
					dbContext.SaveChanges();
				}
				logger.LogNewMessage($"Track deleted successfully.", LogType.INFO);
				return true;
			}
			catch (Exception ex)
			{
				logger.LogNewMessage($"Error occured, track couldn't be deleted. Message {ex.Message}", LogType.ERROR);
				throw new FaultException(ex.Message);
			}
		}

		/// <summary>
		/// Dohvata sve pruge iz baze podataka
		/// </summary>
		/// <returns>Lista pruga</returns>
		public List<Track> GetAllTracks()
		{
			try
			{
				logger.LogNewMessage($"Getting all tracks from the database.", LogType.INFO);
				using (var dbContext = factory.GetContext())
				{
					return dbContext.Track.ToList();
				}
			}
			catch (Exception ex)
			{
				logger.LogNewMessage($"Error occured, tracks couldn't be retrieved. Message {ex.Message}", LogType.ERROR);
				throw new FaultException(ex.Message);
			}
		}

		/// <summary>
		/// Dohvata prugu po ID-ju
		/// </summary>
		/// <param name="id">ID pruge</param>
		/// <returns>Instanca pruge ako je pronađena</returns>
		public Track GetTrackByID(int id)
		{
			try
			{
				logger.LogNewMessage($"Getting track with id {id} from the database.", LogType.INFO);
				using (var dbContext = factory.GetContext())
				{
					return dbContext.Track.Find(id);
				}
			}
			catch (Exception ex)
			{
				logger.LogNewMessage($"Error occured, track couldn't be retrieved. Message {ex.Message}", LogType.ERROR);
				return null;
			}
		}

		/// <summary>
		/// Ažurira podatke o pruzi
		/// </summary>
		/// <param name="newData">Nova instanca pruge</param>
		/// <returns>Vraća true ako su podaci uspešno ažurirani</returns>
		public bool UpdateTrack(Track newData)
		{
			try
			{
				logger.LogNewMessage($"Updating track with id {newData.Id}", LogType.INFO);
				using (var dbContext = factory.GetContext())
				{
					dbContext.Entry(dbContext.Track.Find(newData.Id)).CurrentValues.SetValues(newData);
					dbContext.SaveChanges();
				}
				logger.LogNewMessage($"Track updated successfully.", LogType.INFO);
				return true;
			}
			catch (Exception ex)
			{
				logger.LogNewMessage($"Error occured, track couldn't be updated. Message {ex.Message}", LogType.ERROR);
				throw new FaultException(ex.Message);
			}
		}

		/// <summary>
		/// Dohvata sve pruge koje nisu vezane za stanicu
		/// </summary>
		/// <returns>Lista pruga koje nisu vezane za stanicu</returns>
		public List<Track> GetUnattachedTracks()
		{
			try
			{
				logger.LogNewMessage($"Getting all unattached tracks from the database.", LogType.INFO);
				using (var dbContext = factory.GetContext())
				{
					return dbContext.Track.Where(x => x.StationId == null).ToList();
				}
			}
			catch (Exception ex)
			{
				logger.LogNewMessage($"Error occured, tracks couldn't be retrieved. Message {ex.Message}", LogType.ERROR);
				throw new FaultException(ex.Message);
			}
		}
	}
}
