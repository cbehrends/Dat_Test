namespace QuickStart.DisplayHelpers
{
	public class UpdateAlarmUrlDisplayHelper : DisplayHelper<UpdateAlarmUrlSuccessData>
	{
		private readonly string _alarmUrl;

		public UpdateAlarmUrlDisplayHelper(UpdateAlarmUrlSuccessData data, string alarmUrl) : base(data)
		{
			_alarmUrl = alarmUrl;
		}

		protected override void Display(int indent)
		{
			DisplayLabel("Update Alarm Url", indent);
			DisplayValue("Url", _alarmUrl, indent + 1);
		}
	}
}