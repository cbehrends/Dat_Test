#region usings
using System;
using QuickStart.Infrastructure;
#endregion

namespace QuickStart
{
	public class LookupSignedCarriers : Program
	{
		public LookupSignedCarriers(ConfiguredProperties properties)
			: base(properties) {}

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
				session.LookupSignedCarriers();
				result = Result.Success;
			}
			return result;
		}
	}
}
