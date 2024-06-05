using Common.Contracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;

namespace Common.DomainModels
{
	[DataContract]
	public class Station : IInternalValidation, IDeepCloneable<Station>
	{
		private int id;
		private string name;
		private int trackNumber;

		/// <summary>
		/// Konstruktor za inicijalizaciju Station sa svim parametrima
		/// </summary>
		/// <param name="id">ID stanice</param>
		/// <param name="name">Ime stanice</param>
		/// <param name="trackNumber">Broj pruge</param>
		/// <param name="tracks">Lista pruga</param>
		/// <param name="place">Lokacija stanice</param>
		/// <param name="roads">Lista puteva</param>
		public Station(int id, string name, int trackNumber, List<Track> tracks, Place place, List<Road> roads)
		{
			this.id = id;
			this.name = name;
			this.trackNumber = trackNumber;
			this.Tracks = tracks;
			this.Place = place;
			this.Roads = roads;
		}

		/// <summary>
		/// Prazan konstruktor za inicijalizaciju Station sa podrazumevanim vrednostima
		/// </summary>
		public Station()
		{
			Id = 0;
			Name = "";
			TrackNumber = 0;
			Tracks = new List<Track>();
			Roads = new List<Road>();
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

		[DataMember]
		public int TrackNumber
		{
			get { return trackNumber; }
			set { trackNumber = value; }
		}

		[DataMember]
		public virtual Place Place { get; set; }

		[DataMember]
		public virtual IList<Track> Tracks { get; set; }

		[IgnoreDataMember]
		public virtual IList<Road> Roads { get; set; }

		/// <summary>
		/// Validacija stanice
		/// </summary>
		/// <returns>Vraća true ako su svi atributi validni, inače false</returns>
		public bool IsValid()
		{
			return id >= 0 && name.Length >= 3 && Place != null;
		}

		/// <summary>
		/// Metoda za duboko kopiranje stanice
		/// </summary>
		/// <returns>Vraća kopiju stanice</returns>
		public Station DeepCopy()
		{
			Station copy = new Station();
			copy.id = id;
			copy.name = name;
			copy.trackNumber = trackNumber;
			if (Place != null)
			{
				copy.Place = Place.DeepCopy();
			}
			if (Tracks != null)
			{
				copy.Tracks = Tracks.ToList().ConvertAll(track => track.DeepCopy());
			}
			if (Roads != null)
			{
				copy.Roads = Roads.ToList();
			}
			return copy;
		}
	}
}
