using Client.ViewModels;
using System;
using System.Windows.Input;

// Komanda za osvježavanje liste stanica
namespace Client.Commands
{
	public class RefreshStationsCommand : ICommand
	{
		private MainViewModel receiver;

		// Konstruktor koji prima glavni pogled modela kao primaoca
		public RefreshStationsCommand(MainViewModel receiver)
		{
			this.receiver = receiver;
		}

		public event EventHandler CanExecuteChanged;

		// Metoda koja provjerava može li se izvršiti komanda
		public bool CanExecute(object parameter)
		{
			// Uvijek vraća true jer se komanda može izvršiti u svakom trenutku
			return true;
		}

		// Metoda koja izvršava komandu
		public void Execute(object parameter)
		{
			// Poziva metodu za osvježavanje liste stanica u glavnom pogledu modela
			receiver.RefreshStationsList();
		}
	}
}

