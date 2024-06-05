using Client.ViewModels;
using System;
using System.Windows.Input;

// Komanda za otvaranje dijaloga za dodavanje mjesta
namespace Client.Commands.Dialog_commands
{
	public class OpenAddPlaceDialogCommand : ICommand
	{
		// Prima MainViewModel kao primaoca
		private MainViewModel receiver;

		// Konstruktor koji postavlja primaoca komande
		public OpenAddPlaceDialogCommand(MainViewModel receiver)
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
			// Komanda može uvijek biti izvršena
			return true;
		}

		// Metoda koja izvršava komandu otvaranja dijaloga za dodavanje mjesta
		public void Execute(object parameter)
		{
			// Poziva metodu za otvaranje dijaloga za dodavanje mjesta
			receiver.OpenAddPlaceDialog();
		}
	}
}
