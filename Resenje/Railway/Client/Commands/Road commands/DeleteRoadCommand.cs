using Client.Contracts;
using Client.ViewModels;
using Common.DomainModels;
using System;
using System.Windows.Input;

// Komanda za brisanje ceste
namespace Client.Commands
{
	public class DeleteRoadCommand : IPrimaryEntityCommand
	{
		// Prima glavni pogled modela kao primaoca
		public MainViewModel Reciever { get; set; }

		// Konstruktor koji prima glavni pogled modela i upravljanje primarnim entitetima
		public DeleteRoadCommand(MainViewModel receiver, IPrimaryEntityCommandManagement commandManager)
		{
			Reciever = receiver;
			PrimaryEntityCommandManager = commandManager;
		}

		// Konstruktor za kloniranje komande
		public DeleteRoadCommand(DeleteRoadCommand command)
		{
			Reciever = command.Reciever;
			PrimaryEntityCommandManager = command.PrimaryEntityCommandManager;
			PredicatePostState = command.PredicatePostState;
			PredicatePreviousState = command.PredicatePreviousState;
		}

		// Prethodno stanje ceste
		public Road PredicatePreviousState { get; set; }

		// Naknadno stanje ceste
		public Road PredicatePostState { get; set; }

		// Upravljanje primarnim entitetima
		public IPrimaryEntityCommandManagement PrimaryEntityCommandManager { get; set; }

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

		// Metoda za ponovno izvršavanje akcije
		public void Redo()
		{
			// Poziva metodu za brisanje odabrane ceste s prethodnim stanjem ceste kao parametrom
			Reciever.DeleteSelectedRoad(PredicatePreviousState);
		}

		// Metoda koja izvršava komandu
		public void Execute(object parameter)
		{
			// Dobavlja cestu iz parametra
			Road road = parameter as Road;
			// Postavlja prethodno stanje ceste na duboku kopiju ceste
			PredicatePreviousState = road.DeepCopy();
			// Postavlja naknadno stanje ceste na duboku kopiju ceste
			PredicatePostState = road.DeepCopy();
			// Postavlja ID naknadnog stanja na 0 kako bi se spriječilo ažuriranje postojeće ceste
			PredicatePostState.Id = 0;
			// Poziva metodu za brisanje odabrane ceste
			Reciever.DeleteSelectedRoad(road);
			// Dodaje trenutnu komandu u upravljanje primarnim entitetima za kasnije undo akcije
			PrimaryEntityCommandManager.Add(new DeleteRoadCommand(this));
		}

		// Metoda za poništavanje akcije
		public void Undo()
		{
			// Dodaje prethodno stanje ceste kao novo stanje ceste
			PredicatePreviousState = Reciever.AddRoad(PredicatePostState);
		}
	}
}

