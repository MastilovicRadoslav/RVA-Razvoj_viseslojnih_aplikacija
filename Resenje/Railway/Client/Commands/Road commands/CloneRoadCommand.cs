using Client.Contracts;
using Client.ViewModels;
using Common.DomainModels;
using System;
using System.Windows.Input;

// Komanda za kloniranje ceste
namespace Client.Commands
{
	public class CloneRoadCommand : IPrimaryEntityCommand
	{
		// Prima glavni pogled modela kao primaoca
		public MainViewModel Reciever { get; set; }

		// Predstavlja prethodno stanje ceste prije kloniranja
		public Road PredicatePreviousState { get; set; }

		// Predstavlja novo stanje ceste nakon kloniranja
		public Road PredicatePostState { get; set; }

		// Interfejs za upravljanje primarnim entitetima komande
		public IPrimaryEntityCommandManagement PrimaryEntityCommandManager { get; set; }

		// Konstruktor koji prima glavni pogled modela i upravljanje primarnim entitetima komande
		public CloneRoadCommand(MainViewModel receiver, IPrimaryEntityCommandManagement commandManager)
		{
			Reciever = receiver;
			this.PrimaryEntityCommandManager = commandManager;
		}

		// Konstruktor koji prima kloniranu komandu
		public CloneRoadCommand(CloneRoadCommand command)
		{
			Reciever = command.Reciever;
			PrimaryEntityCommandManager = command.PrimaryEntityCommandManager;
			PredicatePostState = command.PredicatePostState;
			PredicatePreviousState = command.PredicatePreviousState;
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
			// Vraća true ako je parametar različit od null
			return parameter != null;
		}

		// Metoda koja vraća komandu u prethodno stanje
		public void Undo()
		{
			// Poziva metodu za brisanje odabrane ceste s postavljenim prethodnim stanjem ceste
			Reciever.DeleteSelectedRoad(PredicatePostState);
		}

		// Metoda koja izvršava komandu
		public void Execute(object parameter)
		{
			// Postavlja parametar kao cestu
			Road road = parameter as Road;
			// Postavlja prethodno stanje ceste kao trenutnu cestu
			PredicatePreviousState = road;
			// Klonira cestu i postavlja je kao novo stanje ceste
			PredicatePostState = Reciever.CloneSelectedRoad(road);
			// Dodaje kloniranu komandu u upravljanje primarnim entitetima komande
			PrimaryEntityCommandManager.Add(new CloneRoadCommand(this));
		}

		// Metoda koja ponavlja izvršenu akciju
		public void Redo()
		{
			// Postavlja novo stanje ceste kao kloniranu cestu
			PredicatePostState = Reciever.CloneSelectedRoad(PredicatePreviousState);
		}
	}
}

