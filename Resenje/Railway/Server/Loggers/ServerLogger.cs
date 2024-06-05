using Common.Contracts;

namespace Server.Loggers
{
	public class ServerLogger : ILogging  //Ova klasa implementira interfejs ILogging i koristi log4net za logovanje poruka.
	{
		private static ServerLogger instance; // Singleton instanca loggera
		private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); // log4net instanca za logovanje

		/// <summary>
		/// Metoda za dobijanje ili kreiranje instance loggera
		/// </summary>
		/// <returns>Singleton instanca ServerLogger</returns>
		public static ServerLogger GetOrCreate()
		{
			if (instance == null) instance = new ServerLogger(); // Kreira novu instancu ako ne postoji
			return instance;
		}

		/// <summary>
		/// Loguje novu poruku
		/// </summary>
		/// <param name="message">Poruka za logovanje</param>
		/// <param name="logType">Tip loga (INFO, DEBUG, ERROR, WARNING)</param>
		public void LogNewMessage(string message, LogType logType)
		{
			switch (logType)
			{
				case LogType.INFO:
					log.Info(message); // Loguje informaciju
					break;
				case LogType.DEBUG:
					log.Debug(message); // Loguje debug informaciju
					break;
				case LogType.ERROR:
					log.Error(message); // Loguje grešku
					break;
				case LogType.WARNING:
					log.Warn(message); // Loguje upozorenje
					break;
			}
		}
	}
}
