using Client.Commands;
using Common.Contracts;
using Common.DomainModels;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Client.ViewModels
{
	// ViewModel za prozor dodavanja korisnika, koristi MVVM obrazac
	public class AddUserViewModel : INotifyPropertyChanged
	{
		// Konstruktor za inicijalizaciju AddUserViewModel sa korisničkim servisom, logger-om, glavnim korisnikom, novim korisnikom i prozorom
		public AddUserViewModel(IUserService userService, ILogging logger, User principal, User newUser, Window window)
		{
			UserService = userService;
			Logger = logger;
			Principal = principal;
			NewUser = newUser;
			Window = window;
			AddUserCommand = new AddUserCommand(this);
		}

		#region fields
		// Polja i svojstva
		public IUserService UserService { get; set; }
		public ILogging Logger { get; set; }
		public User Principal { get; set; }
		private User newUser;

		public Window Window { get; set; }

		public User NewUser
		{
			get { return newUser; }
			set
			{
				newUser = value;
				OnPropertyChanged("NewUser");
			}
		}
		#endregion

		#region commands
		// Komanda za dodavanje korisnika
		public ICommand AddUserCommand { get; set; }
		#endregion

		#region Bindings
		// Binding svojstva za UI
		public string Name { get; set; }
		public string UserName { get; set; }
		public string LastName { get; set; }
		public string Password { get; set; }
		public bool IsAdmin { get; set; }
		#endregion

		#region AddUser
		// Metoda za dodavanje korisnika
		public void AddUser(User user)
		{
			try
			{
				Logger.LogNewMessage($"Trying to add new user with username {NewUser.UserName}", LogType.INFO);
				UserService.AddUser(user);
				Logger.LogNewMessage($"User added successfully", LogType.INFO);
				Window.Close();
			}
			catch (Exception ex)
			{
				Logger.LogNewMessage($"Failed to add user. Error message {ex.Message}", LogType.ERROR);
			}
		}
		#endregion

		// Implementacija INotifyPropertyChanged interfejsa
		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(String propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
