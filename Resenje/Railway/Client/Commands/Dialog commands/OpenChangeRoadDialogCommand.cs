using Client.ViewModels;
using Common.DomainModels;
using System;
using System.Windows.Input;

// Komanda za otvaranje dijaloga za izmjenu puta
namespace Client.Commands.Dialog_commands
{
	public class OpenChangeRoadDialogCommand : ICommand
	{
		// Prima MainViewModel kao primaoca
		private MainViewModel receiver;

		// Konstruktor koji postavlja primaoca komande
		public OpenChangeRoadDialogCommand(MainViewModel receiver)
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
			// Može se izvršiti ako je parametar različit od null
			return parameter != null;
		}

		// Metoda koja izvršava komandu otvaranja dijaloga za izmjenu puta
		public void Execute(object parameter)
		{
			// Poziva metodu za otvaranje dijaloga za izmjenu puta i proslijeđuje parametar kao put
			receiver.OpenChangeRoadDialog(parameter as Road);
		}
	}
}

