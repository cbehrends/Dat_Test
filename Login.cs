using System;
using QuickStart.Infrastructure;

namespace QuickStart
{
	public class Login : Program
	{
		public Login(ConfiguredProperties properties) : base(properties) {}

		protected override int Execute(DateTime when)
		{
			SessionFacade session;
			if (Account1FailsLogin(out session))
			{
				return Result.Failure;
			}
			Console.WriteLine("Login successful.");
			return Result.Success;
		}
	}
}