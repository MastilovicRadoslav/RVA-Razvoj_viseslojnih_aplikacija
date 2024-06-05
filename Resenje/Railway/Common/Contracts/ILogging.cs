namespace Common.Contracts
{

	public interface ILogging    //Ovaj interfejs defini�e metodu za logovanje poruka.
	{

		/// 
		/// <param name="message"></param>
		/// <param name="logType"></param>
		void LogNewMessage(string message, LogType logType);
	}
}