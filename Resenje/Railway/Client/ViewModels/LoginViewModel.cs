using Client.Commands;
using Client.Contracts;
using Common.Contracts;
using System;
using System.ServiceModel.Security;
using System.Windows;
using System.Windows.Input;

namespace Client.ViewModels
{
	// ViewModel za Login prozor, koristi MVVM obrazac
	public class LoginViewModel
	{
		// Konstruktor za inicijalizaciju LoginViewModel sa prozorom, logger-om i konektorom
		public LoginViewModel(Window window, ILogging logger, IRailwayServiceProxyCreationFacade connector)
		{
			Window = window;
			Logger = logger;
			LoginCommand = new LoginCommand(this);
			QuitCommand = new QuitLoginWindowCommand(this);
			Connector = connector;
		}

		// Svojstvo za prozor
		public Window Window { get; set; }

		// Svojstvo za korisničko ime
		public string Username { get; set; }

		// Svojstvo za lozinku
		public string Password { get; set; }

		// Svojstvo za korisnički servis
		public IUserService UserService { get; set; }

		// Svojstvo za logger
		public ILogging Logger { get; set; }

		// Svojstvo za konektor servisa
		public IRailwayServiceProxyCreationFacade Connector { get; set; }

		// Komanda za prijavu
		public ICommand LoginCommand { get; private set; }

		// Komanda za izlaz
		public ICommand QuitCommand { get; private set; }

		// Metoda koja proverava da li se može izvršiti prijava
		public bool CanLogin()
		{
			return Username != "" && Password != "";
		}

		// Metoda za izvršavanje prijave
		public bool Login()
		{
			try
			{
				// Logovanje pokušaja prijave
				Logger.LogNewMessage($"Trying to log in with specified client credentials..", LogType.INFO);
				Window.Cursor = Cursors.Wait;

				// Kreiranje proxy objekta za korisnički servis
				UserService = Connector.GetUserServiceProxy(Username, Password);

				// Otvaranje glavnog prozora sa podacima o korisniku
				new MainWindow(UserService.FindUserByUserName(Username)).Show();

				// Zatvaranje prozora za prijavu
				Window.Close();

				return true;
			}
			catch (MessageSecurityException ex)
			{
				// Logovanje greške prilikom prijave zbog nevalidnih kredencijala
				Logger.LogNewMessage($"Credentials invalid, access denied by host.", LogType.WARNING);
				MessageBox.Show(Window, "Entered credentials are invalid. Access denied.", "Wrong credentials");
				Window.Cursor = Cursors.Arrow;
				return false;
			}
			catch (Exception exc)
			{
				// Logovanje nepoznate greške prilikom prijave
				Logger.LogNewMessage($"Unknown error happened while trying to log in {exc.Message}", LogType.ERROR);
				MessageBox.Show(Window, "Unknown error occured while processing your request. Please try again.", "Error.");
				Window.Cursor = Cursors.Arrow;
				return false;
			}
		}
	}
}
