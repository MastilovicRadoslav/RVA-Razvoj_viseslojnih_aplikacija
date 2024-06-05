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
	public class RoadServiceProvider : IRoadService //Klasa pruža razne operacije nad entitetom Road.
	{
		private ILogging logger; // Logger za beleženje događaja
		private IDatabaseContextFactory factory; // Fabrika za kreiranje konteksta baze podataka

		/// <summary>
		/// Konstruktor klase RoadServiceProvider
		/// </summary>
		/// <param name="logger">Instanca loggera</param>
		/// <param name="factory">Fabrika za kreiranje konteksta baze podataka</param>
		public RoadServiceProvider(ILogging logger, IDatabaseContextFactory factory)
		{
			this.logger = logger;
			this.factory = factory;
		}

		/// <summary>
		/// Dodaje novi put u bazu podataka
		/// </summary>
		/// <param name="road">Instanca puta</param>
		/// <returns>Vraća instancu puta ako je uspešno dodat</returns>
		public Road AddRoad(Road road)
		{
			try
			{
				logger.LogNewMessage($"Adding new road to database", LogType.INFO);
				using (var dbContext = factory.GetContext())
				{
					foreach (Station s in road.Stations)
					{
						dbContext.Stations.Attach(s);
					}
					dbContext.Roads.Add(road);
					dbContext.SaveChanges();
					logger.LogNewMessage($"Road added.", LogType.INFO);
					return road;
				}
			}
			catch (Exception ex)
			{
				logger.LogNewMessage($"Error occured, couldn't add entity. Error message {ex.Message}", LogType.ERROR);
				throw new FaultException(ex.Message);
			}
		}

		/// <summary>
		/// Klonira postojeći put u bazu podataka
		/// </summary>
		/// <param name="road">Instanca puta</param>
		/// <returns>Vraća instancu kloniranog puta</returns>
		public Road CloneRoad(Road road)
		{
			try
			{
				logger.LogNewMessage($"Cloning road with name {road.Name} from database..", LogType.INFO);
				using (var dbContext = factory.GetContext())
				{
					foreach (Station s in road.Stations)
					{
						dbContext.Stations.Attach(s);
					}
					road.Id = 0;
					dbContext.Roads.Add(road);
					dbContext.SaveChanges();
					logger.LogNewMessage($"Road cloned.", LogType.INFO);
					return road;
				}
			}
			catch (Exception ex)
			{
				logger.LogNewMessage($"Error occured, road couldn't be cloned. Error message {ex.Message}", LogType.ERROR);
				throw new FaultException(ex.Message);
			}
		}

		/// <summary>
		/// Briše put iz baze podataka po ID-ju
		/// </summary>
		/// <param name="roadID">ID puta</param>
		/// <returns>Vraća true ako je put uspešno obrisan</returns>
		public bool DeleteRoad(int roadID)
		{
			try
			{
				logger.LogNewMessage($"Deleting road with id {roadID} from database..", LogType.INFO);
				using (var dbContext = factory.GetContext())
				{
					Road roadToDelete = new Road() { Id = roadID };
					dbContext.Roads.Attach(roadToDelete);
					dbContext.Roads.Remove(roadToDelete);
					dbContext.SaveChanges();
				}
				logger.LogNewMessage($"Road deleted.", LogType.INFO);
				return true;
			}
			catch (Exception ex)
			{
				logger.LogNewMessage($"Error occured, road couldn't be deleted. Error message {ex.Message}", LogType.ERROR);
				throw new FaultException(ex.Message);
			}
		}

		/// <summary>
		/// Dohvata sve puteve iz baze podataka
		/// </summary>
		/// <returns>Lista puteva</returns>
		public List<Road> GetAllRoads()
		{
			try
			{
				logger.LogNewMessage($"Getting all roads from database..", LogType.INFO);
				using (var dbContext = factory.GetContext())
				{
					return dbContext.Roads.Include("Stations").ToList();
				}
			}
			catch (Exception ex)
			{
				logger.LogNewMessage($"Error occured, message {ex.Message}", LogType.ERROR);
				throw new FaultException(ex.Message);
			}
		}

		/// <summary>
		/// Ažurira podatke o putu
		/// </summary>
		/// <param name="newData">Nova instanca puta</param>
		/// <returns>Vraća true ako su podaci uspešno ažurirani</returns>
		public bool UpdateRoad(Road newData)
		{
			try
			{
				logger.LogNewMessage($"Updating data for road with id {newData.Id}.", LogType.INFO);
				using (var dbContext = factory.GetContext())
				{
					foreach (Station s in newData.Stations) s.Tracks = null; // Ovo mora da se uradi da bi se izbegle izuzetke vezane za integritet baze podataka zbog detached state
					var road = dbContext.Roads.Where(p => p.Id == newData.Id)
											  .Include(p => p.Stations)
											  .SingleOrDefault();
					dbContext.Entry(road).CurrentValues.SetValues(newData);

					// Brisanje postojećih stanica koje nisu u novim podacima
					foreach (var existingStation in road.Stations.ToList())
					{
						if (!newData.Stations.Any(c => c.Id == existingStation.Id))
							road.Stations.Remove(existingStation);
					}

					// Dodavanje novih stanica koje nisu u postojećim podacima
					foreach (var station in newData.Stations)
					{
						if (!road.Stations.Any(x => x.Id == station.Id))
						{
							dbContext.Stations.Attach(station);
							road.Stations.Add(station);
						}
					}

					dbContext.SaveChanges();
				}
				logger.LogNewMessage($"Update successful", LogType.INFO);
				return true;
			}
			catch (Exception ex)
			{
				logger.LogNewMessage($"Error occured updating road  message:\n {ex.Message}", LogType.ERROR);
				throw new FaultException(ex.Message);
			}
		}

		/// <summary>
		/// Dohvata put po ID-ju
		/// </summary>
		/// <param name="id">ID puta</param>
		/// <returns>Instanca puta ako je pronađena</returns>
		public Road GetRoadById(int id)
		{
			try
			{
				logger.LogNewMessage($"Getting road with id {id} from database..", LogType.INFO);
				using (var dbContext = factory.GetContext())
				{
					var road = dbContext.Roads.Where(x => x.Id == id).Include("Stations").SingleOrDefault();
					logger.LogNewMessage($"Road retrieved.", LogType.INFO);
					return road;
				}
			}
			catch (Exception ex)
			{
				logger.LogNewMessage($"Error occured, message {ex.Message}", LogType.ERROR);
				throw new FaultException(ex.Message);
			}
		}

		/// <summary>
		/// Pretražuje puteve po imenu i oznaci
		/// </summary>
		/// <param name="name">Ime puta</param>
		/// <param name="label">Oznaka puta</param>
		/// <returns>Lista puteva koji odgovaraju kriterijumima</returns>
		public List<Road> SearchRoads(string name, string label)
		{
			try
			{
				logger.LogNewMessage($"Searching for roads..", LogType.INFO);
				bool shouldFilterName = name != "";
				bool shouldFilterLabel = label != "";
				using (var dbContext = factory.GetContext())
				{
					IQueryable<Road> result = null;
					if (shouldFilterName) result = dbContext.Roads.Where(x => x.Name == name);
					if (shouldFilterLabel && result != null)
					{
						result = result.Where(x => x.Label == label);
					}
					else if (shouldFilterLabel)
					{
						result = dbContext.Roads.Where(x => x.Label == label);
					}

					return result.Include("Stations").ToList();
				}
			}
			catch (Exception ex)
			{
				logger.LogNewMessage($"Error occured, message {ex.Message}", LogType.ERROR);
				throw new FaultException(ex.Message);
			}
		}
	}
}
