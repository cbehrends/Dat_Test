namespace QuickStart.DisplayHelpers
{
	public class SearchDisplayHelper : DisplayHelper<CreateSearchSuccessData>
	{
		public SearchDisplayHelper(CreateSearchSuccessData data) : base(data) {}

		protected override void Display(int indent)
		{
			DisplayLabel("Create Search Success Data", indent);
			DisplayValue("Total Matches", Data.totalMatches, indent + 1);
			Display(Data.search, indent + 1);
			if (Data.matches != null)
			{
				for (int i = 0; i < Data.matches.Length; i++)
				{
					int oneBasedIndex = i + 1;
					Display(Data.matches[i], indent + 1, oneBasedIndex);
				}
			}
			if (Data.remainingMatchingIds != null)
			{
				foreach (string id in Data.remainingMatchingIds)
				{
					DisplayValue("Remaining Matching Id", id, indent + 1);
				}
			}
		}

		private static void Display(global::Search data, int indent)
		{
			if (data == null)
			{
				return;
			}

			DisplayLabel("Search", indent);
			DisplayValue("Search Id", data.searchId, indent + 1);
			Display(data.specification, indent + 1);
			Display(data.status, indent + 1);
		}

		private static void Display(MatchingAsset data, int indent, int oneBasedIndex)
		{
			if (data == null)
			{
				return;
			}

			DisplayLabel("Matching Asset #" + oneBasedIndex, indent);
			Display(data.asset, indent + 1);
			Display(data.callback, indent + 1);
			Display(data.creditScore, indent + 1);
			Display(data.destinationDeadhead, indent + 1, "Destination Deadhead");
			if (data.dotIds != null)
			{
				foreach (DotIds dotId in data.dotIds)
				{
					Display(dotId, indent + 1);
				}
			}
			Display(data.originDeadhead, indent + 1, "Origin Deadhead");
			Display(data.thirdPartyInfo, indent + 1);
			Display(data.tripMileage, indent + 1, "Trip Mileage");
		}

		private static void Display(DotIds data, int indent)
		{
			if (data == null)
			{
				return;
			}

			DisplayLabel("DotIds", indent);
			if (data.brokerMcNumberSpecified)
			{
				DisplayValue("Broker MC Number", data.brokerMcNumber, indent + 1);
			}
			if (data.carrierMcNumberSpecified)
			{
				DisplayValue("Carrier MC Number", data.carrierMcNumber, indent + 1);
			}
			if (data.dotNumberSpecified)
			{
				DisplayValue("DOT Number", data.dotNumber, indent + 1);
			}
			if (data.freightForwarderMcNumberSpecified)
			{
				DisplayValue("FF Number", data.freightForwarderMcNumber, indent + 1);
			}
			if (data.mexicoMcNumberSpecified)
			{
				DisplayValue("Mexico MC Number", data.mexicoMcNumber, indent + 1);
			}
		}

		private static void Display(ThirdPartyInfo data, int indent)
		{
			if (data == null)
			{
				return;
			}

			if (data.assurableSpecified)
			{
				DisplayValue("Assurable", data.assurable, indent);
			}
			if (data.nmftaMemberSpecified)
			{
				DisplayValue("MNFTA Member", data.nmftaMember, indent);
			}
			if (data.ooidaMemberSpecified)
			{
				DisplayValue("OOIDA Member", data.ooidaMember, indent);
			}
			if (data.rivieraGreenLightSpecified)
			{
				DisplayValue("Riviera Green Light", data.rivieraGreenLight, indent);
			}
			if (data.rmisGreenLightSpecified)
			{
				DisplayValue("RMIS Green Light", data.rmisGreenLight, indent);
			}
		}

		private static void Display(CreditScoreInfo data, int indent)
		{
			if (data == null)
			{
				return;
			}

			DisplayLabel("Credit Score Info", indent);
			DisplayValue("Days to Pay", data.daysToPay, indent + 1);
			DisplayValue("Score", data.score, indent + 1);
			DisplayValue("Score TimeStamp", data.scoreTimeStamp, indent + 1);
		}

		private static void Display(PostingCallback data, int indent)
		{
			if (data == null)
			{
				return;
			}

			DisplayLabel("Posting Callback", indent);
			DisplayValue("Company Name", data.companyName, indent + 1);
			DisplayValue("Display Company", data.displayCompany, indent + 1);
			Display(data.name, indent + 1);
			if (data.Item is CallbackPhoneNumber)
			{
				var d = data.Item as CallbackPhoneNumber;
				Display(d, indent + 1);
			}
			if (data.Item is CallbackEmailAddress)
			{
				var d = data.Item as CallbackEmailAddress;
				Display(d, indent + 1);
			}
			DisplayValue("Posters State Province", data.postersStateProvince, indent + 1);
			DisplayValue("User Id", data.userId, indent + 1);
		}

		private static void Display(CallbackEmailAddress data, int indent)
		{
			if (data == null)
			{
				return;
			}

			DisplayLabel("Callback Email Address", indent + 1);
			DisplayValue("Email", data.email, indent + 1);
		}

		private static void Display(CallbackPhoneNumber data, int indent)
		{
			if (data == null)
			{
				return;
			}

			DisplayLabel("Callback Phone Number", indent);
			Display(data.phone, indent + 1);
		}

		private static void Display(PersonName data, int indent)
		{
			if (data == null)
			{
				return;
			}

			DisplayLabel("Name", indent);
			DisplayValue("First", data.firstName, indent + 1);
			DisplayValue("Middle", data.middleName, indent + 1);
			DisplayValue("Last", data.lastName, indent + 1);
			DisplayValue("Initials", data.initials, indent + 1);
			DisplayValue("Prefix", data.prefix, indent + 1);
			DisplayValue("Suffix", data.suffix, indent + 1);
			DisplayValue("Title", data.title, indent + 1);
		}

		private static void Display(SearchCriteria data, int indent)
		{
			if (data == null)
			{
				return;
			}

			DisplayLabel("Search Criteria", indent);
			Display(data.availability, indent + 1);
			if (data.ageLimitMinutesSpecified)
			{
				DisplayValue("Age Limit Minutes", data.ageLimitMinutes, indent + 1);
			}
			DisplayValue("Asset Type", data.assetType, indent + 1);
			Display(data.destination, indent + 1);
			if (data.equipmentClasses != null)
			{
				foreach (EquipmentClass eq in data.equipmentClasses)
				{
					DisplayValue("Equipment Class", eq, indent + 1);
				}
			}
			if (data.includeLtlsSpecified)
			{
				DisplayValue("Include LTLs", data.includeLtls, indent + 1);
			}
			if (data.includeFullsSpecified)
			{
				DisplayValue("Include Fulls", data.includeFulls, indent + 1);
			}
			Display(data.limits, indent + 1);
			Display(data.origin, indent + 1);
		}

		private static void Display(GeoCriteria data, int indent)
		{
			if (data == null)
			{
				return;
			}

			DisplayLabel("Geo Criteria", indent);
			if (data.Item is SearchArea)
			{
				var d = data.Item as SearchArea;
				Display(d, indent + 1);
			}
			if (data.Item is SearchRadius)
			{
				var d = data.Item as SearchRadius;
				Display(d, indent + 1);
			}
			if (data.Item is SearchOpen)
			{
				var d = data.Item as SearchOpen;
				Display(d, indent + 1);
			}
		}

		private static void Display(SearchOpen data, int indent)
		{
			if (data == null)
			{
				return;
			}
			DisplayLabel("Search Open", indent);
		}

		private static void Display(SearchRadius data, int indent)
		{
			if (data == null)
			{
				return;
			}
			DisplayLabel("Search Radius", indent);
			Display(data.place, indent + 1);
			Display(data.radius, indent + 1, "Radius");
		}

		private static void Display(SearchArea data, int indent)
		{
			if (data == null)
			{
				return;
			}
			DisplayLabel("Search Area", indent);
			if (data.zones != null)
			{
				foreach (Zone zone in data.zones)
				{
					DisplayValue("Zone", zone, indent + 1);
				}
			}
			if (data.stateProvinces != null)
			{
				foreach (StateProvince stateProvince in data.stateProvinces)
				{
					DisplayValue("State/Province", stateProvince, indent + 1);
				}
			}
		}
	}
}