using Client.Contracts;
using Common.Contracts;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Client.Connectors
{
	// RailwayServiceConnector implementira IRailwayServiceProxyCreationFacade interfejs
	public class RailwayServiceConnector : IRailwayServiceProxyCreationFacade
	{
		// URL endpointi za različite servise
		private string userEndpoint = "http://localhost:18000/IUserService";
		private string roadEndpoint = "http://localhost:18000/IRoadService";
		private string stationEndpoint = "http://localhost:18000/IStationService";
		private string trackEndpoint = "http://localhost:18000/ITrackService";
		private string placeEndpoint = "http://localhost:18000/IPlaceService";
		private string countryEndpoint = "http://localhost:18000/ICountryService";
		private Binding binding;

		// Konstruktor koji inicijalizuje binding
		public RailwayServiceConnector()
		{
			binding = GetRailwayServiceBinding();
		}

		// Property za pristup i postavljanje endpointova
		public string UserEndpoint { get => userEndpoint; set => userEndpoint = value; }
		public string RoadEndpoint { get => roadEndpoint; set => roadEndpoint = value; }
		public string StationEndpoint { get => stationEndpoint; set => stationEndpoint = value; }
		public string TrackEndpoint { get => trackEndpoint; set => trackEndpoint = value; }
		public string PlaceEndpoint { get => placeEndpoint; set => placeEndpoint = value; }
		public string CountryEndpoint { get => countryEndpoint; set => countryEndpoint = value; }

		// Metoda za kreiranje proxy objekta za IUserService
		public IUserService GetUserServiceProxy(string username, string password)
		{
			ChannelFactory<IUserService> channelFactory = new ChannelFactory<IUserService>(binding, userEndpoint);
			channelFactory.Credentials.UserName.UserName = username;
			channelFactory.Credentials.UserName.Password = password;
			return channelFactory.CreateChannel();
		}

		// Metoda za kreiranje proxy objekta za IRoadService
		public IRoadService GetRoadServiceProxy(string username, string password)
		{
			ChannelFactory<IRoadService> channelFactory = new ChannelFactory<IRoadService>(binding, roadEndpoint);
			channelFactory.Credentials.UserName.UserName = username;
			channelFactory.Credentials.UserName.Password = password;
			return channelFactory.CreateChannel();
		}

		// Metoda za kreiranje proxy objekta za IStationService
		public IStationService GetStationServiceProxy(string username, string password)
		{
			ChannelFactory<IStationService> channelFactory = new ChannelFactory<IStationService>(binding, stationEndpoint);
			channelFactory.Credentials.UserName.UserName = username;
			channelFactory.Credentials.UserName.Password = password;
			return channelFactory.CreateChannel();
		}

		// Metoda za kreiranje proxy objekta za ITrackService
		public ITrackService GetTrackServiceProxy(string username, string password)
		{
			ChannelFactory<ITrackService> channelFactory = new ChannelFactory<ITrackService>(binding, trackEndpoint);
			channelFactory.Credentials.UserName.UserName = username;
			channelFactory.Credentials.UserName.Password = password;
			return channelFactory.CreateChannel();
		}

		// Metoda za kreiranje proxy objekta za IPlaceService
		public IPlaceService GetPlaceServiceProxy(string username, string password)
		{
			ChannelFactory<IPlaceService> channelFactory = new ChannelFactory<IPlaceService>(binding, placeEndpoint);
			channelFactory.Credentials.UserName.UserName = username;
			channelFactory.Credentials.UserName.Password = password;
			return channelFactory.CreateChannel();
		}

		// Metoda za kreiranje proxy objekta za ICountryService
		public ICountryService GetCountryServiceProxy(string username, string password)
		{
			ChannelFactory<ICountryService> channelFactory = new ChannelFactory<ICountryService>(binding, countryEndpoint);
			channelFactory.Credentials.UserName.UserName = username;
			channelFactory.Credentials.UserName.Password = password;
			return channelFactory.CreateChannel();
		}

		// Metoda za dobijanje binding konfiguracije za RailwayService
		private Binding GetRailwayServiceBinding()
		{
			BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
			basicHttpBinding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
			basicHttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;

			return basicHttpBinding;
		}
	}
}
