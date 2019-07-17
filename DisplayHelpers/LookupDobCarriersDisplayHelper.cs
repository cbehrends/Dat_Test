namespace QuickStart.DisplayHelpers
{
	public class LookupDobCarriersDisplayHelper : DisplayHelper<LookupDobCarriersSuccessData>
	{
		public LookupDobCarriersDisplayHelper(LookupDobCarriersSuccessData data, int indent)
			: base(data, indent) {}

		protected override void Display(int indent)
		{
			DisplayLabel("Lookup Dob Carriers Success Data", indent);
			Display(Data.dobCarrier, indent + 1);
		}

		private void Display(DobCarrier dobCarrier, int indent)
		{
			if (dobCarrier == null)
			{
				return;
			}
			DisplayLabel("DobCarrier info", indent);

			CarrierInfo carrierInfo = dobCarrier.carrierInfo;
			if (carrierInfo != null)
			{
				DisplayValue("DBA name", carrierInfo.dbaName, indent + 1);
				if (carrierInfo.docket != null)
				{
					DisplayValue("Docket name", carrierInfo.docket.prefix + carrierInfo.docket.number, indent + 1);
				}
				if (carrierInfo.dotNumberSpecified && carrierInfo.dotNumber > 0)
				{
					DisplayValue("Dot Number", carrierInfo.dotNumber, indent);
				}
				DisplayValue("Duns", carrierInfo.duns, indent + 1);
				IntrastateType intrastate = carrierInfo.intrastate;
				if (intrastate != null)
				{
					DisplayValue("Intrastate", intrastate.intrastateState.ToString() + intrastate.intrastateCode, indent + 1);
				}
				DisplayValue("Carrier Info Last Update", carrierInfo.lastUpdate, indent + 1);
				DisplayValue("Legal Name", carrierInfo.legalName, indent + 1);

				Display("Mailing address", carrierInfo.mailingAddress, indent + 1);
				Display("Physical address", carrierInfo.physicalAddress, indent + 1);
				if (carrierInfo.scacCode != null)
				{
					foreach (string code in carrierInfo.scacCode)
					{
						DisplayValue("Scacc Code", code, indent + 1);
					}
				}
				DisplayValue("Website", carrierInfo.website, indent + 1);
			}
			ContactInfo contactInfo = dobCarrier.contactInfo;
			if (contactInfo.contacts != null)
			{
				foreach (Contact contact in contactInfo.contacts)
				{
					Display(contact, indent + 1);
				}
			}
			DisplayValue("Contact Info Last Update", contactInfo.lastUpdate, indent + 1);

			Document[] documents = dobCarrier.documents;
			if (documents != null)
			{
				foreach (Document document in documents)
				{
					Display(document, indent + 1);
				}
			}

			Display(dobCarrier.equipment, indent + 1);

			GeographicCoverage geographicCoverage = dobCarrier.geographicCoverage;
			if (geographicCoverage != null)
			{
				LaneType[] lanes = geographicCoverage.lanes;
				if (lanes != null)
				{
					foreach (LaneType laneType in lanes)
					{
						Display(laneType, indent + 1);
					}
				}
				DisplayValue("Last Geographic Coverage Update Date", geographicCoverage.lastUpdate, indent + 1);
				if (geographicCoverage.operatingStateProvince != null)
				{
					foreach (StateProvince stateProvince in geographicCoverage.operatingStateProvince)
					{
						DisplayValue("Geographic Coverage operation state province", stateProvince, indent + 1);
					}
				}
				DisplayValue("Geographic Coverage service in mexico", geographicCoverage.serviceInMexico, indent + 1);
			}
		}

		private void Display(LaneType laneType, int indent)
		{
			DisplayLabel("Lane Type", indent);
			DisplayValue("Origin state", laneType.origin.stateProvince, indent + 1);
			DisplayValue("Origin metro area", laneType.origin.metroArea, indent + 1);
			DisplayValue("Destination state", laneType.destination.stateProvince, indent + 1);
			DisplayValue("Destination metro area", laneType.destination.metroArea, indent + 1);
		}

		private void Display(Contact contact, int indent)
		{
			DisplayLabel("Contact", indent);
			DisplayValue("Email", contact.email, indent + 1);
			Display(contact.fax, indent + 1, "Fax");
			DisplayValue("Is primary contact", contact.isPrimaryContact, indent + 1);
			DisplayValue("Name", contact.name, indent + 1);
			Display(contact.phone, indent + 1, "Phone");
			DisplayValue("Role", contact.role, indent + 1);
			DisplayValue("Title", contact.title, indent + 1);
		}

		private void Display(Document document, int indent)
		{
			DisplayLabel("Document", indent);
			DisplayValue("Created Date", document.created, indent + 1);
			DisplayValue("Description", document.description, indent + 1);
			DisplayValue("Doc Name", document.documentName, indent + 1);
			DisplayValue("Type", document.documentType, indent + 1);
			DisplayValue("File Ext", document.fileExtension, indent + 1);
			DisplayValue("File Name", document.fileName, indent + 1);
			DisplayValue("Url", document.url, indent + 1);
		}

		private void Display(Equipment1 equipment, int indent)
		{
			if (equipment == null)
			{
				return;
			}
			DisplayLabel("Equipment", indent);
			DisplayValue("Has air ride", equipment.hasAirRide, indent + 1);
			DisplayValue("Has chains", equipment.hasChains, indent + 1);
			DisplayValue("Has coil racks", equipment.hasCoilRacks, indent + 1);
			DisplayValue("Has conestoga", equipment.hasConestoga, indent + 1);
			DisplayValue("Has container locks", equipment.hasContainerLocks, indent + 1);
			DisplayValue("Has curtains", equipment.hasCurtains, indent + 1);
			DisplayValue("Has etrac", equipment.hasEtrac, indent + 1);
			DisplayValue("Has garment racks", equipment.hasGarmentRacks, indent + 1);
			DisplayValue("Has hot shot", equipment.hasHotshot, indent + 1);
			DisplayValue("Has insulated", equipment.hasInsulated, indent + 1);
			DisplayValue("Has pad wrap", equipment.hasPadWrap, indent + 1);
			DisplayValue("Has straps", equipment.hasStraps, indent + 1);
			DisplayValue("Has tarps", equipment.hasTarps, indent + 1);
			DisplayValue("Has vented", equipment.hasVented, indent + 1);

			DisplayValue("Number of Company Drivers", equipment.numberOfCompanyDrivers, indent + 1);
			DisplayValue("Number of Owner Operators", equipment.numberOfOwnerOperators, indent + 1);
			DisplayValue("Number of PowerUnits", equipment.numberOfPowerUnits, indent + 1);
			DisplayValue("Number of Teams", equipment.numberOfTeams, indent + 1);

			DisplayValue("On board communications", equipment.onBoardCommunications, indent + 1);

			Trailer[] trailers = equipment.trailers;
			if (trailers != null)
			{
				foreach (Trailer trailer in trailers)
				{
					DisplayValue("Number of trailers", trailer.numberOf, indent + 1);
					DisplayValue("Trailer type", trailer.trailerType, indent + 1);
				}
			}
		}
	}
}
