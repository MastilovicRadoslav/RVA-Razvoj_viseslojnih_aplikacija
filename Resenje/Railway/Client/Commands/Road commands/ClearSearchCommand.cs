using Client.ViewModels;
using System;
using System.Windows.Input;

// Komanda za brisanje pretrage puteva
namespace Client.Commands.Road_commands
{
	public class ClearSearchCommand : ICommand
	{
		// Prima MainViewModel kao primaoca
		private MainViewModel receiver;

		// Konstruktor koji postavlja primaoca komande
		public ClearSearchCommand(MainViewModel receiver)
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
			// Komanda uvijek može biti izvršena
			return true;
		}

		// Metoda koja izvršava komandu brisanja pretrage puteva
		public void Execute(object parameter)
		{
			// Poziva metodu za brisanje pretrage puteva
			receiver.ClearRoadsSearch();
		}
	}
}

