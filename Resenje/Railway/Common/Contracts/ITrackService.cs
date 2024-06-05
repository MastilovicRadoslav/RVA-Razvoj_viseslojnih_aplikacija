using Common.DomainModels;
using System.Collections.Generic;
using System.ServiceModel;

namespace Common.Contracts
{

	[ServiceContract]
	public interface ITrackService  //Interfejs koji implementira klasa TrackService
	{

		[OperationContract]
		/// 
		/// <param name="track"></param>
		bool AddTrack(Track track);

		[OperationContract]
		/// 
		/// <param name="track"></param>
		bool DeleteTrack(int trackID);

		[OperationContract]
		List<Track> GetAllTracks();

		[OperationContract]
		List<Track> GetUnattachedTracks();

		[OperationContract]
		/// 
		/// <param name="id"></param>
		Track GetTrackByID(int id);

		[OperationContract]
		/// 
		/// <param name="newData"></param>
		bool UpdateTrack(Track newData);
	}
}