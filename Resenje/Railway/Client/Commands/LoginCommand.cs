using Client.ViewModels;
using System;
using System.Windows.Input;

namespace Client.Commands
{
	// Klasa koja implementira ICommand interfejs za izvršavanje komande prijave
	public class LoginCommand : ICommand
	{
		// Referenca na LoginViewModel
		private LoginViewModel receiver;

		// Konstruktor koji inicijalizuje LoginCommand sa LoginViewModel-om
		public LoginCommand(LoginViewModel receiver)
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
			return receiver.CanLogin();
		}

		// Metoda koja izvršava komandu prijave
		public void Execute(object parameter)
		{
			receiver.Login();
		}
	}
}
