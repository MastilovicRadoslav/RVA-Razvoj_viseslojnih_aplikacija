using Server.Contracts;
using Server.Database;

namespace Server.Factories
{
	public class DatabaseContextFactory : IDatabaseContextFactory    //Ova klasa implementira interfejs IDatabaseContextFactory i pruža metodu za kreiranje, novih instanci konteksta baze podataka.
	{
		/// <summary>
		/// Vraća novu instancu konteksta baze podataka
		/// </summary>
		/// <returns>Instanca RailwayContext</returns>
		public RailwayContext GetContext()
		{
			return new RailwayContext();
		}
	}
}
