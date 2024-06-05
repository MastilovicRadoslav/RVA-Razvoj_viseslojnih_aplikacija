using Common.Contracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Common.DomainModels
{
	[DataContract]
	public class User : IInternalValidation, IDeepCloneable<User>
	{
		private int id;
		private bool isAdmin;
		private string lastName;
		private string name;
		private string username;
		private string password;

		/// <summary>
		/// Konstruktor za inicijalizaciju korisnika sa svim parametrima
		/// </summary>
		/// <param name="id">ID korisnika</param>
		/// <param name="name">Ime korisnika</param>
		/// <param name="username">Korisničko ime</param>
		/// <param name="lastName">Prezime korisnika</param>
		/// <param name="password">Lozinka korisnika</param>
		/// <param name="isAdmin">Da li je korisnik administrator</param>
		public User(int id, string name, string username, string lastName, string password, bool isAdmin)
		{
			this.id = id;
			this.name = name;
			this.username = username;
			this.lastName = lastName;
			this.password = password;
			this.isAdmin = isAdmin;
		}

		/// <summary>
		/// Prazan konstruktor za inicijalizaciju korisnika sa podrazumevanim vrednostima
		/// </summary>
		public User()
		{
			ID = 0;
			Name = "";
			LastName = "";
			UserName = "";
			Password = "";
			IsAdmin = false;
		}

		/// <summary>
		/// Provera lozinke
		/// </summary>
		/// <param name="password">Lozinka za proveru</param>
		/// <returns>Vraća true ako lozinka odgovara, inače false</returns>
		public bool CheckPassword(string password)
		{
			return password == this.password;
		}

		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[DataMember]
		public int ID
		{
			get { return id; }
			set { id = value; }
		}

		[DataMember]
		public bool IsAdmin
		{
			get { return isAdmin; }
			set { isAdmin = value; }
		}

		/// <summary>
		/// Validacija korisnika
		/// </summary>
		/// <returns>Vraća true ako su svi atributi validni, inače false</returns>
		public bool IsValid()
		{
			return id >= 0 && name.Length >= 3 && username.Length >= 3 && lastName.Length >= 3 && password.Length >= 3;
		}

		/// <summary>
		/// Metoda za duboko kopiranje korisnika
		/// </summary>
		/// <returns>Vraća kopiju korisnika</returns>
		public User DeepCopy()
		{
			return new User(id, name, username, lastName, password, isAdmin);
		}

		[Column(TypeName = "varchar"), MinLength(3)]
		[DataMember]
		public string LastName
		{
			get { return lastName; }
			set { lastName = value; }
		}

		[Column(TypeName = "varchar"), MinLength(3)]
		[DataMember]
		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		[Column(TypeName = "varchar"), MinLength(3)]
		[DataMember]
		public string Password
		{
			get { return password; }
			set { password = value; }
		}

		[Column(TypeName = "varchar"), MinLength(3)]
		[DataMember]
		public string UserName
		{
			get { return username; }
			set { username = value; }
		}
	}
}
