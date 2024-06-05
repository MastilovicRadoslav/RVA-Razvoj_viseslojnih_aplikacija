using Client.ViewModels;
using Common.DomainModels;
using System;
using System.Windows.Input;

namespace Client.Commands.Road_commands
{
	// Klasa koja implementira ICommand interfejs za uklanjanje stanica sa puta
	public class RemoveAttachedStationsCommand : ICommand
	{
		// Privatno polje za referencu na RoadInputViewModel
		private RoadInputViewModel reciever;

		// Konstruktor koji inicijalizuje RemoveAttachedStationsCommand sa RoadInputViewModel-om
		public RemoveAttachedStationsCommand(RoadInputViewModel reciever)
		{
			this.reciever = reciever;
		}

		// Događaj koji se poziva kada se može promeniti stanje komande
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

		// Metoda koja proverava da li se komanda može izvršiti
		public bool CanExecute(object parameter)
		{
			// Komanda može biti izvršena samo ako je prosleđeni parametar različit od null
			return parameter != null;
		}

		// Metoda koja izvršava komandu za uklanjanje stanica sa puta
		public void Execute(object parameter)
		{
			// Poziva metodu RemoveStationsFromRoad na reciever-u (RoadInputViewModel)
			reciever.RemoveStationsFromRoad(parameter as Station);
		}
	}
}
