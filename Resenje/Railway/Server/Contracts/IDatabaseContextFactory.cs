using Server.Database; // Uvozi kontekst baze podataka

namespace Server.Contracts
{
	/// <summary>
	/// Interfejs za fabriku koja kreira instance konteksta baze podataka
	/// </summary>
	public interface IDatabaseContextFactory //Ovaj interfejs definiše metodu za kreiranje novih instanci konteksta baze podataka.
	{
		/// <summary>
		/// Vraća novu instancu konteksta baze podataka
		/// </summary>
		/// <returns>Instanca RailwayContext</returns>
		RailwayContext GetContext();
	}
}
