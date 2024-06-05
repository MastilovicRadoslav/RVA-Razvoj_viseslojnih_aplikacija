﻿using Client.Contracts;
using Client.ViewModels;
using Common.DomainModels;
using System;
using System.Windows.Input;

namespace Client.Commands.Road_commands
{
	// Klasa koja implementira IPrimaryEntityCommand interfejs za dodavanje puta
	public class AddRoadCommand : IPrimaryEntityCommand
	{
		// Svojstvo za referencu na RoadInputViewModel
		public RoadInputViewModel Reciever { get; set; }

		// Konstruktor koji inicijalizuje AddRoadCommand sa RoadInputViewModel-om i PrimaryEntityCommandManager-om
		public AddRoadCommand(RoadInputViewModel reciever, IPrimaryEntityCommandManagement primaryEntityCommandManager)
		{
			this.Reciever = reciever;
			PrimaryEntityCommandManager = primaryEntityCommandManager;
		}

		// Konstruktor za kopiranje AddRoadCommand objekta
		public AddRoadCommand(AddRoadCommand command)
		{
			Reciever = command.Reciever;
			PrimaryEntityCommandManager = command.PrimaryEntityCommandManager;
			PredicatePostState = command.PredicatePostState;
			PredicatePreviousState = command.PredicatePreviousState;
		}

		// Svojstva za čuvanje prethodnog i novog stanja puta
		public Road PredicatePreviousState { get; set; }
		public Road PredicatePostState { get; set; }
		public IPrimaryEntityCommandManagement PrimaryEntityCommandManager { get; set; }

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
			return parameter != null && (parameter as Road).IsValid();
		}

		// Metoda koja izvršava komandu za dodavanje puta
		public void Execute(object parameter)
		{
			Road road = parameter as Road;
			PredicatePreviousState = road;
			PredicatePostState = Reciever.AddNewRoad(road);
			PrimaryEntityCommandManager.Add(new AddRoadCommand(this));
		}

		// Metoda za ponovno izvršavanje komande (redo)
		public void Redo()
		{
			PredicatePostState = Reciever.AddNewRoad(PredicatePreviousState);
		}

		// Metoda za poništavanje komande (undo)
		public void Undo()
		{
			Reciever.DeleteRoad(PredicatePostState);
		}
	}
}