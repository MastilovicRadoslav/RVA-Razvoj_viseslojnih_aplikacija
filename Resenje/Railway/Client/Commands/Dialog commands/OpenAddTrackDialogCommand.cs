using Client.ViewModels;
using System;
using System.Windows.Input;

// Komanda za otvaranje dijaloga za dodavanje pruge
namespace Client.Commands.Dialog_commands
{
	public class OpenAddTrackDialogCommand : ICommand
	{
		// Prima MainViewModel kao primaoca
		private MainViewModel receiver;

		// Konstruktor koji postavlja primaoca komande
		public OpenAddTrackDialogCommand(MainViewModel receiver)
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
			// Uvijek može biti izvršena
			return true;
		}

		// Metoda koja izvršava komandu otvaranja dijaloga za dodavanje pruge
		public void Execute(object parameter)
		{
			// Poziva metodu za otvaranje dijaloga za dodavanje pruge
			receiver.OpenAddTrackDialog();
		}
	}
}

