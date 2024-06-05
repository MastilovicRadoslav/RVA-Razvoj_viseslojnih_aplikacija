using Common.Contracts;
using Common.DomainModels;
using Server.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace Server.ServiceProviders
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)] // Postavlja InstanceContextMode na Single, znači jedna instanca se koristi za sve zahteve
	public class CountryServiceProvider : ICountryService //Klasa pruža razne operacije nad entitetom Country.
	{
		private ILogging logger; // Logger za beleženje događaja
		private IDatabaseContextFactory factory; // Fabrika za kreiranje konteksta baze podataka

		/// <summary>
		/// Konstruktor klase CountryServiceProvider
		/// </summary>
		/// <param name="logger">Instanca loggera</param>
		/// <param name="factory">Fabrika za kreiranje konteksta baze podataka</param>
		public CountryServiceProvider(ILogging logger, IDatabaseContextFactory factory)
		{
			this.logger = logger;
			this.factory = factory;
		}

		/// <summary>
		/// Dohvata državu po ID-ju
		/// </summary>
		/// <param name="id">ID države</param>
		/// <returns>Instanca države ako je pronađena</returns>
		public Country GeCountryByID(int id)
		{
			try
			{
				logger.LogNewMessage($"Getting country by id {id}.", LogType.INFO);
				using (var dbContext = factory.GetContext())
				{
					return dbContext.Countries.Find(id);
				}
			}
			catch (Exception ex)
			{
				logger.LogNewMessage($"Error occured getting country by id {id}. Message {ex.Message}", LogType.ERROR);
				throw new FaultException(ex.Message);
			}
		}

		/// <summary>
		/// Dohvata državu po imenu
		/// </summary>
		/// <param name="name">Ime države</param>
		/// <returns>Instanca države ako je pronađena</returns>
		public Country GetCountryByName(string name)
		{
			try
			{
				logger.LogNewMessage($"Getting country by name {name}.", LogType.INFO);
				using (var dbContext = factory.GetContext())
				{
					return dbContext.Countries.Where(t => t.Name == name).Single();
				}
			}
			catch (Exception ex)
			{
				logger.LogNewMessage($"Error occured getting country by name {name}. Message {ex.Message}", LogType.ERROR);
				throw new FaultException(ex.Message);
			}
		}

		/// <summary>
		/// Dohvata sve države iz baze podataka
		/// </summary>
		/// <returns>Lista država</returns>
		public List<Country> GetAll()
		{
			try
			{
				logger.LogNewMessage($"Getting all countries from the database.", LogType.INFO);
				using (var dbContext = factory.GetContext())
				{
					return dbContext.Countries.ToList();
				}
			}
			catch (Exception ex)
			{
				logger.LogNewMessage($"Error occured getting all countries. Message {ex.Message}", LogType.ERROR);
				throw new FaultException(ex.Message);
			}
		}
	}
}
