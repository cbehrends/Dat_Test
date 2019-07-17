using System;
using QuickStart.Infrastructure;

namespace QuickStart
{
	public class Post : Program
	{
		public Post(ConfiguredProperties properties) : base(properties) {}

		protected override int Execute(DateTime when)
		{
			int result;
			PostAssetRequest sampleLoad = SampleFactory.BuildLoad(when);

			SessionFacade session;
			if (Account1FailsLogin(out session))
			{
				result = Result.Failure;
			}
			else
			{
				session.DeleteAllAssets();
				session.Post(sampleLoad);
				result = Result.Success;
			}
			return result;
		}
	}
}