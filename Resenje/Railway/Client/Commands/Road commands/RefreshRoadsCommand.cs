using Client.ViewModels;
using System;
using System.Windows.Input;

// Komanda za osvježavanje liste cesta
namespace Client.Commands
{
	public class RefreshRoadsCommand : ICommand
	{
		private MainViewModel receiver;

		// Konstruktor koji prima glavni pogled modela kao primaoca
		public RefreshRoadsCommand(MainViewModel receiver)
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
			// Poziva metodu za osvježavanje liste cesta u glavnom pogledu modela
			receiver.RefreshRoadsList();
		}
	}
}

