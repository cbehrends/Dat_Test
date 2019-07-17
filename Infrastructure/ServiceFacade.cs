using System;
using System.ServiceModel;
using QuickStart.DisplayHelpers;

namespace QuickStart.Infrastructure
{
	/// <summary>
	/// Represents the state of the TFMI service (i.e., its endpoint URL) and
	/// its behavior (logging a user into a session).
	/// </summary>
	/// <remarks>For more details on the Facade design pattern, see 
	/// <seealso cref="http://en.wikipedia.org/wiki/Facade_pattern"/>.  </remarks>
	public class ServiceFacade
	{
		private readonly string _url;

		public ServiceFacade(string url)
		{
			_url = url;
		}

		public SessionFacade Login(string user, string password)
		{
			// build client to TFMI service 
			var remoteAddress = new EndpointAddress(_url);
			var binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport) {MaxReceivedMessageSize = 2 << 20};
			var client = new TfmiFreightMatchingPortTypeClient(binding, remoteAddress);

			// build request
			var loginRequest = new LoginRequest
			                   {
			                   	loginOperation =
			                   		new LoginOperation {loginId = user, password = password, thirdPartyId = "SampleClient.NET"}
			                   };

			// build various headers required by the service method
			var applicationHeader = new ApplicationHeader
			                        {application = "Connexion C# .NET Test", applicationVersion = "1.0"};
			var correlationHeader = new CorrelationHeader();
			var sessionHeader = new SessionHeader
			                    {sessionToken = new SessionToken {primary = new byte[] {}, secondary = new byte[] {}}};

			// invoke the service
			WarningHeader warningHeader;
			LoginResponse loginResponse;
			client.Login(applicationHeader,
			             ref correlationHeader,
			             ref sessionHeader,
			             loginRequest,
			             out warningHeader,
			             out loginResponse);

			// return a SessionFacade, which wraps the login results along with the client object
			var data = loginResponse.loginResult.Item as LoginSuccessData;
			if (data == null)
			{
				Console.Error.WriteLine("Error logging in");
				var serviceError = loginResponse.loginResult.Item as ServiceError;
				serviceError.Display(1);
				return null;
			}
			return new SessionFacade(applicationHeader, correlationHeader, data, client);
		}
	}
}