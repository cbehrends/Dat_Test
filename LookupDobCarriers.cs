#region usings
using System;
using QuickStart.Infrastructure;
#endregion

namespace QuickStart
{
	public class LookupDobCarriers : Program
	{
		private readonly string _exampleCarrierId;

		public LookupDobCarriers(ConfiguredProperties properties)
			: base(properties)
		{
			_exampleCarrierId = properties.ExampleCarrierId;
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
				session.LookupDobCarriersByCarrierId(_exampleCarrierId);
				result = Result.Success;
			}
			return result;
		}
	}
}
