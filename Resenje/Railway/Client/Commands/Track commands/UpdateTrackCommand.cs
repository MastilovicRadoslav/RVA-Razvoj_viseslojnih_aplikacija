using Client.ViewModels;
using Common.DomainModels;
using System;
using System.Windows.Input;

namespace Client.Commands.Track_Commands
{
	// Klasa koja implementira ICommand interfejs za ažuriranje pruge
	public class UpdateTrackCommand : ICommand
	{
		// Privatno polje za referencu na TrackInputViewModel
		private TrackInputViewModel receiver;

		// Konstruktor koji inicijalizuje UpdateTrackCommand sa TrackInputViewModel-om
		public UpdateTrackCommand(TrackInputViewModel receiver)
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
			// Komanda može biti izvršena samo ako je prosleđeni parametar validan objekat Track
			return parameter != null && (parameter as Track).IsValid();
		}

		// Metoda koja izvršava komandu za ažuriranje pruge
		public void Execute(object parameter)
		{
			// Poziva metodu UpdateTrack na receiver-u (TrackInputViewModel)
			receiver.UpdateTrack(parameter as Track);
		}
	}
}
