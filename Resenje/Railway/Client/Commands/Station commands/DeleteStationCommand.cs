using Client.ViewModels;
using Common.DomainModels;
using System;
using System.Windows.Input;

// Komanda za brisanje stanice
namespace Client.Commands
{
	public class DeleteStationCommand : ICommand
	{
		// Prima glavni pogled modela kao primaoca
		private MainViewModel receiver;

		// Konstruktor koji prima glavni pogled modela
		public DeleteStationCommand(MainViewModel receiver)
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
			// Vraća true ako je parametar različit od null
			return parameter != null;
		}

		// Metoda koja izvršava komandu
		public void Execute(object parameter)
		{
			// Poziva metodu za brisanje odabrane stanice s parametrom stanice
			receiver.DeleteSelectedStation(parameter as Station);
		}
	}
}

