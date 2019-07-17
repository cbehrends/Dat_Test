#region usings
using QuickStart.Infrastructure;
#endregion

namespace QuickStart.DisplayHelpers
{
	public class LookupDobSignedCarriersDisplayHelper : DisplayHelper<LookupDobSignedCarriersSuccessData>
	{
		private readonly SessionFacade _session;

		public LookupDobSignedCarriersDisplayHelper(LookupDobSignedCarriersSuccessData data, SessionFacade session)
			: base(data)
		{
			_session = session;
		}

		protected override void Display(int indent)
		{
			DisplayLabel("Lookup Signed Carrier Success Data", indent);
			if (Data.signedCarriers != null)
			{
				foreach (SignedCarrier signedCarrier in Data.signedCarriers)
				{
					Display(signedCarrier, indent + 1);
				}
			}
		}

		private void Display(SignedCarrier signedCarrier, int indent)
		{
			DisplayValue("carrier id", signedCarrier.carrierId, indent);
			DisplayValue("endpointUrl", _session.EndpointUrl.AbsoluteUri, indent);
			_session.LookupDobCarriersByCarrierId(signedCarrier.carrierId, indent);
			if (signedCarrier.signedContracts != null)
			{
				foreach (SignedContract signedContract in signedCarrier.signedContracts)
				{
					Display(signedContract, indent);
				}
			}
		}

		private void Display(SignedContract signedContract, int indent)
		{
			DisplayLabel("Signed contract", indent);
			Contract contract = signedContract.contract;
			if (contract != null)
			{
				DisplayValue("Description", contract.description, indent + 1);
				DisplayValue("Document Status", contract.documentStatus.ToString(), indent + 1);
				DisplayValue("Name", contract.name, indent + 1);
				DisplayValue("Version", contract.version, indent + 1);
			}
			if (signedContract.signatory != null)
			{
				DisplayLabel("Signatory", indent);
				DisplayValue("Name", signedContract.signatory.name, indent + 1);
				DisplayValue("Title", signedContract.signatory.title, indent + 1);
			}
			if (signedContract.signedSpecified)
			{
				DisplayValue("Contract Signed Date", signedContract.signed, indent + 1);
			}
		}
	}
}
