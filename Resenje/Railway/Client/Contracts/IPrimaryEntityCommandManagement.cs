namespace Client.Contracts
{
	// Interfejs za upravljanje komandama primarnih entiteta
	public interface IPrimaryEntityCommandManagement
	{
		// Metoda koja proverava da li se može izvršiti operacija poništavanja
		bool CanUndo();

		// Metoda koja proverava da li se može izvršiti operacija ponovnog izvršavanja
		bool CanRedo();

		// Metoda za izvršavanje operacije poništavanja
		void Undo();

		// Metoda za izvršavanje operacije ponovnog izvršavanja
		void Redo();

		// Metoda za dodavanje nove komande
		void Add(IPrimaryEntityCommand command);
	}
}
