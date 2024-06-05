using Common.DomainModels;
using System.Data.Entity;

namespace Server.Database
{
	public class RailwayContext : DbContext  //Ova klasa nasleđuje DbContext i predstavlja kontekst baze podataka za aplikaciju.
	{
		private DbSet<Country> countries; // Set za entitet Country
		private DbSet<Place> places; // Set za entitet Place
		private DbSet<Road> roads; // Set za entitet Road
		private DbSet<Station> stations; // Set za entitet Station
		private DbSet<Track> tracks; // Set za entitet Track
		private DbSet<User> users; // Set za entitet User

		/// <summary>
		/// Konstruktor koji inicijalizuje bazu podataka sa imenom "Database"
		/// </summary>
		public RailwayContext() : base("Database")
		{
			this.Configuration.LazyLoadingEnabled = true; // Omogućava lenjo učitavanje
			this.Configuration.ProxyCreationEnabled = false; // Onemogućava kreiranje proxy objekata, potrebno za WCF serializaciju virtualnih svojstava
		}

		// Svojstva za pristup setovima entiteta
		public DbSet<Country> Countries
		{
			get { return countries; }
			set { countries = value; }
		}

		public DbSet<Place> Places
		{
			get { return places; }
			set { places = value; }
		}

		public DbSet<Road> Roads
		{
			get { return roads; }
			set { roads = value; }
		}

		public DbSet<Station> Stations
		{
			get { return stations; }
			set { stations = value; }
		}

		public DbSet<Track> Track
		{
			get { return tracks; }
			set { tracks = value; }
		}

		public DbSet<User> Users
		{
			get { return users; }
			set { users = value; }
		}

		/// <summary>
		/// Konfiguracija modela baze podataka
		/// </summary>
		/// <param name="modelBuilder">Instanca DbModelBuilder</param>
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			// Postavljanje jedinstvenih indeksa za entitete
			modelBuilder.Entity<User>().HasIndex(u => u.UserName).IsUnique(); // Jedinstveno korisničko ime za entitet User
			modelBuilder.Entity<Country>().HasIndex(c => c.Name).IsUnique(); // Jedinstveno ime za entitet Country
			modelBuilder.Entity<Station>().HasIndex(s => s.Name).IsUnique(); // Jedinstveno ime za entitet Station
			modelBuilder.Entity<Track>().HasIndex(s => s.Label).IsUnique(); // Jedinstvena oznaka za entitet Track

			// Konfiguracija odnosa između entiteta
			modelBuilder.Entity<Place>().HasRequired(t => t.Country).WithMany().WillCascadeOnDelete(true); // Entitet Place zahteva entitet Country, brisanje u kaskadi
			modelBuilder.Entity<Station>().HasMany(t => t.Tracks).WithOptional(); // Entitet Station može imati više entiteta Track
			modelBuilder.Entity<Station>().HasRequired(t => t.Place).WithMany(); // Entitet Station zahteva entitet Place
			modelBuilder.Entity<Station>().HasMany(t => t.Roads).WithMany(t => t.Stations).Map(t =>
			{
				t.MapLeftKey("StationID");
				t.MapRightKey("RoadID");
				t.ToTable("RoadStations");
			}); // Mnogostruki odnos između entiteta Station i Road
		}
	}
}
