#region usings
using QuickStart.Infrastructure;
#endregion

namespace QuickStart.DisplayHelpers
{
	public class LookupDobEventsDisplayHelper : DisplayHelper<LookupDobEventsSuccessData>
	{
		private readonly SessionFacade _session;

		public LookupDobEventsDisplayHelper(LookupDobEventsSuccessData data, SessionFacade session)
			: base(data)
		{
			_session = session;
		}

		protected override void Display(int indent)
		{
			DisplayLabel("LookupEventsResult", indent);
			Display(Data.dobEvents, indent + 1);
		}

		private void Display(DobEvent[] events, int indent)
		{
			if (events == null)
			{
				return;
			}
			foreach (DobEvent dobEvent in events)
			{
				if (dobEvent.carrierId != null)
				{
					DisplayValue("Carrier id", dobEvent.carrierId, indent);
					_session.LookupDobCarriersByCarrierId(dobEvent.carrierId, indent + 1);
				}

				EventTypes[] types = dobEvent.eventTypes;
				if (types != null)
				{
					foreach (EventTypes eventType in types)
					{
						DisplayValue("Event Types", eventType, indent);
					}
				}

				SignedContract[] signedContracts = dobEvent.signedContracts;
				if (signedContracts != null)
				{
					foreach (SignedContract signedContract in signedContracts)
					{
						DisplayValue("Signed Contracts", signedContract, indent + 1);
					}
				}
			}
		}
	}
}
