using Client.ViewModels;
using Common.DomainModels;
using System;
using System.Windows.Input;

// Komanda za brisanje mjesta
namespace Client.Commands
{
	public class DeletePlaceCommand : ICommand
	{
		// Prima glavni pogled modela kao primaoca
		private MainViewModel receiver;

		// Konstruktor koji prima glavni pogled modela
		public DeletePlaceCommand(MainViewModel receiver)
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
			// Poziva metodu za brisanje odabranog mjesta s parametrom mjesta
			receiver.DeleteSelectedPlace(parameter as Place);
		}
	}
}

