using Client.ViewModels;
using System;
using System.Windows.Input;

namespace Client.Commands
{
	// Klasa koja implementira ICommand interfejs za odjavu korisnika
	public class LogoutCommand : ICommand
	{
		// Privatno polje za referencu na MainViewModel
		private MainViewModel receiver;

		// Konstruktor koji inicijalizuje LogoutCommand sa MainViewModel-om
		public LogoutCommand(MainViewModel receiver)
		{
			this.receiver = receiver;
		}

		// Događaj koji se poziva kada se može promeniti stanje komande
		public event EventHandler CanExecuteChanged
		{
			add
			{
				CommandManager.RequerySuggested += value;
			}
			remove
			{
				CommandManager.RequerySuggested -= value;
			}
		}

		// Metoda koja proverava da li se komanda može izvršiti
		public bool CanExecute(object parameter)
		{
			// Komanda može biti izvršena samo ako prozor nije null
			return receiver.Window != null;
		}

		// Metoda koja izvršava komandu za odjavu korisnika
		public void Execute(object parameter)
		{
			// Poziva metodu Logout na receiver-u (MainViewModel)
			receiver.Logout();
		}
	}
}
