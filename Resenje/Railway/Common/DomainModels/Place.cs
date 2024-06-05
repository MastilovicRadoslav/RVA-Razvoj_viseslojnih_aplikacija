using Common.Contracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Common.DomainModels
{
	[DataContract]
	public class Place : IInternalValidation, IDeepCloneable<Place>
	{
		private Country country;
		private int id;
		private string name;

		/// <summary>
		/// Konstruktor za inicijalizaciju Place sa svim parametrima
		/// </summary>
		/// <param name="id">ID mesta</param>
		/// <param name="name">Ime mesta</param>
		/// <param name="country">Država mesta</param>
		public Place(int id, string name, Country country)
		{
			this.id = id;
			this.name = name;
			this.country = country;
		}

		/// <summary>
		/// Prazan konstruktor za inicijalizaciju Place sa podrazumevanim vrednostima
		/// </summary>
		public Place()
		{
			id = 0;
			Name = "";
		}

		[DataMember]
		public Country Country
		{
			get { return country; }
			set { country = value; }
		}

		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[DataMember]
		public int Id
		{
			get { return id; }
			set { id = value; }
		}

		[Column(TypeName = "varchar"), MinLength(3)]
		[DataMember]
		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		/// <summary>
		/// Validacija mesta
		/// </summary>
		/// <returns>Vraća true ako su svi atributi validni, inače false</returns>
		public bool IsValid()
		{
			return id >= 0 && name.Length >= 3 && country != null;
		}

		/// <summary>
		/// Metoda za duboko kopiranje mesta
		/// </summary>
		/// <returns>Vraća kopiju mesta</returns>
		public Place DeepCopy()
		{
			Place copy = new Place();
			copy.id = id;
			copy.name = name;
			if (country != null)
			{
				copy.country = country.DeepCopy();
			}
			return copy;
		}
	}
}
