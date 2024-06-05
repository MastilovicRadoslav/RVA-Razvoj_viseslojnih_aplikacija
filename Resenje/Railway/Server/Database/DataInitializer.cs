using Common.Contracts;
using Common.DomainModels;
using Server.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Server.Database
{
	public class DataInitializer  //Ova klasa sadrži logiku za inicijalizaciju podataka u bazi podataka.
	{
		private ILogging logger; // Logger za beleženje događaja
		private IDatabaseContextFactory factory; // Fabrika za kreiranje konteksta baze podataka

		/// <summary>
		/// Konstruktor klase DataInitializer
		/// </summary>
		/// <param name="logger">Instanca loggera</param>
		/// <param name="factory">Fabrika za kreiranje konteksta baze podataka</param>
		public DataInitializer(ILogging logger, IDatabaseContextFactory factory)
		{
			this.logger = logger;
			this.factory = factory;
		}

		/// <summary>
		/// Inicijalizuje podatke u bazi podataka
		/// </summary>
		public void InitializeData()
		{
			InitializeCountries();
			InitializePlaces();
			InitializeTracks();
			InitializeStations();
			InitializeRoads();
			InitializeUsers();
		}

		/// <summary>
		/// Inicijalizuje države u bazi podataka
		/// </summary>
		private void InitializeCountries()
		{
			logger.LogNewMessage($"Initializing countries..", LogType.INFO);

			try
			{
				var fileContent = File.ReadAllLines("countries.txt"); // Čita sve linije iz fajla
				var worldCountries = new List<string>(fileContent); // Kreira listu država iz fajla
				logger.LogNewMessage($"Fetching countries from repository", LogType.DEBUG);
				using (var dbContext = factory.GetContext())
				{
					var countries = dbContext.Countries.ToList(); // Dohvata sve države iz baze podataka

					List<Country> countriesToAdd = new List<Country>();
					foreach (string countryName in worldCountries)
					{
						if (countries.Where(x => x.Name == countryName).Count() == 0)
							countriesToAdd.Add(new Country(0, countryName)); // Dodaje novu državu ako ne postoji u bazi
					}

					if (countriesToAdd.Count > 0) dbContext.Countries.AddRange(countriesToAdd); // Dodaje nove države u bazu podataka
					dbContext.SaveChanges(); // Čuva promene u bazi podataka

					logger.LogNewMessage($"Countries intialized successfully.", LogType.INFO);
				}
			}
			catch (Exception ex)
			{
				logger.LogNewMessage($"Error occured trying to intialize countries. Message {ex.Message}", LogType.ERROR);
			}
		}

		/// <summary>
		/// Inicijalizuje mesta u bazi podataka
		/// </summary>
		private void InitializePlaces()
		{
			logger.LogNewMessage($"Initializing places..", LogType.INFO);

			try
			{
				using (var dbContext = factory.GetContext())
				{
					List<Place> places = new List<Place>
					{
						new Place(0, "Belgrade", dbContext.Countries.First(t => t.Name == "Serbia")),
						new Place(0, "Novi Sad", dbContext.Countries.First(t => t.Name == "Serbia")),
						new Place(0, "Zagreb", dbContext.Countries.First(t => t.Name == "Croatia")),
						new Place(0, "Xinjiang", dbContext.Countries.First(t => t.Name == "China"))
					};

					logger.LogNewMessage($"Fetching places from repository", LogType.DEBUG);

					var existingPlaces = dbContext.Places.ToList(); // Dohvata sve postojeće mesta iz baze
					List<Place> placesToAdd = new List<Place>();

					foreach (Place p in places)
					{
						if (existingPlaces.Where(x => x.Name == p.Name).Count() == 0)
							placesToAdd.Add(p); // Dodaje novo mesto ako ne postoji u bazi
					}

					dbContext.Places.AddRange(placesToAdd); // Dodaje nova mesta u bazu podataka
					dbContext.SaveChanges(); // Čuva promene u bazi podataka
					logger.LogNewMessage($"Places intialized successfully.", LogType.INFO);
				}
			}
			catch (Exception ex)
			{
				logger.LogNewMessage($"Error occured trying to intialize places. Message {ex.Message}", LogType.ERROR);
			}
		}

		/// <summary>
		/// Inicijalizuje pruge u bazi podataka
		/// </summary>
		private void InitializeTracks()
		{
			logger.LogNewMessage($"Initializing tracks..", LogType.INFO);

			List<Track> tracks = new List<Track>
			{
				new Track(0, "123", "Mirogojnica", EntranceType.LEFT),
				new Track(0, "2bf", "Retardevica", EntranceType.RIGHT),
				new Track(0, "cgt", "Kuharevica", EntranceType.LEFT),
				new Track(0, "12k", "Muqin", EntranceType.LEFT),
				new Track(0, "12g", "Tian", EntranceType.LEFT),
				new Track(0, "3f5", "Franjovnica", EntranceType.RIGHT),
				new Track(0, "dsf", "disigari", EntranceType.LEFT)
			};

			try
			{
				logger.LogNewMessage($"Fetching tracks from repository", LogType.DEBUG);
				using (var dbContext = factory.GetContext())
				{
					var existingTracks = dbContext.Track.ToList(); // Dohvata sve postojeće pruge iz baze
					List<Track> tracksToAdd = new List<Track>();

					foreach (Track t in tracks)
					{
						if (existingTracks.Where(x => x.Label == t.Label).Count() == 0)
							tracksToAdd.Add(t); // Dodaje novu prugu ako ne postoji u bazi
					}

					dbContext.Track.AddRange(tracksToAdd); // Dodaje nove pruge u bazu podataka
					dbContext.SaveChanges(); // Čuva promene u bazi podataka
					logger.LogNewMessage($"Tracks intialized successfully.", LogType.INFO);
				}
			}
			catch (Exception ex)
			{
				logger.LogNewMessage($"Error occured trying to intialize tracks. Message {ex.Message}", LogType.ERROR);
			}
		}

		/// <summary>
		/// Inicijalizuje stanice u bazi podataka
		/// </summary>
		private void InitializeStations()
		{
			logger.LogNewMessage($"Initializing stations..", LogType.INFO);

			try
			{
				logger.LogNewMessage($"Fetching stations from repository", LogType.DEBUG);

				using (var dbContext = factory.GetContext())
				{
					var existingStations = dbContext.Stations.ToList(); // Dohvata sve postojeće stanice iz baze
					var existingTracks = dbContext.Track.ToList(); // Dohvata sve postojeće pruge iz baze
					List<Station> stationsToAdd = new List<Station>();
					List<Station> stations = new List<Station>();

					// Kreiranje novih stanica i pridruživanje pruga
					List<Track> prokopTracks = new List<Track>
					{
						existingTracks.Where(x => x.Label == "cgt").First()
					};
					var prokopPlace = dbContext.Places.SingleOrDefault(x => x.Name == "Belgrade");
					stations.Add(new Station(0, "Prokop", 1, prokopTracks, prokopPlace, new List<Road>()));

					List<Track> glavniKolodvorTracks = new List<Track>
					{
						existingTracks.Where(x => x.Label == "2bf").First(),
						existingTracks.Where(x => x.Label == "123").First(),
						existingTracks.Where(x => x.Label == "3f5").First()
					};
					var glavniKolodvorPlace = dbContext.Places.SingleOrDefault(x => x.Name == "Zagreb");
					stations.Add(new Station(0, "Glavni Kolodvor", 3, glavniKolodvorTracks, glavniKolodvorPlace, new List<Road>()));

					List<Track> xinjiangTracks = new List<Track>
					{
						existingTracks.Where(x => x.Label == "12k").First(),
						existingTracks.Where(x => x.Label == "12g").First()
					};
					var houchezhanPlace = dbContext.Places.SingleOrDefault(x => x.Name == "Xinjiang");
					stations.Add(new Station(0, "Huochezhan", 2, xinjiangTracks, houchezhanPlace, new List<Road>()));

					List<Track> noviSadTracks = new List<Track>
					{
						existingTracks.Where(x => x.Label == "dsf").First()
					};
					var nsPlace = dbContext.Places.SingleOrDefault(x => x.Name == "Xinjiang");
					stations.Add(new Station(0, "Novosadska zeleznicka stanica", 2, noviSadTracks, nsPlace, new List<Road>()));

					foreach (Station s in stations)
					{
						if (existingStations.Where(x => x.Name == s.Name).Count() == 0)
							stationsToAdd.Add(s); // Dodaje novu stanicu ako ne postoji u bazi
					}

					dbContext.Stations.AddRange(stationsToAdd); // Dodaje nove stanice u bazu podataka
					dbContext.SaveChanges(); // Čuva promene u bazi podataka
					logger.LogNewMessage($"Stations intialized successfully.", LogType.INFO);
				}
			}
			catch (Exception ex)
			{
				logger.LogNewMessage($"Error occured trying to intialize stations. Message {ex.Message}", LogType.ERROR);
			}
		}

		/// <summary>
		/// Inicijalizuje puteve u bazi podataka
		/// </summary>
		private void InitializeRoads()
		{
			logger.LogNewMessage($"Initializing roads..", LogType.INFO);

			try
			{
				using (var dbContext = factory.GetContext())
				{
					logger.LogNewMessage($"Fetching roads from repository", LogType.DEBUG);

					var existingRoads = dbContext.Roads.ToList(); // Dohvata sve postojeće puteve iz baze
					var existingStations = dbContext.Stations.ToList(); // Dohvata sve postojeće stanice iz baze
					List<Road> roadsToAdd = new List<Road>();
					List<Road> roads = new List<Road>();

					// Kreiranje novih puteva i pridruživanje stanica
					List<Station> balkanStations = new List<Station>
					{
						existingStations.Where(x => x.Name == "Prokop").First(),
						existingStations.Where(x => x.Name == "Glavni Kolodvor").First(),
						existingStations.Where(x => x.Name == "Novosadska zeleznicka stanica").First()
					};
					roads.Add(new Road(0, "Balkan road", "E-69", balkanStations));

					List<Station> chineseStations = new List<Station>
					{
						existingStations.Where(x => x.Name == "Huochezhan").First()
					};
					roads.Add(new Road(0, "West China railway", "CH-X-189", chineseStations));
					roads.Add(new Road(0, "North Korea rail", "Kim-Jong-None", new List<Station>()));

					foreach (Road r in roads)
					{
						if (existingRoads.Where(x => x.Name == r.Name).Count() == 0)
							roadsToAdd.Add(r); // Dodaje novi put ako ne postoji u bazi
					}

					dbContext.Roads.AddRange(roadsToAdd); // Dodaje nove puteve u bazu podataka
					dbContext.SaveChanges(); // Čuva promene u bazi podataka
					logger.LogNewMessage($"Roads intialized successfully.", LogType.INFO);
				}
			}
			catch (Exception ex)
			{
				logger.LogNewMessage($"Error occured trying to intialize roads. Message {ex.Message}", LogType.ERROR);
			}
		}

		/// <summary>
		/// Inicijalizuje korisnike u bazi podataka
		/// </summary>
		private void InitializeUsers()
		{
			logger.LogNewMessage($"Initializing users..", LogType.INFO);

			List<User> users = new List<User>
			{
				new User(0, "Admin", "admin", "Admin", "admin", true),
				new User(0, "Dajana", "dajana", "Radovic", "dajana", true),
				new User(0, "Kristian", "kristian", "Tojzan", "kristian", true),
				new User(0, "Vanja", "vanja", "Mihajlovic", "vanja", false),
				new User(0, "Radoslav", "radoslav", "Mastilovic", "radoslav", false)
			};

			try
			{
				using (var dbContext = factory.GetContext())
				{
					logger.LogNewMessage($"Fetching users from repository", LogType.DEBUG);

					var existingUsers = dbContext.Users.ToList(); // Dohvata sve postojeće korisnike iz baze
					List<User> usersToAdd = new List<User>();

					foreach (User u in users)
					{
						if (existingUsers.Where(x => x.UserName == u.UserName).Count() == 0)
							usersToAdd.Add(u); // Dodaje novog korisnika ako ne postoji u bazi
					}

					dbContext.Users.AddRange(usersToAdd); // Dodaje nove korisnike u bazu podataka
					dbContext.SaveChanges(); // Čuva promene u bazi podataka
					logger.LogNewMessage($"Users intialized successfully.", LogType.INFO);
				}
			}
			catch (Exception ex)
			{
				logger.LogNewMessage($"Error occured trying to intialize users. Message {ex.Message}", LogType.ERROR);
			}
		}
	}
}
