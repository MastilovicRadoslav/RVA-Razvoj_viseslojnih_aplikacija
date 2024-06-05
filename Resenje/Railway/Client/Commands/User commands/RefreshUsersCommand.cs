using Client.ViewModels;
using System;
using System.Windows.Input;

// Komanda za osvježavanje liste korisnika
namespace Client.Commands
{
	public class RefreshUsersCommand : ICommand
	{
		private MainViewModel receiver;

		// Konstruktor koji prima glavni pogled modela kao primaoca
		public RefreshUsersCommand(MainViewModel receiver)
		{
			this.receiver = receiver;
		}

		public event EventHandler CanExecuteChanged;

		// Metoda koja provjerava može li se izvršiti komanda
		public bool CanExecute(object parameter)
		{
			return true;
		}

		// Metoda koja izvršava komandu
		public void Execute(object parameter)
		{
			// Poziva metodu za osvježavanje liste korisnika u glavnom pogledu modela
			receiver.RefreshUsersList();
		}
	}
}

