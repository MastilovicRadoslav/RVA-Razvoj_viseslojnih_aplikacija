using Server.Facades;
using System;

namespace Server
{

	class Program
	{

		static void Main(string[] args)
		{
			// Kreira instancu ServerInitializer klase
			ServerInitializer initializer = new ServerInitializer();
			// Poziva metodu za pokretanje servera
			initializer.StartServer();
			// Čeka da korisnik pritisne taster pre nego što zatvori aplikaciju
			Console.ReadKey();
		}
	}
}
