using Common.DomainModels;
using System.Windows.Input;

namespace Client.Contracts
{
	public interface IPrimaryEntityCommand : ICommand
	{
		void Redo();
		void Undo();
		Road PredicatePreviousState { get; set; }
		Road PredicatePostState { get; set; }
		IPrimaryEntityCommandManagement PrimaryEntityCommandManager { get; set; }
	}
}
