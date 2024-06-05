using Client.Models;
using Client.ViewModels;
using System;
using System.Windows.Input;

// Komanda za pretragu puteva
namespace Client.Commands.Road_commands
{
	public class SearchRoadsCommand : ICommand
	{
		// Prima MainViewModel kao primaoca
		private MainViewModel receiver;

		// Konstruktor koji postavlja primaoca komande
		public SearchRoadsCommand(MainViewModel receiver)
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
			// Komanda može biti izvršena ako je parametar različit od null i ako je model pretrage puta validan
			return parameter != null && (parameter as RoadSearchModel).IsValid();
		}

		// Metoda koja izvršava komandu pretrage puteva
		public void Execute(object parameter)
		{
			// Poziva metodu za pretragu puteva sa zadanim modelom pretrage
			receiver.SearchRoads(parameter as RoadSearchModel);
		}
	}
}

