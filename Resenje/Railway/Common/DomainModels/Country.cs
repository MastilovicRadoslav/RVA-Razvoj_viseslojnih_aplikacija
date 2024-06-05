using Common.Contracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Common.DomainModels
{

	[DataContract]
	public class Country : IInternalValidation, IDeepCloneable<Country>
	{

		private int id;
		private string name;

		/// 
		/// <param name="id"></param>
		/// <param name="name"></param>
		public Country(int id, string name)
		{
			this.id = id;
			this.name = name;
		}

		public Country()
		{
			id = 0;
			Name = "";
		}

		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[DataMember]
		public int Id
		{
			get
			{
				return id;
			}
			set
			{
				id = value;
			}
		}

		public bool IsValid()
		{

			return id >= 0 && name.Length >= 2;
		}

		public Country DeepCopy()
		{
			Country copy = new Country();
			copy.id = id;
			copy.name = name;

			return copy;
		}

		[Column(TypeName = "varchar"), MinLength(3)]
		[DataMember]
		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				name = value;
			}
		}

	}
}