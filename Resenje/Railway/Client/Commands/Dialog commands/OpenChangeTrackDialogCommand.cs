using Client.ViewModels;
using Common.DomainModels;
using System;
using System.Windows.Input;

// Komanda za otvaranje dijaloga za izmjenu pruge
namespace Client.Commands.Dialog_commands
{
	public class OpenChangeTrackDialogCommand : ICommand
	{
		// Prima MainViewModel kao primaoca
		private MainViewModel receiver;

		// Konstruktor koji postavlja primaoca komande
		public OpenChangeTrackDialogCommand(MainViewModel receiver)
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
			// Provjerava se da li je parametar različit od null
			return parameter != null;
		}

		// Metoda koja izvršava komandu otvaranja dijaloga za izmjenu pruge
		public void Execute(object parameter)
		{
			// Poziva metodu za otvaranje dijaloga za izmjenu pruge, proslijeđujući prugu kao parametar
			receiver.OpenChangeTrackDialog(parameter as Track);
		}
	}
}

