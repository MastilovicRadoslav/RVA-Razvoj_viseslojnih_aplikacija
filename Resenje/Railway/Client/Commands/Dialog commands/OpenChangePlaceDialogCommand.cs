﻿using Client.ViewModels;
using Common.DomainModels;
using System;
using System.Windows.Input;

// Komanda za otvaranje dijaloga za promjenu mjesta
namespace Client.Commands.Dialog_commands
{
	public class OpenChangePlaceDialogCommand : ICommand
	{
		// Prima MainViewModel kao primaoca
		private MainViewModel receiver;

		// Konstruktor koji postavlja primaoca komande
		public OpenChangePlaceDialogCommand(MainViewModel receiver)
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
			// Komanda može biti izvršena ako je parametar različit od null
			return parameter != null;
		}

		// Metoda koja izvršava komandu otvaranja dijaloga za promjenu mjesta
		public void Execute(object parameter)
		{
			// Poziva metodu za otvaranje dijaloga za promjenu mjesta, proslijeđujući mu parametar kao Place objekt
			receiver.OpenChangePlaceDialog(parameter as Place);
		}
	}
}