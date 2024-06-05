using Client.Contracts;
using System.Collections.Generic;

namespace Client.Managers
{
	// Klasa za upravljanje komandama primarnih entiteta, implementira IPrimaryEntityCommandManagement interfejs
	public class PrimaryEntityCommandManager : IPrimaryEntityCommandManagement
	{
		// Stekovi za čuvanje komandi za poništavanje (undo) i ponavljanje (redo)
		private Stack<IPrimaryEntityCommand> UndoCommands;
		private Stack<IPrimaryEntityCommand> RedoCommands;

		// Konstruktor koji inicijalizuje stekove za komande
		public PrimaryEntityCommandManager()
		{
			UndoCommands = new Stack<IPrimaryEntityCommand>();
			RedoCommands = new Stack<IPrimaryEntityCommand>();
		}

		// Metoda za dodavanje nove komande u stek za poništavanje
		public void Add(IPrimaryEntityCommand command)
		{
			UndoCommands.Push(command);
			RedoCommands.Clear(); // Briše redo stek kada se doda nova komanda
		}

		// Metoda za ponavljanje poslednje poništene komande
		public void Redo()
		{
			if (CanRedo())
			{
				IPrimaryEntityCommand command = RedoCommands.Pop();
				command.Redo();
				UndoCommands.Push(command);
			}
		}

		// Metoda za poništavanje poslednje izvršene komande
		public void Undo()
		{
			if (CanUndo())
			{
				IPrimaryEntityCommand command = UndoCommands.Pop();
				command.Undo();
				RedoCommands.Push(command);
			}
		}

		// Metoda koja proverava da li postoji komanda za poništavanje
		public bool CanUndo()
		{
			return UndoCommands.Count != 0;
		}

		// Metoda koja proverava da li postoji komanda za ponavljanje
		public bool CanRedo()
		{
			return RedoCommands.Count != 0;
		}
	}
}
