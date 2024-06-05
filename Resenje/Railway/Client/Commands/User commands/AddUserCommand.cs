using Client.ViewModels;
using Common.DomainModels;
using System;
using System.Windows.Input;

namespace Client.Commands
{
	public class AddUserCommand : ICommand
	{
		private AddUserViewModel reciever;

		public AddUserCommand(AddUserViewModel reciever)
		{
			this.reciever = reciever;
		}

		public event EventHandler CanExecuteChanged
		{
			add
			{
				CommandManager.RequerySuggested += value;
			}
			remove
			{
				CommandManager.RequerySuggested += value;
			}
		}

		public bool CanExecute(object parameter)
		{
			return parameter != null && (parameter as User).IsValid();
		}

		public void Execute(object parameter)
		{
			reciever.AddUser(parameter as User);
		}
	}
}
