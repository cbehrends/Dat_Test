namespace QuickStart.DisplayHelpers
{
	public class LookupCarrierDisplayHelper : DisplayHelper<LookupCarrierSuccessData>
	{
		public LookupCarrierDisplayHelper(LookupCarrierSuccessData data) : base(data) {}

		protected override void Display(int indent)
		{
			DisplayLabel("Lookup Carrier Success Data", indent);
			Display(Data.header, indent + 1);
			Display(Data.dotProfile, indent + 1);
			Display(Data.dotAuthority, indent + 1);
			Display(Data.dotInsurance, indent + 1);
			Display(Data.safestat, indent + 1);
			Display(Data.safetyRating, indent + 1);
			Display(Data.fmcsaInspections, indent + 1);
			Display(Data.fmcsaCrashes, indent + 1);
			Display(Data.csa2010SafetyFitness, indent + 1);
			Display(Data.csa2010Basics, indent + 1);
			Display(Data.extendedProfile, indent + 1);
		}

		private static void Display(ExtendedProfile profile, int indent)
		{
			if (profile == null)
			{
				return;
			}
			DisplayLabel("Extended Profile", indent);
			DisplayValue("Last Update", profile.lastUpdateOfExtendedProfile, indent + 1);
			Display(profile.companyBackground, indent + 1);
			Display(profile.fleet, indent + 1);
			Display(profile.services, indent + 1);
			Display(profile.territory, indent + 1);
		}

		private static void Display(TerritoryType territory, int indent)
		{
			if (territory == null)
			{
				return;
			}
			DisplayLabel("Territory", indent);
			{
				// Optional elements, but default to false if omitted, as described in schema documentation
				DisplayValue("CSA Approved", territory.isCsaApproved, indent + 1);
				DisplayValue("FAST certified", territory.isFastCertified, indent + 1);
				DisplayValue("Provides service to Mexico", territory.serviceToMexico, indent + 1);
			}
			DisplayValues("Service to", territory.statesProvinces, indent + 1);
			if (territory.lane != null)
			{
				foreach (LaneType lane in territory.lane)
				{
					Display(lane, indent + 1);
				}
			}
		}

		private static void Display(LaneType lane, int indent)
		{
			DisplayValue("Operates on", Format(lane.origin) + " -> " + Format(lane.destination), indent);
		}

		private static string Format(EndPoint endPoint)
		{
			EndPointMetroArea metroArea = endPoint.metroArea;
			string stateProvince = endPoint.stateProvince.ToString();
			if (metroArea != null)
			{
				return string.Format("{0}, {1} ({2})", metroArea.name, stateProvince, metroArea.code);
			}
			return stateProvince;
		}

		private static void Display(ExtendedProfileServices services, int indent)
		{
			if (services == null)
			{
				return;
			}
			DisplayLabel("Services", indent);
			if (services.specialServices != null)
			{
				foreach (string specialService in services.specialServices)
				{
					DisplayValue("Special service", specialService, indent + 1);
				}
			}
			{
				// Optional elements, but default to false if omitted, as described in schema documentation
				DisplayValue("Handles overweight loads", services.handlesOverweightLoads, indent + 1);
				DisplayValue("Handles long loads", services.handlesLongLoads, indent + 1);
				DisplayValue("Handles wide loads", services.handlesWideLoads, indent + 1);
				DisplayValue("Handles EDI", services.handlesEdi, indent + 1);
				DisplayValue("Responsible care", services.isResponsibleCare, indent + 1);
				DisplayValue("Has garment hanging", services.hasGarmentHanging, indent + 1);
				DisplayValue("Has spotted trailers", services.hasSpottedTrailers, indent + 1);
				DisplayValue("Does trailer exchange", services.doesTrailerExchange, indent + 1);
				DisplayValue("Does warehousing", services.doesWarehousing, indent + 1);
				DisplayValue("Does LTL", services.doesLTL, indent + 1);
				DisplayValue("Does multi-stops", services.doesMultiStops, indent + 1);
				DisplayValue("Does parcels", services.doesParcels, indent + 1);
				DisplayValue("Carries alchohol", services.carriesAlcohol, indent + 1);
				DisplayValue("Carries cigarettes", services.carriesCigarettes, indent + 1);
				DisplayValue("Carries ammunition or explosives", services.carriesAmmunitionExplosives, indent + 1);
				DisplayValue("Carries cosmetics", services.carriesCosmetics, indent + 1);
				DisplayValue("Carries garments", services.carriesGarments, indent + 1);
				DisplayValue("Carries hazmat", services.carriesHazmat, indent + 1);
			}
		}

		private static void Display(ExtendedProfileCompanyBackground background, int indent)
		{
			if (background == null)
			{
				return;
			}
			DisplayLabel("Company Background", indent);
			DisplayValue("Principle", background.principle, indent + 1);
			DisplayValue("Principal title", background.principalTitle, indent + 1);
			DisplayValue("Primary contact", background.primaryContact, indent + 1);
			Display(background.phone, indent + 1, "Phone");
			Display(background.fax, indent + 1, "Fax");
			DisplayValue("Email", background.emailAddress, indent + 1);
			DisplayValue("Website", background.website, indent + 1);
			DisplayValue("Year founded", background.yearFounded, background.yearFoundedSpecified, indent + 1);

			{
				// Optional elements, but default to false if omitted, as described in schema documentation
				DisplayValue("Woman owned", background.isWomanOwned, indent + 1);
				DisplayValue("Minority owned", background.isMinorityOwned, indent + 1);
			}
		}

		private static void Display(ExtendedProfileFleet fleet, int indent)
		{
			if (fleet == null)
			{
				return;
			}
			DisplayLabel("Fleet", indent);
			DisplayValue("Satellite tracking", fleet.satelliteTracking, indent + 1);

			{
				// Optional elements, but default to false if omitted, as described in schema documentation
				DisplayValue("Number of teams", fleet.numberOfTeams, indent + 1);
				DisplayValue("Number of teams", fleet.numberOfTeams, indent + 1);
				DisplayValue("Has extra-wide vans", fleet.hasXWideVans, indent + 1);
				DisplayValue("Has extra-long vans", fleet.hasXLongVans, indent + 1);
				DisplayValue("Has extra-long reefers", fleet.hasXLongReefers, indent + 1);
				DisplayValue("Has freezer reefers", fleet.hasFreezerReefers, indent + 1);
				DisplayValue("Has Refrigerated only reefers", fleet.hasRefrigeratedOnlyReefers, indent + 1);
				DisplayValue("Insurance covers all drivers", fleet.insuranceCoversAllDrivers, indent + 1);
				DisplayValue("Insurance covers all trucks", fleet.insuranceCoversAllTrucks, indent + 1);
			}

			if (fleet.truck != null)
			{
				foreach (ExtendedProfileFleetTruck truck in fleet.truck)
				{
					Display(truck, indent + 1);
				}
			}
		}

		private static void Display(ExtendedProfileFleetTruck truck, int indent)
		{
			if (truck == null)
			{
				return;
			}
			DisplayLabel("Truck", indent);
			DisplayValue("Code", truck.code, indent + 1);
			DisplayValue("Type", truck.type, indent + 1);
			DisplayValue("Number of", truck.numberOf, indent + 1);
		}

		private static void Display(Csa2010BasicMeasurements measurements, int indent)
		{
			if (measurements == null)
			{
				return;
			}
			DisplayLabel("CSA Basics", indent);
			DisplayValue("Last Update", measurements.lastUpdateOfCsa2010Basics, indent + 1);
			DisplayValue("Insurance or other violation",
			             measurements.insuranceOrOtherViolation,
			             measurements.insuranceOrOtherViolationSpecified,
			             indent + 1);
			DisplayValue("Insurance or other indicator",
			             measurements.insuranceOrOtherIndicator,
			             measurements.insuranceOrOtherIndicatorSpecified,
			             indent + 1);
			for (int i = 0; i < measurements.measurement.Length; i++)
			{
				int oneBasedIndex = i + 1;
				Display(measurements.measurement[i], indent + 1, oneBasedIndex);
			}
		}

		private static void Display(Csa2010BasicMeasurementsMeasurement measurement, int indent, int oneBasedIndex)
		{
			if (measurement == null)
			{
				return;
			}
			DisplayLabel("Measurement #" + oneBasedIndex, indent);
			DisplayValue("Basic", measurement.basic, indent + 1);
			DisplayValue("Measure", measurement.measure, measurement.measureSpecified, indent + 1, "0.0");
			DisplayValue("Percentile", measurement.percentile, measurement.percentileSpecified, indent + 1, "0.0");
			DisplayValue("Number of inspections",
			             measurement.numberOfInspections,
			             measurement.numberOfInspectionsSpecified,
			             indent + 1);
			DisplayValue("Roadside alert", measurement.roadsideAlert, indent + 1);
			DisplayValue("Serious violation indicator", measurement.seriousViolationIndicator, indent + 1);
			DisplayValue("BASIC indicator", measurement.basicIndicator, indent + 1);
		}

		private static void Display(Csa2010SafetyFitness fitness, int indent)
		{
			if (fitness == null)
			{
				return;
			}
			DisplayLabel("CSA Safety Fitness", indent);
			DisplayValue("Last Update", fitness.lastUpdateOfCsa2010SafetyFitness, indent + 1);
			foreach (string activity in fitness.interventionActivity)
			{
				DisplayValue("Intervention activity", activity, indent + 1);
			}
			DisplayValue("Safety fitness determination",
			             fitness.safetyFitnessDetermination,
			             fitness.safetyFitnessDeterminationSpecified,
			             indent + 1);
		}

		private static void Display(FmcsaCrashes crashes, int indent)
		{
			if (crashes == null)
			{
				return;
			}
			DisplayLabel("Inspections", indent);
			DisplayValue("Last Update", crashes.lastUpdateOfCrashes, indent + 1);
			//DisplayValue("Fatal crashes", crashes., indent + 1);
			//DisplayValue("Injury crashes", crashes., indent + 1);
			//DisplayValue("Fatal or injury crashes", crashes., indent + 1);
			DisplayValue("Crashes requiring tow", crashes.crashesTow, crashes.crashesTowSpecified, indent + 1);
			DisplayValue("Crashes involving hazmat", crashes.crashesHazmat, crashes.crashesHazmatSpecified, indent + 1);
		}

		private static void Display(FmcsaInspections inspections, int indent)
		{
			if (inspections == null)
			{
				return;
			}
			DisplayLabel("Inspections", indent);
			DisplayValue("Last Update", inspections.lastUpdateOfInspections, indent + 1);
			DisplayValue("Total inspections",
			             inspections.totalInspections,
			             inspections.totalInspectionsSpecified,
			             indent + 1);
			Display(inspections.vehicleInspections, indent + 1, "Vehicle");
			Display(inspections.driverInspections, indent + 1, "Driver");
			Display(inspections.hazmatInspections, indent + 1, "Hazmat");
		}

		private static void Display(FmcsaInspection inspection, int indent, string prefix)
		{
			if (inspection == null)
			{
				return;
			}
			DisplayValue(prefix + " inspections", inspection.inspections, indent + 1);
			DisplayValue(prefix + " out-of-service", inspection.outOfService, indent + 1);
		}

		private static void Display(FmcsaSafetyRating1 rating, int indent)
		{
			if (rating == null)
			{
				return;
			}
			DisplayLabel("Safety Rating", indent);
			DisplayValue("Last Update", rating.lastUpdateOfSafety, indent + 1);
			DisplayValue("Rating", rating.rating, rating.ratingSpecified, indent + 1);
			DisplayValue("Rating date", rating.ratingDate, rating.ratingDateSpecified, indent + 1);
			DisplayValue("Review type", rating.reviewType, indent + 1);
			DisplayValue("Review date", rating.reviewDate, rating.reviewDateSpecified, indent + 1);
		}


		private static void Display(FmcsaSafeStat1 safeStat, int indent)
		{
			if (safeStat == null)
			{
				return;
			}
			DisplayLabel("SafeStat", indent);
			DisplayValue("Last Update", safeStat.lastUpdateOfSafeStat, indent + 1);
			DisplayValue("Driver", safeStat.seaDriver, safeStat.seaDriverSpecified, indent + 1, "0.##");
			DisplayValue("Vehicle", safeStat.seaVehicle, safeStat.seaVehicleSpecified, indent + 1, "0.##");
			DisplayValue("Management", safeStat.seaManagement, safeStat.seaManagementSpecified, indent + 1, "0.##");
		}

		private static void Display(DotInsuranceType dotInsurance, int indent)
		{
			if (dotInsurance == null)
			{
				return;
			}
			DisplayLabel("DOT Insurance", indent);
			DisplayValue("Last Update", dotInsurance.lastUpdateOfDotInsurance, indent + 1);
			foreach (InsuranceRecord insuranceRecord in dotInsurance.insuranceRecord)
			{
				Display(insuranceRecord, indent + 1);
			}
		}

		private static void Display(InsuranceRecord record, int indent)
		{
			DisplayLabel("Record", indent);
			DisplayValue("Coverage Type", record.coverageType, indent + 1);
			DisplayValue("BIPD Class", record.bipdClass, record.bipdClassSpecified, indent + 1);
			DisplayValue("Form code", record.formCode, indent + 1);
			DisplayValue("Policy number", record.policyNumber, indent + 1);
			DisplayValue("Coverage from", record.coverageFrom, indent + 1);
			DisplayValue("Coverage to", record.coverageTo, indent + 1);
			DisplayValue("Effective date", record.effectiveDate, indent + 1);
			DisplayValue("Canceled date", record.canceledDate, record.canceledDateSpecified, indent + 1);
			Display(record.insuranceCarrier, indent + 1);
		}

		private static void Display(InsuranceRecordInsuranceCarrier carrier, int indent)
		{
			if (carrier == null)
			{
				return;
			}
			DisplayLabel("Insurance Carrier ", indent);
			DisplayValue("ID", carrier.id, indent + 1);
			DisplayValue("Company Name", carrier.companyName, indent + 1);
			DisplayValue("Contact", carrier.contact, indent + 1);
			Display("Location", carrier.location, indent + 1);
		}

		private static void Display(DotAuthority1 authority, int indent)
		{
			if (authority == null)
			{
				return;
			}
			DisplayLabel("DOT Authority", indent);
			DisplayValue("Last Update", authority.lastUpdateOfDotAuthority, indent + 1);
			DisplayValue("Common Authority", authority.commonAuthority, authority.commonAuthoritySpecified, indent + 1);
			DisplayValue("Pending Common Authority",
			             authority.pendingCommonAuthority,
			             authority.pendingCommonAuthoritySpecified,
			             indent + 1);
			DisplayValue("Revoked Common Authority",
			             authority.revokedCommonAuthority,
			             authority.revokedCommonAuthoritySpecified,
			             indent + 1);

			DisplayValue("Contract Authority", authority.contractAuthority, authority.contractAuthoritySpecified, indent + 1);
			DisplayValue("Pending Contract Authority",
			             authority.pendingContractAuthority,
			             authority.pendingContractAuthoritySpecified,
			             indent + 1);
			DisplayValue("Revoked Contract Authority",
			             authority.revokedContractAuthority,
			             authority.revokedContractAuthoritySpecified,
			             indent + 1);

			DisplayValue("Broker Authority", authority.brokerAuthority, authority.brokerAuthoritySpecified, indent + 1);
			DisplayValue("Pending Broker Authority",
			             authority.pendingBrokerAuthority,
			             authority.pendingBrokerAuthoritySpecified,
			             indent + 1);
			DisplayValue("Revoked Broker Authority",
			             authority.revokedBrokerAuthority,
			             authority.revokedBrokerAuthoritySpecified,
			             indent + 1);

			DisplayValue("Carries Freight", authority.carriesFreight, authority.carriesFreightSpecified, indent + 1);
			DisplayValue("Carries Passengers", authority.carriesPassengers, authority.carriesPassengersSpecified, indent + 1);
			DisplayValue("Carries HHG", authority.carriesHhg, authority.carriesHhgSpecified, indent + 1);
			DisplayValue("BIPD Required", authority.bipdRequired, authority.bipdRequiredSpecified, indent + 1);
			DisplayValue("Cargo Required", authority.cargoRequired, authority.cargoRequiredSpecified, indent + 1);
			DisplayValue("Bond Surety Required",
			             authority.bondSuretyRequired,
			             authority.bondSuretyRequiredSpecified,
			             indent + 1);
			DisplayValue("BIPD on file", authority.bondOnFile, authority.bipdOnFileSpecified, indent + 1);
			DisplayValue("Cargo on file", authority.cargoOnFile, authority.cargoOnFileSpecified, indent + 1);
			DisplayValue("Bond on file", authority.bondOnFile, authority.bondOnFileSpecified, indent + 1);
		}

		private static void Display(DotProfile1 dotProfile, int indent)
		{
			if (dotProfile == null)
			{
				return;
			}
			DisplayLabel("DOT Profile", indent);
			DisplayValue("Last Update", dotProfile.lastUpdateOfDotProfile, indent + 1);
			DisplayValue("Active", dotProfile.isActive, indent + 1);
			DisplayValue("Entity Type", dotProfile.entityType, indent + 1);
			DisplayValue("Operation Type", dotProfile.operationType, indent + 1);
			DisplayValue("Out of interstate service",
			             dotProfile.outOfInterstateServiceDate,
			             dotProfile.outOfInterstateServiceDateSpecified,
			             indent + 1);
			DisplayValue("Power Units", dotProfile.powerUnitsSpecified, dotProfile.powerUnitsSpecified, indent + 1);
			DisplayValue("Drivers", dotProfile.drivers, dotProfile.driversSpecified, indent + 1);
			Display(dotProfile.mcs150Mileage, indent + 1);
			if (dotProfile.commodities != null)
			{
				foreach (CommodityType1 commodity in dotProfile.commodities)
				{
					DisplayValue("Commodity", commodity, indent + 1);
				}
			}
			if (dotProfile.specialCommodities != null)
			{
				foreach (string specialCommodity in dotProfile.specialCommodities)
				{
					DisplayValue("Special Commodity", specialCommodity, indent + 1);
				}
			}
		}

		private static void Display(Header header, int indent)
		{
			if (header == null)
			{
				return;
			}
			DisplayLabel("Header", indent);
			DisplayValue("Legal Name", header.legalName, indent + 1);
			DisplayValueIfPresent("DBA Name", header.dbaName, indent + 1);
			DisplayValue("DOT Number", header.dotNumber, header.dotNumberSpecified, indent + 1);
			Display(header.docket, indent + 1);
			Display(header.intrastate, indent + 1);
			DisplayValue("DUNS", header.duns, header.dunsSpecified, indent + 1);
			if (header.scac != null)
			{
				foreach (string scac in header.scac)
				{
					DisplayValue("SCAC", scac, indent + 1);
				}
			}
			Display("Physical Location", header.physicalLocation, indent + 1);
			Display("Mailing Location", header.mailingLocation, indent + 1);
		}

		private static void Display(DotProfileMcs150Mileage mileage, int indent)
		{
			if (mileage == null)
			{
				return;
			}
			DisplayValue("MCS Mileage",
			             string.Format("{0} {1} {2}",
			                           mileage.mileageYear,
			                           mileage.formDate.ToShortDateString(),
			                           mileage.mileage),
			             indent);
		}

		private static void Display(DocketType docket, int indent)
		{
			if (docket != null)
			{
				DisplayValue("Docket", string.Format("{0} {1}", docket.prefix, docket.number), indent);
			}
		}

		private static void Display(IntrastateType intrastateType, int indent)
		{
			if (intrastateType != null)
			{
				DisplayValue("Intrastate", intrastateType, indent);
			}
		}
	}
}