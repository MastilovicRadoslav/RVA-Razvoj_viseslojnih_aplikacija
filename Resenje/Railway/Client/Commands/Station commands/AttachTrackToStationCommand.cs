using Client.ViewModels;
using Common.DomainModels;
using System;
using System.Windows.Input;

namespace Client.Commands.Station_commands
{
	// Klasa koja implementira ICommand interfejs za povezivanje pruge sa stanicom
	public class AttachTrackToStationCommand : ICommand
	{
		// Privatno polje za referencu na StationInputViewModel
		private StationInputViewModel receiver;

		// Konstruktor koji inicijalizuje AttachTrackToStationCommand sa StationInputViewModel-om
		public AttachTrackToStationCommand(StationInputViewModel receiver)
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
			// Komanda može biti izvršena samo ako je prosleđeni parametar različit od null
			return parameter != null;
		}

		// Metoda koja izvršava komandu za povezivanje pruge sa stanicom
		public void Execute(object parameter)
		{
			// Poziva metodu AttachTrackToStation na receiver-u (StationInputViewModel)
			receiver.AttachTrackToStation(parameter as Track);
		}
	}
}
