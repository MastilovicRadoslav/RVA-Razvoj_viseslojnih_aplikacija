using Client.ViewModels;
using Common.DomainModels;
using System;
using System.Windows.Input;

// Komanda za otvaranje dijaloga za dodavanje korisnika
namespace Client.Commands
{
	public class OpenAddUserDialogCommand : ICommand
	{
		// Prima MainViewModel kao primaoca
		private MainViewModel receiver;

		// Konstruktor koji postavlja primaoca komande
		public OpenAddUserDialogCommand(MainViewModel receiver)
		{
			this.receiver = receiver;
		}

		// Događaj koji se pokreće kada se promijeni mogućnost izvršenja komande
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

		// Metoda koja provjerava može li se izvršiti komanda
		public bool CanExecute(object parameter)
		{
			// Provjerava da li je parametar različit od null i da li je korisnik administrator
			return parameter != null && (parameter as User).IsAdmin;
		}

		// Metoda koja izvršava komandu otvaranja dijaloga za dodavanje korisnika
		public void Execute(object parameter)
		{
			// Poziva metodu za otvaranje dijaloga za dodavanje korisnika
			receiver.OpenAddUserDialog();
		}
	}
}

