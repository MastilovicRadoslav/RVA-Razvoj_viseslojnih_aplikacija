using Client.ViewModels;
using Common.DomainModels;
using System;
using System.Windows.Input;

namespace Client.Commands.Station_commands
{
	// Klasa koja implementira ICommand interfejs za ažuriranje stanice
	public class UpdateStationCommand : ICommand
	{
		// Privatno polje za referencu na StationInputViewModel
		private StationInputViewModel receiver;

		// Konstruktor koji inicijalizuje UpdateStationCommand sa StationInputViewModel-om
		public UpdateStationCommand(StationInputViewModel receiver)
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

		// Metoda koja izvršava komandu za ažuriranje stanice
		public void Execute(object parameter)
		{
			// Poziva metodu UpdateStation na receiver-u (StationInputViewModel)
			receiver.UpdateStation(parameter as Station);
		}
	}
}
