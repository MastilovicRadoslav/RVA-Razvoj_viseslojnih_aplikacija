using Common.Contracts;
using System;
using System.IdentityModel.Selectors;
using System.ServiceModel;

namespace Server.ServiceHosts
{
	public class RailwayServiceHost  //Ova klasa upravlja postavkama i operacijama za WCF servisni host.
	{
		protected UserNamePasswordValidator authValidator; // Validator za korisničko ime i lozinku
		protected BasicHttpBinding binding; // Binding za BasicHttp
		protected Type contract; // Tip ugovora
		protected ILogging logger; // Logger za beleženje događaja
		protected string serviceEndpointName; // Naziv krajnje tačke servisa
		protected ServiceHost serviceHost; // Host za servis

		/// <summary>
		/// Konstruktor klase RailwayServiceHost
		/// </summary>
		/// <param name="serviceEndpointName">Naziv krajnje tačke servisa</param>
		/// <param name="binding">Binding za BasicHttp</param>
		/// <param name="authValidator">Validator za korisničko ime i lozinku</param>
		/// <param name="serviceHost">Host za servis</param>
		/// <param name="contract">Tip ugovora</param>
		/// <param name="logger">Instanca loggera</param>
		public RailwayServiceHost(string serviceEndpointName, BasicHttpBinding binding, UserNamePasswordValidator authValidator, ServiceHost serviceHost, Type contract, ILogging logger)
		{
			this.serviceEndpointName = serviceEndpointName;
			this.binding = binding;
			this.authValidator = authValidator;
			this.serviceHost = serviceHost;
			this.contract = contract;
			this.logger = logger;

			// Podesi sigurnost za BasicHttpBinding
			this.binding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
			this.binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;

			// Podesi korisničko ime i lozinku za validaciju
			this.serviceHost.Credentials.UserNameAuthentication.UserNamePasswordValidationMode = System.ServiceModel.Security.UserNamePasswordValidationMode.Custom;
			this.serviceHost.Credentials.UserNameAuthentication.CustomUserNamePasswordValidator = authValidator;

			// Dodaj krajnju tačku servisa
			this.serviceHost.AddServiceEndpoint(this.contract, this.binding, this.serviceEndpointName);
		}

		/// <summary>
		/// Zatvara kanal servisa
		/// </summary>
		public void CloseServiceChannel()
		{
			try
			{
				logger.LogNewMessage($"Closing service host for type {contract}", LogType.INFO);
				serviceHost.Close();
				logger.LogNewMessage($"Successfully closed host for type {contract}", LogType.INFO);
			}
			catch (Exception ex)
			{
				logger.LogNewMessage($"Failed to close service host for type {contract}. ERROR message {ex.Message}", LogType.ERROR);
			}
		}

		/// <summary>
		/// Otvara kanal servisa
		/// </summary>
		public void OpenServiceChannel()
		{
			try
			{
				logger.LogNewMessage($"Trying to open service host for type {contract}", LogType.INFO);
				serviceHost.Open();
				logger.LogNewMessage($"Successfully opened host for type {contract}", LogType.INFO);
			}
			catch (Exception ex)
			{
				logger.LogNewMessage($"Failed to open service host for type {contract}. ERROR message {ex.Message}", LogType.ERROR);
			}
		}
	}
}
