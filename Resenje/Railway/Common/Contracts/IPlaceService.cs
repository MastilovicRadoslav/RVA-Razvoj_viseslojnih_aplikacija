using Common.DomainModels;
using System.Collections.Generic;
using System.ServiceModel;

namespace Common.Contracts
{
	[ServiceContract]
	public interface IPlaceService  // Interfejs koji implementira klasa PlaceService
	{
		[OperationContract]
		/// <summary>
		/// Dodaje novo mesto
		/// </summary>
		/// <param name="place">Objekat Place koji se dodaje</param>
		/// <returns>Vraća true ako je mesto uspešno dodato, inače false</returns>
		bool AddPlace(Place place);

		[OperationContract]
		/// <summary>
		/// Briše mesto na osnovu ID-a
		/// </summary>
		/// <param name="placeID">ID mesta koje se briše</param>
		/// <returns>Vraća true ako je mesto uspešno obrisano, inače false</returns>
		bool DeletePlace(int placeID);

		[OperationContract]
		/// <summary>
		/// Vraća listu svih mesta
		/// </summary>
		/// <returns>Lista mesta</returns>
		List<Place> GetAllPlaces();

		[OperationContract]
		/// <summary>
		/// Vraća mesto na osnovu ID-a
		/// </summary>
		/// <param name="id">ID mesta</param>
		/// <returns>Objekat Place ako je mesto pronađeno, inače null</returns>
		Place GetPlaceByID(int id);

		[OperationContract]
		/// <summary>
		/// Ažurira mesto sa novim podacima
		/// </summary>
		/// <param name="newData">Objekat Place sa novim podacima</param>
		/// <returns>Vraća true ako je mesto uspešno ažurirano, inače false</returns>
		bool UpdatePlace(Place newData);
	}
}
