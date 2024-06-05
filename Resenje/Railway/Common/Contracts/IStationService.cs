using Common.DomainModels;
using System.Collections.Generic;
using System.ServiceModel;

namespace Common.Contracts
{

	[ServiceContract]
	public interface IStationService //Interfejs koji implemntira klasa StationService
	{

		[OperationContract]
		/// 
		/// <param name="station"></param>
		bool AddStation(Station station);

		[OperationContract]
		/// 
		/// <param name="newData"></param>
		bool ChangeStation(Station newData);

		[OperationContract]
		/// 
		/// <param name="station"></param>
		bool DeleteStation(int stationID);

		[OperationContract]
		List<Station> GetAllStations();

		[OperationContract]
		/// 
		/// <param name="id"></param>
		Station GetStationByID(int id);
	}
}