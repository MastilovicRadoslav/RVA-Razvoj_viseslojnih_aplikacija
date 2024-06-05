using Common.Contracts;
using System;

namespace Client.Models
{
	// Klasa koja predstavlja događaj klijenta
	public class ClientEvent
	{
		// Konstruktor koji inicijalizuje događaj sa tipom loga, vremenom događaja i porukom
		public ClientEvent(LogType logType, DateTime eventTime, string message)
		{
			LogType = logType;
			EventTime = eventTime;
			Message = message;
		}

		// Tip loga (INFO, DEBUG, ERROR, WARNING)
		public LogType LogType { get; set; }

		// Vreme kada se događaj desio
		public DateTime EventTime { get; set; }

		// Poruka događaja
		public string Message { get; set; }
	}
}
