namespace QuickStart.DisplayHelpers
{
	public class ServiceErrorDisplayHelper : DisplayHelper<ServiceError>
	{
		public ServiceErrorDisplayHelper(ServiceError data) : base(data) {}

		protected override void Display(int indent)
		{
			DisplayLabel("Service Error", indent);
			DisplayValue("Name", Data.name, indent + 1);
			DisplayValue("Message", Data.message, indent + 1);
			DisplayValue("Detailed Message", Data.detailedMessage, indent + 1);
			DisplayValue("Code", Data.code, indent + 1);
			if (Data.faultCodeSpecified)
			{
				DisplayValue("Fault Code", Data.faultCode, indent + 1);
			}
		}

		public void DisplayAt(int indent)
		{
			Display(indent);
		}
	}
}