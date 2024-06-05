﻿using Client.ViewModels;
using Common.DomainModels;
using System;
using System.Windows.Input;

namespace Client.Commands.Place_commands
{
	// Klasa koja implementira ICommand interfejs za ažuriranje mesta
	public class UpdatePlaceCommand : ICommand
	{
		// Referenca na PlaceInputViewModel
		private PlaceInputViewModel receiver;

		// Konstruktor koji inicijalizuje UpdatePlaceCommand sa PlaceInputViewModel-om
		public UpdatePlaceCommand(PlaceInputViewModel receiver)
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
			// Komanda može biti izvršena samo ako je prosleđeni parametar validan objekat Place
			return parameter != null && (parameter as Place).IsValid();
		}

		// Metoda koja izvršava komandu za ažuriranje mesta
		public void Execute(object parameter)
		{
			// Poziva metodu UpdatePlace na receiver-u (PlaceInputViewModel)
			receiver.UpdatePlace(parameter as Place);
		}
	}
}
