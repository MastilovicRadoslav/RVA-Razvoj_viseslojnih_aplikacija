using Common.Contracts;

namespace Client.Contracts
{
	// Interfejs koji definiše metode za kreiranje proxy objekata za različite servise u RailwayService sistemu
	public interface IRailwayServiceProxyCreationFacade
	{
		// Metoda za kreiranje proxy objekta za IUserService
		IUserService GetUserServiceProxy(string username, string password);

		// Metoda za kreiranje proxy objekta za ICountryService
		ICountryService GetCountryServiceProxy(string username, string password);

		// Metoda za kreiranje proxy objekta za IPlaceService
		IPlaceService GetPlaceServiceProxy(string username, string password);

		// Metoda za kreiranje proxy objekta za ITrackService
		ITrackService GetTrackServiceProxy(string username, string password);

		// Metoda za kreiranje proxy objekta za IStationService
		IStationService GetStationServiceProxy(string username, string password);

		// Metoda za kreiranje proxy objekta za IRoadService
		IRoadService GetRoadServiceProxy(string username, string password);
	}
}
