using Client.ViewModels;
using Common.DomainModels;
using System;
using System.Windows.Input;

namespace Client.Commands.Station_commands
{
	// Klasa koja implementira ICommand interfejs za dodavanje stanice
	public class AddStationCommand : ICommand
	{
		// Privatno polje za referencu na StationInputViewModel
		private StationInputViewModel receiver;

		// Konstruktor koji inicijalizuje AddStationCommand sa StationInputViewModel-om
		public AddStationCommand(StationInputViewModel receiver)
		{
			this.receiver = receiver;
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
			// Komanda može biti izvršena samo ako je prosleđeni parametar validan objekat Station
			return parameter != null && (parameter as Station).IsValid();
		}

		// Metoda koja izvršava komandu za dodavanje stanice
		public void Execute(object parameter)
		{
			// Poziva metodu AddStation na receiver-u (StationInputViewModel)
			receiver.AddStation(parameter as Station);
		}
	}
}
