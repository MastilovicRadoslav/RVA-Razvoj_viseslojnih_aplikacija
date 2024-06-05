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
	public class UserServiceProvider : IUserService //Klasa pruža razne operacije nad entitetom User.
	{
		private ILogging logger; // Logger za beleženje događaja
		private IDatabaseContextFactory factory; // Fabrika za kreiranje konteksta baze podataka

		/// <summary>
		/// Konstruktor klase UserServiceProvider
		/// </summary>
		/// <param name="logger">Instanca loggera</param>
		/// <param name="factory">Fabrika za kreiranje konteksta baze podataka</param>
		public UserServiceProvider(ILogging logger, IDatabaseContextFactory factory)
		{
			this.logger = logger;
			this.factory = factory;
		}

		/// <summary>
		/// Dodaje novog korisnika u bazu podataka
		/// </summary>
		/// <param name="user">Instanca korisnika</param>
		/// <returns>Vraća true ako je korisnik uspešno dodat</returns>
		public bool AddUser(User user)
		{
			try
			{
				logger.LogNewMessage($"Adding new user with username {user.UserName} to the database", LogType.INFO);
				using (var dbContext = factory.GetContext())
				{
					dbContext.Users.Add(user);
					dbContext.SaveChanges();
				}
				logger.LogNewMessage($"User added successfully.", LogType.INFO);
				return true;
			}
			catch (Exception ex)
			{
				logger.LogNewMessage($"Error occured, user couldn't be added. Message {ex.Message}", LogType.ERROR);
				throw new FaultException(ex.Message);
			}
		}

		/// <summary>
		/// Pronalazi korisnika po korisničkom imenu
		/// </summary>
		/// <param name="name">Korisničko ime</param>
		/// <returns>Instanca korisnika ako je pronađen, inače null</returns>
		public User FindUserByUserName(string name)
		{
			try
			{
				logger.LogNewMessage($"Trying to get user with name {name} from the database", LogType.INFO);
				using (var dbContext = factory.GetContext())
				{
					return dbContext.Users.SingleOrDefault(u => u.UserName == name);
				}
			}
			catch (Exception ex)
			{
				logger.LogNewMessage($"Error occured, user couldn't be found. Message {ex.Message}", LogType.ERROR);
				throw new FaultException(ex.Message);
			}
		}

		/// <summary>
		/// Dohvata korisničke podatke po ID-ju
		/// </summary>
		/// <param name="id">ID korisnika</param>
		/// <returns>Instanca korisnika ako je pronađen</returns>
		public User GetUserData(int id)
		{
			try
			{
				logger.LogNewMessage($"Trying to get user with id {id} from the database", LogType.INFO);
				using (var dbContext = factory.GetContext())
				{
					return dbContext.Users.Find(id);
				}
			}
			catch (Exception ex)
			{
				logger.LogNewMessage($"Error occured, user couldn't be found. Message {ex.Message}", LogType.ERROR);
				throw new FaultException(ex.Message);
			}
		}

		/// <summary>
		/// Ažurira korisničke podatke
		/// </summary>
		/// <param name="id">ID korisnika</param>
		/// <param name="newName">Novo ime korisnika</param>
		/// <param name="newLastName">Novo prezime korisnika</param>
		/// <returns>Vraća true ako su podaci uspešno ažurirani</returns>
		public bool UpdateUser(int id, string newName, string newLastName)
		{
			try
			{
				logger.LogNewMessage($"Trying to update user with id {id}", LogType.INFO);
				using (var dbContext = factory.GetContext())
				{
					User user = dbContext.Users.Find(id);
					user.Name = newName;
					user.LastName = newLastName;
					dbContext.SaveChanges();
				}
				logger.LogNewMessage($"User updated successfully.", LogType.INFO);
				return true;
			}
			catch (Exception ex)
			{
				logger.LogNewMessage($"Error occured, user data couldn't be updated. Message {ex.Message}", LogType.ERROR);
				throw new FaultException(ex.Message);
			}
		}

		/// <summary>
		/// Dohvata sve korisnike iz baze podataka
		/// </summary>
		/// <returns>Lista korisnika</returns>
		public List<User> GetAllUsers()
		{
			try
			{
				logger.LogNewMessage($"Retrieving all users..", LogType.INFO);
				using (var dbContext = factory.GetContext())
				{
					return dbContext.Users.ToList();
				}
			}
			catch (Exception ex)
			{
				logger.LogNewMessage($"Error occured, users couldn't be retrieved. Message {ex.Message}", LogType.ERROR);
				throw new FaultException(ex.Message);
			}
		}
	}
}
