using Client.Contracts;
using Client.Models;
using System.Collections.Generic;

namespace Client.Managers
{
	// ClientEventManager upravlja događajima i obaveštava posmatrače o promenama
	public class ClientEventManager
	{
		// Singleton instanca
		private static ClientEventManager instance;

		// Lista događaja
		private List<ClientEvent> events;

		// Lista posmatrača
		private List<IClientEventNotifiable> observers;

		// Privatni konstruktor sprečava direktno kreiranje instance
		private ClientEventManager()
		{
			events = new List<ClientEvent>();
			observers = new List<IClientEventNotifiable>();
		}

		// Singleton metoda za dobijanje ili kreiranje instance ClientEventManager-a
		public static ClientEventManager GetOrCreate()
		{
			if (instance == null) instance = new ClientEventManager();
			return instance;
		}

		// Dodaje novi događaj u listu događaja i obaveštava sve posmatrače
		public void AddEvent(ClientEvent clientEvent)
		{
			events.Add(clientEvent);
			NotifyAll();
		}

		// Registruje novog posmatrača za obaveštavanje o promenama
		public void RegisterEventObserver(IClientEventNotifiable observer)
		{
			if (observers.Contains(observer)) return;
			observers.Add(observer);
		}

		// Uklanja posmatrača iz liste posmatrača
		public void UnregsiterEventObserver(IClientEventNotifiable observer)
		{
			if (!observers.Contains(observer)) return;
			observers.Remove(observer);
		}

		// Privatna metoda koja obaveštava sve posmatrače o promenama u listi događaja
		private void NotifyAll()
		{
			foreach (IClientEventNotifiable client in observers)
			{
				client.NotifyEventsUpdated(events);
			}
		}
	}
}
