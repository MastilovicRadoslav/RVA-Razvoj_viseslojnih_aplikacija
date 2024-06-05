using Common.DomainModels;
using System.Collections.Generic;
using System.ServiceModel;

namespace Common.Contracts
{
	[ServiceContract]
	public interface IUserService  // Interfejs za UserService
	{
		[OperationContract]
		/// <summary>
		/// Dodaje novog korisnika
		/// </summary>
		/// <param name="user">Korisnik koji se dodaje</param>
		/// <returns>Vraća true ako je korisnik uspešno dodat, inače false</returns>
		bool AddUser(User user);

		[OperationContract]
		/// <summary>
		/// Vraća listu svih korisnika
		/// </summary>
		/// <returns>Lista korisnika</returns>
		List<User> GetAllUsers();

		[OperationContract]
		/// <summary>
		/// Pronalazi korisnika prema korisničkom imenu
		/// </summary>
		/// <param name="name">Korisničko ime</param>
		/// <returns>Vraća korisnika ako je pronađen, inače null</returns>
		User FindUserByUserName(string name);

		[OperationContract]
		/// <summary>
		/// Vraća podatke o korisniku na osnovu ID-a
		/// </summary>
		/// <param name="id">ID korisnika</param>
		/// <returns>Vraća korisnika ako je pronađen, inače null</returns>
		User GetUserData(int id);

		[OperationContract]
		/// <summary>
		/// Ažurira ime i prezime korisnika
		/// </summary>
		/// <param name="id">ID korisnika</param>
		/// <param name="newName">Novo ime</param>
		/// <param name="newLastName">Novo prezime</param>
		/// <returns>Vraća true ako je korisnik uspešno ažuriran, inače false</returns>
		bool UpdateUser(int id, string newName, string newLastName);
	}
}
