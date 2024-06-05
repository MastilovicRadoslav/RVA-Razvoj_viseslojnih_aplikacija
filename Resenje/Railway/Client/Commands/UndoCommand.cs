using Client.Contracts;
using System;
using System.Windows.Input;

// Komanda za vraćanje prethodne akcije
namespace Client.Commands
{
	public class UndoCommand : ICommand
	{
		// Prima upravljanje primarnim entitetima komande kao primaoca
		private IPrimaryEntityCommandManagement reciever;

		// Konstruktor koji prima upravljanje primarnim entitetima komande
		public UndoCommand(IPrimaryEntityCommandManagement reciever)
		{
			this.reciever = reciever;
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
			// Vraća rezultat provjere mogućnosti poništavanja prethodne akcije
			return reciever.CanUndo();
		}

		// Metoda koja izvršava komandu poništavanja prethodne akcije
		public void Execute(object parameter)
		{
			// Poziva metodu za poništavanje prethodne akcije
			reciever.Undo();
		}
	}
}

