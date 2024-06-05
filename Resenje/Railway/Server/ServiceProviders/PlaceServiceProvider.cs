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
	public class PlaceServiceProvider : IPlaceService  //Klasa pruža razne operacije nad entitetom Place
	{
		private ILogging logger; // Logger za beleženje događaja
		private IDatabaseContextFactory factory; // Fabrika za kreiranje konteksta baze podataka

		/// <summary>
		/// Konstruktor klase PlaceServiceProvider
		/// </summary>
		/// <param name="logger">Instanca loggera</param>
		/// <param name="factory">Fabrika za kreiranje konteksta baze podataka</param>
		public PlaceServiceProvider(ILogging logger, IDatabaseContextFactory factory)
		{
			this.logger = logger;
			this.factory = factory;
		}

		/// <summary>
		/// Dodaje novo mesto u bazu podataka
		/// </summary>
		/// <param name="place">Instanca mesta</param>
		/// <returns>Vraća true ako je mesto uspešno dodato</returns>
		public bool AddPlace(Place place)
		{
			try
			{
				logger.LogNewMessage($"Adding new place with name {place.Name} and id {place.Id} to database..", LogType.INFO);
				using (var dbContext = factory.GetContext())
				{
					dbContext.Places.Attach(place);
					dbContext.Places.Add(place);
					dbContext.SaveChanges();
				}
				logger.LogNewMessage($"Place added.", LogType.INFO);
				return true;
			}
			catch (Exception ex)
			{
				logger.LogNewMessage($"Error occured, place couldn't be added. ERROR message : {ex.Message}", LogType.ERROR);
				throw new FaultException(ex.Message);
			}
		}

		/// <summary>
		/// Briše mesto iz baze podataka po ID-ju
		/// </summary>
		/// <param name="placeID">ID mesta</param>
		/// <returns>Vraća true ako je mesto uspešno obrisano</returns>
		public bool DeletePlace(int placeID)
		{
			try
			{
				logger.LogNewMessage($"Deleting place with id '{placeID}' from database..", LogType.INFO);
				using (var dbContext = factory.GetContext())
				{
					Place placeToDelete = dbContext.Places.Where(x => x.Id == placeID)
														  .Include("Country")
														  .Single();
					dbContext.Stations.Where(x => x.Place.Id == placeID)
									  .Include(x => x.Tracks).Load();
					dbContext.Places.Attach(placeToDelete);
					dbContext.Places.Remove(placeToDelete);
					dbContext.SaveChanges();
				}
				logger.LogNewMessage($"Place deleted.", LogType.INFO);
				return true;
			}
			catch (Exception ex)
			{
				logger.LogNewMessage($"Error occured, place couldn't be deleted. ERROR message : {ex.Message}", LogType.ERROR);
				throw new FaultException(ex.Message);
			}
		}

		/// <summary>
		/// Dohvata sva mesta iz baze podataka
		/// </summary>
		/// <returns>Lista mesta</returns>
		public List<Place> GetAllPlaces()
		{
			try
			{
				logger.LogNewMessage("Getting all places from database.", LogType.INFO);
				using (var dbContext = factory.GetContext())
				{
					return dbContext.Places.Include("Country").ToList();
				}
			}
			catch (Exception ex)
			{
				logger.LogNewMessage($"Error occured, place couldn't be retrieved. ERROR message : {ex.Message}", LogType.ERROR);
				throw new FaultException(ex.Message);
			}
		}

		/// <summary>
		/// Dohvata mesto po ID-ju
		/// </summary>
		/// <param name="id">ID mesta</param>
		/// <returns>Instanca mesta ako je pronađena</returns>
		public Place GetPlaceByID(int id)
		{
			try
			{
				logger.LogNewMessage($"Getting place with id '{id}' from database.", LogType.INFO);
				using (var dbContext = factory.GetContext())
				{
					return dbContext.Places.Find(id);
				}
			}
			catch (Exception ex)
			{
				logger.LogNewMessage($"Error occured getting place by id {id}. Message {ex.Message}", LogType.ERROR);
				throw new FaultException(ex.Message);
			}
		}

		/// <summary>
		/// Ažurira podatke o mestu
		/// </summary>
		/// <param name="newData">Nova instanca mesta</param>
		/// <returns>Vraća true ako su podaci uspešno ažurirani</returns>
		public bool UpdatePlace(Place newData)
		{
			try
			{
				logger.LogNewMessage($"Updating data for place with id '{newData.Id}'.", LogType.INFO);
				using (var dbContext = factory.GetContext())
				{
					var existingPlace = dbContext.Places.Where(x => x.Id == newData.Id).Include(c => c.Country).SingleOrDefault();
					dbContext.Entry(existingPlace).CurrentValues.SetValues(newData);
					if (existingPlace.Country.Id != newData.Country.Id)
					{
						// Povezivanje nove zemlje kako bi je dbContext prepoznao
						dbContext.Countries.Attach(newData.Country);
						existingPlace.Country = newData.Country;
					}
					dbContext.SaveChanges();
				}
				return true;
			}
			catch (Exception ex)
			{
				logger.LogNewMessage($"Error occured, place couldn't be updated. ERROR message : {ex.Message}", LogType.ERROR);
				throw new FaultException(ex.Message);
			}
		}
	}
}
