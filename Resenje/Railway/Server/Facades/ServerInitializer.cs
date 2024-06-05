using Common.Contracts;
using Server.Contracts;
using Server.CustomValidators;
using Server.Database;
using Server.Factories;
using Server.Loggers;
using Server.ServiceHosts;
using Server.ServiceProviders;
using System;
using System.ServiceModel;

namespace Server.Facades
{
	public class ServerInitializer  //Klasa ServerInitializer odgovorna je za inicijalizaciju servera, uključujući postavljanje, baze podataka, servisa, servisnih hostova i otvaranje komunikacionih kanala. 
	{
		#region logger
		private ILogging logger = ServerLogger.GetOrCreate(); // Instancira logger
		#endregion

		#region Service providers
		private ICountryService countryService;
		private IPlaceService placeService;
		private IRoadService roadService;
		private IStationService stationService;
		private ITrackService trackService;
		private IUserService userService;
		#endregion

		#region Service hosts
		private RailwayServiceHost roadHost;
		private RailwayServiceHost stationHost;
		private RailwayServiceHost trackHost;
		private RailwayServiceHost placeHost;
		private RailwayServiceHost countryHost;
		private RailwayServiceHost userHost;
		#endregion

		// Konstruktor bez parametara
		public ServerInitializer()
		{
		}

		// Metoda za pokretanje servera
		public void StartServer()
		{
			InitializeDatabaseData(); // Inicijalizuje podatke u bazi
			InitializeServices(); // Inicijalizuje servise
			InitializeServiceHosts(); // Inicijalizuje servisne hostove
			OpenChannels(); // Otvara komunikacione kanale
		}

		// Kreira instancu validatora za korisničko ime i lozinku
		private UsernamePasswordValidator GetValidator()
		{
			return new UsernamePasswordValidator(GetDatabaseContextFactory(), logger);
		}

		// Kreira instancu fabrike za kreiranje konteksta baze podataka
		private IDatabaseContextFactory GetDatabaseContextFactory()
		{
			return new DatabaseContextFactory();
		}

		// Inicijalizuje podatke u bazi podataka
		private void InitializeDatabaseData()
		{
			DataInitializer initializer = new DataInitializer(logger, GetDatabaseContextFactory());
			initializer.InitializeData();
		}

		// Inicijalizuje servise
		private void InitializeServices()
		{
			try
			{
				logger.LogNewMessage($"Initializing service providers.", LogType.INFO);

				countryService = new CountryServiceProvider(ServerLogger.GetOrCreate(), GetDatabaseContextFactory());
				placeService = new PlaceServiceProvider(ServerLogger.GetOrCreate(), GetDatabaseContextFactory());
				roadService = new RoadServiceProvider(ServerLogger.GetOrCreate(), GetDatabaseContextFactory());
				stationService = new StationServiceProvider(ServerLogger.GetOrCreate(), GetDatabaseContextFactory());
				trackService = new TrackServiceProvider(ServerLogger.GetOrCreate(), GetDatabaseContextFactory());
				userService = new UserServiceProvider(ServerLogger.GetOrCreate(), GetDatabaseContextFactory());
			}
			catch (Exception ex)
			{
				logger.LogNewMessage($"Error occured initializing service providers. ERROR {ex.Message}", LogType.ERROR);
			}
		}

		// Inicijalizuje servisne hostove
		private void InitializeServiceHosts()
		{
			logger.LogNewMessage($"Initializing service hosts..", LogType.INFO);

			roadHost = new RailwayServiceHost("http://localhost:18000/IRoadService", new BasicHttpBinding(), GetValidator(), new ServiceHost(roadService), typeof(IRoadService), logger);
			stationHost = new RailwayServiceHost("http://localhost:18000/IStationService", new BasicHttpBinding(), GetValidator(), new ServiceHost(stationService), typeof(IStationService), logger);
			trackHost = new RailwayServiceHost("http://localhost:18000/ITrackService", new BasicHttpBinding(), GetValidator(), new ServiceHost(trackService), typeof(ITrackService), logger);
			placeHost = new RailwayServiceHost("http://localhost:18000/IPlaceService", new BasicHttpBinding(), GetValidator(), new ServiceHost(placeService), typeof(IPlaceService), logger);
			countryHost = new RailwayServiceHost("http://localhost:18000/ICountryService", new BasicHttpBinding(), GetValidator(), new ServiceHost(countryService), typeof(ICountryService), logger);
			userHost = new RailwayServiceHost("http://localhost:18000/IUserService", new BasicHttpBinding(), GetValidator(), new ServiceHost(userService), typeof(IUserService), logger);
		}

		// Otvara komunikacione kanale
		private void OpenChannels()
		{
			try
			{
				logger.LogNewMessage($"Opening communication channels..", LogType.INFO);
				countryHost.OpenServiceChannel();
				placeHost.OpenServiceChannel();
				roadHost.OpenServiceChannel();
				stationHost.OpenServiceChannel();
				trackHost.OpenServiceChannel();
				userHost.OpenServiceChannel();
			}
			catch (Exception ex)
			{
				logger.LogNewMessage($"Error opening services.  {ex.Message}", LogType.ERROR);
			}

			logger.LogNewMessage($"Server is fully operational and ready for use now!", LogType.INFO);
		}
	}
}
