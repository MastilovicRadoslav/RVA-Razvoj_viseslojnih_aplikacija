using Client.ViewModels;
using System;
using System.Windows.Input;

// Komanda za ažuriranje korisničkog profila
namespace Client.Commands
{
	public class UpdateProfileCommand : ICommand
	{
		private MainViewModel receiver;

		// Konstruktor koji prima glavni pogled modela kao primaoca
		public UpdateProfileCommand(MainViewModel receiver)
		{
			this.receiver = receiver;
		}

		public event EventHandler CanExecuteChanged;

		// Metoda koja provjerava može li se izvršiti komanda
		public bool CanExecute(object parameter)
		{
			// Provjerava da li je korisnički profil dostupan
			return receiver.User != null;
		}

		// Metoda koja izvršava komandu
		public void Execute(object parameter)
		{
			// Poziva metodu za ažuriranje korisničkog profila u glavnom pogledu modela
			receiver.UpdateUser();
		}
	}
}

