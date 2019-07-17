#region usings
using System;
using QuickStart.Infrastructure;
#endregion

namespace QuickStart
{
	public class LookupDobEvents : Program
	{
		private readonly DateTime _exampleSinceDate;

		public LookupDobEvents(ConfiguredProperties properties)
			: base(properties)
		{
			_exampleSinceDate = properties.DobCarrierExampleSinceDate;
		}

		protected override int Execute(DateTime when)
		{
			int result;

			SessionFacade session;
			if (DobAccountFailsLogin(out session))
			{
				result = Result.Invalid;
			}
			else
			{
				session.LookupDobEvents(_exampleSinceDate);
				result = Result.Success;
			}
			return result;
		}
	}
}
