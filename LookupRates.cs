using System;
using QuickStart.Infrastructure;

namespace QuickStart
{
	public class LookupRates : Program
	{
		public LookupRates(ConfiguredProperties properties) : base(properties) {}

		protected override int Execute(DateTime when)
		{
			int result;

			SessionFacade session;
			if (Account1FailsLogin(out session))
			{
				result = Result.Invalid;
			}
			else
			{
				session.LookupCurrentRate();
				session.LookupHistoricContract();
				session.LookupHistoricSpot();
				result = Result.Success;
			}
			return result;
		}
	}
}