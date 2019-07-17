using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using QuickStart.Infrastructure;

namespace QuickStart
{
	public abstract class Program
	{
		private readonly ConfiguredProperties _properties;

		/// <summary>
		/// Control flow begins here.
		/// </summary>
		/// <param name="args">Command-line arguments. See <see cref="CommandLineInput.ShowUsage"/> 
		/// for usage.</param>.
		/// <returns>0 on success, -1 on bad input, 1 on runtime error.  See also <see cref="Result"/> 
		/// enum.</returns>
		public static int Main(string[] args)
		{
			var input = new CommandLineInput(args);
			Feature? feature = input.Parse();

			if (feature == null)
			{
				return CommandLineInput.ShowUsage();
			}

			// hijack Console.Out so it writes to a text file
			RedirectOutput(input.OutputFilename);

			// load configurable properties from a properties file
			ConfiguredProperties properties = ConfiguredProperties.Load(input.PropertiesFilename);

			// map the feature and configurable properties to a Program instance
			Program p = GetProgramInstance(feature.Value, properties);

			if (p == null)
			{
				return Result.Invalid;
			}

			return TryToExecute(p, DateTime.Now);
		}

		protected Program(ConfiguredProperties properties)
		{
			_properties = properties;
			AlarmUrl = BuildAlarmUrl(_properties);
		}

		protected virtual bool RequireDistinctUserAccounts
		{
			get { return false; }
		}

		protected Uri AlarmUrl { get; private set; }

		protected abstract int Execute(DateTime when);

		private static Uri BuildAlarmUrl(ConfiguredProperties configuredProperties)
		{
			var uriBuilder = new UriBuilder
			                 {
			                 	Scheme = "https",
								Host = configuredProperties.Host,
								Path = configuredProperties.Path,
			                 	Port = configuredProperties.Port
			                 };
			Uri uri;
			if (!TryUri(uriBuilder, out uri))
			{
				IPAddress ipAddress;
				if (configuredProperties.Host != null && IPAddress.TryParse(configuredProperties.Host, out ipAddress)) {}
				else
				{
					string hostName = Dns.GetHostName();
					IPAddress[] addresses = Dns.GetHostAddresses(hostName);
					ipAddress = addresses.First(x => x.AddressFamily == AddressFamily.InterNetwork);
				}
				uriBuilder.Host = ipAddress.ToString();
				uri = uriBuilder.Uri;
			}
			return uri;
		}
		
		private static bool TryUri(UriBuilder builder, out Uri uri)
		{
			bool wellFormed = false;
			try
			{
				uri = builder.Uri;
				wellFormed = true;
			}
			catch (UriFormatException)
			{
				uri = null;
			}
			return wellFormed;
		}


		private static Program GetProgramInstance(Feature feature, ConfiguredProperties properties)
		{
			Type programType = typeof(Program);
			foreach (Type type in programType.Assembly.GetTypes())
			{
				// if a concrete type, derived from Program
				if (type.IsAbstract == false && type.BaseType == programType)
				{
					// if the type's name matches a Feature value
					if (type.Name.Equals(feature.ToString()))
					{
						// if it can be constructed from a ConfiguredProperties instance
						ConstructorInfo constructor = type.GetConstructor(new[] {typeof(ConfiguredProperties)});
						if (constructor != null)
						{
							// use that type
							object invoked = constructor.Invoke(new object[] {properties});
							return (Program) invoked;
						}
					}
				}
			}

			Console.WriteLine("Feature '{0}' not recognized.", feature);
			return null;
		}

		protected bool Account1FailsLogin(out SessionFacade session)
		{
			return LoginFails(_properties.User1, _properties.Password1, out session);
		}

		protected bool Account2FailsLogin(out SessionFacade session)
		{
			return LoginFails(_properties.User2, _properties.Password2, out session);
		}

		protected bool DobAccountFailsLogin(out SessionFacade session)
		{
			return LoginFails(_properties.DobUser, _properties.DobPassword, out session);
		}

		private bool LoginFails(string user, string password, out SessionFacade session)
		{
			session = new ServiceFacade(_properties.Url).Login(user, password);
			if (session == null)
			{
				WriteFailedLogin(user, password);
				return true;
			}
			return false;
		}

		private static void RedirectOutput(string filename)
		{
			Console.SetOut(new StreamWriter(filename));
		}

		private static int TryToExecute(Program program, DateTime when)
		{
			try
			{
				if (program.Validate() == Result.Invalid)
				{
					return Result.Invalid;
				}
				return program.Execute(when);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				if (ex.InnerException != null)
				{
					Console.WriteLine(ex.InnerException.Message);
				}
			}
			finally
			{
				Console.Out.Flush();
				Console.Out.Close();
			}
			return Result.Failure;
		}

		protected virtual int Validate()
		{
			if (RequireDistinctUserAccounts && _properties.UserAccountsAreNotDistinct)
			{
				Console.WriteLine("User accounts must be different for the demo to work.");
				Console.WriteLine("Searches and alarms won't find loads posted from the same account.");
				Console.WriteLine("  Shared account was '{0}'.", _properties.User1);
				return Result.Invalid;
			}
			return Result.Success;
		}

		private static void WriteFailedLogin(string user, string password)
		{
			Console.WriteLine("Login failed for user '{0}', password '{1}'.", user, password);
		}

		protected static class Result
		{
			public const int Invalid = -1;
			public const int Success = 0;
			public const int Failure = 1;
		}
	}
}