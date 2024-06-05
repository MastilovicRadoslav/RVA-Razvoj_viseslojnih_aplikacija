using Common.Contracts;
using Common.DomainModels;
using Server.Contracts;
using System;
using System.IdentityModel.Selectors;
using System.Linq;
using System.ServiceModel;

namespace Server.CustomValidators
{
	public class UsernamePasswordValidator : UserNamePasswordValidator //Ova klasa nasleđuje UserNamePasswordValidator i koristi se za validaciju korisničkog imena, i lozinke.
	{
		private ILogging logger; // Logger za beleženje događaja
		private IDatabaseContextFactory factory; // Fabrika za kreiranje konteksta baze podataka

		/// <summary>
		/// Konstruktor klase UsernamePasswordValidator
		/// </summary>
		/// <param name="factory">Fabrika za kreiranje konteksta baze podataka</param>
		/// <param name="logger">Instanca loggera</param>
		public UsernamePasswordValidator(IDatabaseContextFactory factory, ILogging logger)
		{
			this.logger = logger;
			this.factory = factory;
		}

		/// <summary>
		/// Validira korisničko ime i lozinku
		/// </summary>
		/// <param name="userName">Korisničko ime</param>
		/// <param name="password">Lozinka</param>
		public override void Validate(string userName, string password)
		{
			try
			{
				logger.LogNewMessage($"Validating username and password for user {userName}", LogType.INFO);

				using (var dbContext = factory.GetContext())
				{
					// Pronalazi korisnika sa zadatim korisničkim imenom
					var user = dbContext.Users.Where(x => x.UserName == userName).SingleOrDefault();

					// Proverava da li korisnik postoji
					if (user == null || user == new User())
					{
						var errorMessage = $"User with username {userName} does not exist in database. Access denied.";
						logger.LogNewMessage(errorMessage, LogType.WARNING);
						throw new FaultException(errorMessage);
					}

					// Proverava da li je lozinka ispravna
					if (!user.CheckPassword(password))
					{
						var errorMessage = $"Wrong password for user {userName}. Access denied.";
						logger.LogNewMessage(errorMessage, LogType.WARNING);
						throw new FaultException(errorMessage);
					}
				}

				logger.LogNewMessage($"User {userName} authenticated.", LogType.INFO);
			}
			catch (Exception ex)
			{
				logger.LogNewMessage($"Authentication exception. Error {ex.Message}", LogType.ERROR);
				throw new FaultException(ex.Message);
			}
		}
	}
}
