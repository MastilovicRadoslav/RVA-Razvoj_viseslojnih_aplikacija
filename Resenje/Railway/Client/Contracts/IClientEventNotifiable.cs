using Client.Models;
using System.Collections.Generic;

namespace Client.Contracts
{
	// Interfejs koji definiše metodu za obaveštavanje o ažuriranim događajima
	public interface IClientEventNotifiable
	{
		// Metoda koja obaveštava posmatrača o ažuriranim događajima
		// <param name="events">Lista ažuriranih događaja</param>
		void NotifyEventsUpdated(List<ClientEvent> events);
	}
}
