using Common.DomainModels;
using System.Collections.Generic;
using System.ServiceModel;

namespace Common.Contracts
{
	[ServiceContract]
	public interface ICountryService   // Interfejs koji implementira klasa CountryService
	{
		[OperationContract]
		/// <summary>
		/// Vraća državu na osnovu ID-a
		/// </summary>
		/// <param name="id">ID države</param>
		/// <returns>Objekat Country ako je država pronađena, inače null</returns>
		Country GeCountryByID(int id);

		[OperationContract]
		/// <summary>
		/// Vraća državu na osnovu imena
		/// </summary>
		/// <param name="name">Ime države</param>
		/// <returns>Objekat Country ako je država pronađena, inače null</returns>
		Country GetCountryByName(string name);

		[OperationContract]
		/// <summary>
		/// Vraća listu svih država
		/// </summary>
		/// <returns>Lista država</returns>
		List<Country> GetAll();
	}
}
