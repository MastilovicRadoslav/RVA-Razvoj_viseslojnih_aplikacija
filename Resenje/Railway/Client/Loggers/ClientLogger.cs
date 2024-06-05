using Client.Managers;
using Client.Models;
using Common.Contracts;
using System;

namespace Client.Loggers
{
	// ClientLogger implementira ILogging interfejs za logovanje poruka
	public class ClientLogger : ILogging
	{
		private static ClientLogger instance;
		private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		private static ClientEventManager eventManager = ClientEventManager.GetOrCreate();

		// Singleton metoda za dobijanje ili kreiranje instance ClientLogger-a
		public static ClientLogger GetOrCreate()
		{
			if (instance == null) instance = new ClientLogger();

			return instance;
		}

		// Metoda za logovanje nove poruke
		// <param name="message">Poruka koja se loguje</param>
		// <param name="logType">Tip log poruke (INFO, DEBUG, ERROR, WARNING)</param>
		public void LogNewMessage(string message, LogType logType)
		{
			ClientEvent clientEvent = new ClientEvent(logType, DateTime.Now, message);
			switch (logType)
			{
				case LogType.INFO:
					log.Info(message);
					break;
				case LogType.DEBUG:
					log.Debug(message);
					break;
				case LogType.ERROR:
					log.Error(message);
					break;
				case LogType.WARNING:
					log.Warn(message);
					break;
			}

			// Dodavanje događaja u event manager
			eventManager.AddEvent(clientEvent);
		}
	}
}
