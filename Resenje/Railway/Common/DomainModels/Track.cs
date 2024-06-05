using Common.Contracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Common.DomainModels
{
	[DataContract]
	public class Track : IInternalValidation, IDeepCloneable<Track>
	{
		// Privatna polja za čuvanje vrednosti atributa
		private EntranceType entrance = EntranceType.LEFT;
		private int id;
		private string label;
		private string name;
		private int? stationId;

		/// <summary>
		/// Konstruktor za inicijalizaciju Track sa svim parametrima
		/// </summary>
		/// <param name="id">ID pruge</param>
		/// <param name="label">Oznaka pruge</param>
		/// <param name="name">Ime pruge</param>
		/// <param name="entrance">Tip ulaza pruge</param>
		public Track(int id, string label, string name, EntranceType entrance)
		{
			this.id = id;
			this.name = name;
			this.label = label;
			this.entrance = entrance;
		}

		/// <summary>
		/// Prazan konstruktor za inicijalizaciju Track sa podrazumevanim vrednostima
		/// </summary>
		public Track()
		{
			id = 0;
			Name = "";
			label = "";
			Entrance = EntranceType.LEFT;
		}

		[DataMember]
		public EntranceType Entrance
		{
			get { return entrance; }
			set { entrance = value; }
		}

		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[DataMember]
		public int Id
		{
			get { return id; }
			set { id = value; }
		}

		[Column(TypeName = "varchar"), StringLength(3, MinimumLength = 3)]
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
		public int? StationId
		{
			get { return stationId; }
			set { stationId = value; }
		}

		/// <summary>
		/// Validacija pruge
		/// </summary>
		/// <returns>Vraća true ako su svi atributi validni, inače false</returns>
		public bool IsValid()
		{
			return id >= 0 && name.Length >= 3 && label.Length == 3;
		}

		/// <summary>
		/// Metoda za duboko kopiranje pruge
		/// </summary>
		/// <returns>Vraća kopiju pruge</returns>
		public Track DeepCopy()
		{
			Track copy = new Track();
			copy.id = id;
			copy.name = name;
			copy.label = label;
			copy.entrance = entrance;
			return copy;
		}
	}
}
