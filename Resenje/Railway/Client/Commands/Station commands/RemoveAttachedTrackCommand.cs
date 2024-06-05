using Client.ViewModels;
using Common.DomainModels;
using System;
using System.Windows.Input;

namespace Client.Commands.Station_commands
{
	// Klasa koja implementira ICommand interfejs za uklanjanje pruge sa stanice
	public class RemoveAttachedTrackCommand : ICommand
	{
		// Privatno polje za referencu na StationInputViewModel
		private StationInputViewModel receiver;

		// Konstruktor koji inicijalizuje RemoveAttachedTrackCommand sa StationInputViewModel-om
		public RemoveAttachedTrackCommand(StationInputViewModel receiver)
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

		// Metoda koja izvršava komandu za uklanjanje pruge sa stanice
		public void Execute(object parameter)
		{
			// Poziva metodu RemoveTrackFromStation na receiver-u (StationInputViewModel)
			receiver.RemoveTrackFromStation(parameter as Track);
		}
	}
}
