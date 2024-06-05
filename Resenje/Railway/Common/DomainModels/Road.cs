using Common.Contracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;

namespace Common.DomainModels
{
	[DataContract]
	public class Road : IInternalValidation, IDeepCloneable<Road>
	{
		private int id;
		private string label;
		private string name;

		/// <summary>
		/// Konstruktor za inicijalizaciju Road sa svim parametrima
		/// </summary>
		/// <param name="id">ID puta</param>
		/// <param name="name">Ime puta</param>
		/// <param name="label">Oznaka puta</param>
		/// <param name="stations">Lista stanica na putu</param>
		public Road(int id, string name, string label, List<Station> stations)
		{
			this.id = id;
			this.name = name;
			this.label = label;
			this.Stations = stations;
		}

		/// <summary>
		/// Prazan konstruktor za inicijalizaciju Road sa podrazumevanim vrednostima
		/// </summary>
		public Road()
		{
			id = 0;
			name = "";
			label = "";
			Stations = new List<Station>();
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
		public string Label
		{
			get { return label; }
			set { label = value; }
		}

		[Column(TypeName = "varchar"), MinLength(3)]
		[DataMember]
		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		[DataMember]
		public virtual IList<Station> Stations { get; set; }

		/// <summary>
		/// Validacija puta
		/// </summary>
		/// <returns>Vraća true ako su svi atributi validni, inače false</returns>
		public bool IsValid()
		{
			return id >= 0 && name.Length >= 3 && label.Length >= 2;
		}

		/// <summary>
		/// Metoda za duboko kopiranje puta
		/// </summary>
		/// <returns>Vraća kopiju puta</returns>
		public Road DeepCopy()
		{
			Road copy = new Road();
			copy.Id = Id;
			copy.Name = Name;
			copy.Label = Label;
			if (Stations != null)
			{
				copy.Stations = Stations.ToList().ConvertAll(station => station.DeepCopy());
			}
			return copy;
		}
	}
}
