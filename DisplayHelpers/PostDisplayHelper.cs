namespace QuickStart.DisplayHelpers
{
	public class PostDisplayHelper : DisplayHelper<PostAssetSuccessData>
	{
		public PostDisplayHelper(PostAssetSuccessData data) : base(data) {}

		protected override void Display(int indent)
		{
			DisplayLabel("Post Asset Success Data", indent);
			Display(Data.asset, indent + 1);
			Display(Data.alarm, indent + 1);
		}

		private static void Display(global::Alarm data, int indent)
		{
			if (data == null)
			{
				return;
			}

			DisplayLabel("Alarm", indent);
			DisplayValue("Alarm Id", data.alarmId, indent + 1);
			Display(data.alarmCriteria, indent + 1);
			DisplayValue("Basis Asset Id", data.basisAssetId, indent + 1);
			DisplayValue("Basis Asset Posters Reference Id", data.basisAssetPostersReferenceId, indent + 1);
			DisplayValue("Matches Remaining", data.matchesRemaining, indent + 1);
			if (data.matchingAssetIds != null)
			{
				foreach (string id in data.matchingAssetIds)
				{
					DisplayValue("Matching Asset Id", id, indent + 1);
				}
				DisplayValue("Max Matches", data.maxMatches, indent + 1);
			}
		}

		private static void Display(AlarmSearchCriteria data, int indent)
		{
			if (data == null)
			{
				return;
			}

			DisplayLabel("Alarm Search Criteria", indent);
			if (data.ageLimitMinutesSpecified)
			{
				DisplayValue("Age Limit Minutes", data.ageLimitMinutes, indent + 1);
			}
			if (data.lifetimeMinutesSpecified)
			{
				DisplayValue("Lifetime Minutes Specified", data.lifetimeMinutes, indent + 1);
			}
			if (data.maxMatchesSpecified)
			{
				DisplayValue("Max Matches Specified", data.maxMatches, indent + 1);
			}
			if (data.notifyAllApplicationsSpecified)
			{
				DisplayValue("Notify All Applications Specified", data.notifyAllApplications, indent + 1);
			}
			Display(data.destinationRadius, indent + 1, "Destination Radius");
			Display(data.originRadius, indent + 1, "Origin Radius");
			DisplayValue("Reference Id", data.referenceId, indent + 1);
		}
	}
}