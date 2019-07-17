#region usings
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
#endregion

namespace QuickStart.Infrastructure
{
	public class ConfiguredProperties
	{
		private readonly Lazy<DateTime> _lazyExampleDate;

		private ConfiguredProperties(string[] lines)
		{
			var dictionary = new Dictionary<string, Action<string>>
			                 {
			                 	{"url=", s => Url = s},
			                 	{"loginId1=", s => User1 = s},
			                 	{"loginId2=", s => User2 = s},
			                 	{"password1=", s => Password1 = s},
			                 	{"password2=", s => Password2 = s},
			                 	{"path=", s => Path = s},
			                 	{"port=", s => Port = int.Parse(s)},
			                 	{"dobCarrierExampleCarrierId=", s => ExampleCarrierId = s},
			                 	{"loginDob=", s => DobUser = s},
			                 	{"passwordDob=", s => DobPassword = s},
								{"host=", SetHost},
			                 	{"dobCarrierExampleSinceDate=", s => DobCarrierExampleSinceDateRawString = s},
			                 	{"dobCarrierExampleSinceDateFormat=", s => DobCarrierExampleSinceDateFormat = s}
			                 };

			foreach (string line in lines)
			{
				KeyValuePair<string, Action<string>>[] immutableCopy = dictionary.ToArray();
				foreach (KeyValuePair<string, Action<string>> pair in immutableCopy)
				{
					if (line.StartsWith(pair.Key))
					{
						pair.Value.Invoke(line.Substring(pair.Key.Length));
						dictionary.Remove(pair.Key);
						break;
					}
				}
			}

			_lazyExampleDate = new Lazy<DateTime>(Parse);
		}

		public string DobUser { get; private set; }
		public string DobPassword { get; private set; }
		public DateTime DobCarrierExampleSinceDate
		{
			get { return _lazyExampleDate.Value; }
		}
		private string DobCarrierExampleSinceDateRawString { get; set; }
		private string DobCarrierExampleSinceDateFormat { get; set; }
		public string ExampleCarrierId { get; private set; }
		public string Host { get; private set; }
		public string Password1 { get; private set; }
		public string Password2 { get; private set; }
		public string Path { get; private set; }
		public int Port { get; private set; }
		public string Url { get; private set; }
		public string User1 { get; private set; }
		public string User2 { get; private set; }

		public bool UserAccountsAreNotDistinct
		{
			get { return User1.Equals(User2, StringComparison.InvariantCultureIgnoreCase); }
		}

		public static ConfiguredProperties Load(string filename)
		{
			string[] lines = File.ReadAllLines(filename);
			return new ConfiguredProperties(lines);
		}

		public DateTime Parse()
		{
			DateTime result;
			DateTime.TryParseExact(DobCarrierExampleSinceDateRawString.Trim(),
				DobCarrierExampleSinceDateFormat,
				null,
				DateTimeStyles.AdjustToUniversal,
				out result);
			return result;
		}

		private void SetHost(string host)
		{
			const string localhost = "localhost";
			if (localhost.Equals(host, StringComparison.InvariantCultureIgnoreCase))
			{
				throw new ArgumentOutOfRangeException("host",
					string.Format("Cannot be '{0}'; that is not reachable from the TransCore servers.", localhost));
			}
			Host = host;
		}
	}
}
