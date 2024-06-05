using Client.ViewModels;
using System;
using System.Windows.Input;

namespace Client.Commands
{
	// Klasa koja implementira ICommand interfejs za zatvaranje prozora za prijavu
	public class QuitLoginWindowCommand : ICommand
	{
		// Referenca na LoginViewModel
		private LoginViewModel receiver;

		// Konstruktor koji inicijalizuje QuitLoginWindowCommand sa LoginViewModel-om
		public QuitLoginWindowCommand(LoginViewModel receiver)
		{
			this.receiver = receiver;
		}

		// Događaj koji se poziva kada se može promeniti stanje komande
		public event EventHandler CanExecuteChanged;

		// Metoda koja proverava da li se komanda može izvršiti
		public bool CanExecute(object parameter)
		{
			return true; // Komanda je uvek izvršljiva
		}

		// Metoda koja izvršava komandu za zatvaranje prozora
		public void Execute(object parameter)
		{
			receiver.Window.Close();
		}
	}
}
