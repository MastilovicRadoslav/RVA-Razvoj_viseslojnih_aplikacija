using Common.DomainModels;
using System.Collections.Generic;
using System.ServiceModel;

namespace Common.Contracts
{

	[ServiceContract]
	public interface IRoadService  //Interfejs koji implementira klasa IRoadService
	{

		[OperationContract]
		/// 
		/// <param name="road"></param>
		Road AddRoad(Road road);

		[OperationContract]
		/// 
		/// <param name="id"></param>
		Road GetRoadById(int id);

		[OperationContract]
		/// 
		/// <param name="road"></param>
		Road CloneRoad(Road road);

		[OperationContract]
		/// 
		/// <param name="road"></param>
		bool DeleteRoad(int roadID);


		[OperationContract]
		/// 
		/// <param name="name"></param>
		/// <param name="label"></param>
		List<Road> SearchRoads(string name, string label);

		[OperationContract]
		List<Road> GetAllRoads();

		[OperationContract]
		/// 
		/// <param name="newData"></param>
		bool UpdateRoad(Road newData);
	}
}