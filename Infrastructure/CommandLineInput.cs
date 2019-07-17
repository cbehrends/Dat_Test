using System;
using System.Collections.Generic;
using System.Linq;

namespace QuickStart.Infrastructure
{
	/// <summary>
	/// Helper class to parse the command-line parameters to the executable and to explain usage.
	/// </summary>
	public class CommandLineInput
	{
		private const string DefaultOutputFilename = @"./ConnexionTest.txt";
		private const string DefaultPropertiesFilename = @"./login.properties";
		private const int Feature = 0;
		private const string OutputFileToken = "-o:";
		private const string PropertiesFileToken = "-p:";

		private readonly string[] _args;
		private readonly Dictionary<string, string> _argumentsDictionary;

		public CommandLineInput(string[] args)
		{
			_args = args;
			_argumentsDictionary = new Dictionary<string, string>
			                       {
			                       	{OutputFileToken, DefaultOutputFilename},
			                       	{PropertiesFileToken, DefaultPropertiesFilename}
			                       };
		}

		public Feature? Parse()
		{
			if (_args.Length == 0)
			{
				return null;
			}
			if (_args.Skip(1).Any(CannotParseArgument))
			{
				return null;
			}
			Feature? feature;
			if (IsFeature(_args[Feature], out feature))
			{
				return feature;
			}
			return null;
		}

		public string OutputFilename
		{
			get { return _argumentsDictionary[OutputFileToken]; }
		}

		public string PropertiesFilename
		{
			get { return _argumentsDictionary[PropertiesFileToken]; }
		}

		public static int ShowUsage()
		{
			Console.WriteLine("Usage:");
			Console.WriteLine("<app.exe> <feature> [{0}filename1] [{1}filename2]", OutputFileToken, PropertiesFileToken);
			Console.WriteLine("...where");
			Console.WriteLine();
			Console.WriteLine("  <feature> is one of the folowing:");
			string[] names = Enum.GetNames(typeof(Feature));
			Array values = Enum.GetValues(typeof(Feature));
			for (int i = 0; i < names.Length; i++)
			{
				Console.WriteLine("    {0} for {1}", (int) values.GetValue(i), names[i]);
			}
			Console.WriteLine();
			Console.WriteLine("  {0} specifies an output file, whose contents will be overwritten.", OutputFileToken);
			Console.WriteLine("    The default filename is '{0}'.", DefaultOutputFilename);
			Console.WriteLine();
			Console.WriteLine("  {0} specifies a 'properties' file.", PropertiesFileToken);
			Console.WriteLine("    The default filename is '{0}'.", DefaultPropertiesFilename);
			return -1;
		}

		private static bool IsFeature(string s, out Feature? feature)
		{
			try
			{
				feature = (Feature) Enum.Parse(typeof(Feature), s);
				return true;
			}
			catch (ArgumentException) {}
			catch (OverflowException) {}
			feature = null;
			return false;
		}

		private bool CannotParseArgument(string argument)
		{
			foreach (string token in new[] {OutputFileToken, PropertiesFileToken})
			{
				if (argument.StartsWith(token))
				{
					_argumentsDictionary[token] = argument.Substring(token.Length);
					return false;
				}
			}
			return true;
		}
	}
}