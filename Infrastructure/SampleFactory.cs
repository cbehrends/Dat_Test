using System;

namespace QuickStart.Infrastructure
{
	/// <summary>
	/// Static source of sample data.
	/// </summary>
	public static class SampleFactory
	{
		public static readonly CityAndState Origin = new CityAndState
		                                             {
		                                             	city = "Chicago",
		                                             	county = "Cook",
		                                             	stateProvince = StateProvince.IL
		                                             };

		public static readonly CityAndState Destination = new CityAndState
		                                                  {
		                                                  	city = "Canton",
		                                                  	county = "Norfolk",
		                                                  	stateProvince = StateProvince.MA
		                                                  };

		public static PostAssetRequest BuildLoad(DateTime when)
		{
			string refId = when.Millisecond.ToString();

			var shipment = new Shipment
			               {
			               	destination = new Place {Item = Destination},
			                equipmentType = EquipmentType.Flatbed,
			               	origin = new Place {Item = Origin},
			               	rate =
			               		new ShipmentRate
			               		{
			               			baseRateDollars = 1700,
			               			rateBasedOn = RateBasedOnType.Flat,
			               			rateMiles = 951,
			               			rateMilesSpecified = true
			               		},
			               	truckStops =
			               		new TruckStops
			               		{
			               			enhancements = new[] {TruckStopVideoEnhancement.Flash, TruckStopVideoEnhancement.Highlight},
			               			Item = new ClosestTruckStops(),
			               			posterDisplayName = "12345"
			               		}
			               };

			var postAssetOperation = new PostAssetOperation
			                         {
			                         	availability =
			                         		new Availability
			                         		{
			                         			earliest = GetEarliestAvailability(when),
			                         			earliestSpecified = true,
			                         			latest = GetLatestAvailability(when),
			                         			latestSpecified = true
			                         		},
			                         	comments = new[] {"Call Now!"},
			                         	count = 1,
			                         	countSpecified = true,
			                         	dimensions =
			                         		new Dimensions
			                         		{
			                         			heightInches = 48,
			                         			heightInchesSpecified = true,
			                         			lengthFeet = 30,
			                         			lengthFeetSpecified = true,
			                         			volumeCubicFeet = 0,
			                         			volumeCubicFeetSpecified = false,
			                         			weightPounds = 45000,
			                         			weightPoundsSpecified = true
			                         		},
			                         	includeAsset = true,
			                         	includeAssetSpecified = true,
			                         	Item = shipment,
			                         	ltl = true,
			                         	ltlSpecified = true,
			                         	postersReferenceId = refId,
			                         	stops = 0,
			                         	stopsSpecified = true
			                         };

			return new PostAssetRequest {postAssetOperations = new[] {postAssetOperation}};
		}

		public static CreateSearchRequest BuildSearch(PostAssetRequest assetRequest)
		{
			Availability derivedAvailability = DeriveAvailability(assetRequest);
			Dimensions derivedDimensions = DeriveDimensions(assetRequest);

			var origin = new SearchArea {stateProvinces = new[] {StateProvince.CA, StateProvince.IL}};

			var destination = new SearchArea {zones = new[] {Zone.MidAtlantic}};

			var searchCriteria = new SearchCriteria
			                     {
			                     	ageLimitMinutes = 90,
			                     	ageLimitMinutesSpecified = true,
			                     	assetType = AssetType.Shipment,
			                     	availability = derivedAvailability,
			                     	destination = new GeoCriteria {Item = destination},
			                     	equipmentClasses = new[] {EquipmentClass.Flatbeds, EquipmentClass.Reefers},
			                     	includeFulls = true,
			                     	includeFullsSpecified = true,
			                     	includeLtls = true,
			                     	includeLtlsSpecified = true,
			                     	limits = derivedDimensions,
			                     	origin = new GeoCriteria {Item = origin}
			                     };

			var createSearchOperation = new CreateSearchOperation
			                            {
			                            	criteria = searchCriteria,
			                            	includeSearch = true,
			                            	includeSearchSpecified = true,
			                            	sortOrder = SortOrder.Closest,
			                            	sortOrderSpecified = true
			                            };

			return new CreateSearchRequest {createSearchOperation = createSearchOperation};
		}

		public static PostAssetRequest BuildTruckWithAlarm(PostAssetRequest loadRequest)
		{
			PostAssetOperation loadOperation = loadRequest.postAssetOperations[0];
			var load = loadOperation.Item as Shipment;
			var equipment = new Equipment
			                {
			                	origin = new Place {Item = load.origin.Item},
			                	destination = new EquipmentDestination {Item = new Place {Item = load.destination.Item}},
			                	equipmentType = load.equipmentType
			                };
			Availability availability = loadOperation.availability;
			Dimensions dimensions = loadOperation.dimensions;
			var alarm = new AlarmSearchCriteria
			            {
			            	ageLimitMinutes = 90,
			            	originRadius = new Mileage {method = MileageType.Road, miles = 50},
			            	destinationRadius = new Mileage {method = MileageType.Road, miles = 50}
			            };
			var operation = new PostAssetOperation
			                {
			                	availability = availability,
			                	alarm = alarm,
			                	count = loadOperation.count,
			                	countSpecified = true,
			                	dimensions = dimensions,
			                	Item = equipment,
			                	includeAsset = true,
			                	includeAssetSpecified = true
			                };
			if (loadOperation.stopsSpecified)
			{
				operation.stops = loadOperation.stops;
				operation.stopsSpecified = true;
			}
			if (loadOperation.ltlSpecified)
			{
				operation.ltl = loadOperation.ltl;
				operation.ltlSpecified = true;
			}
			return new PostAssetRequest {postAssetOperations = new[] {operation}};
		}

		private static Availability DeriveAvailability(PostAssetRequest assetRequest)
		{
			Availability availability = assetRequest.postAssetOperations[0].availability;
			DateTime timeEarlierThanLoadsEarliest = availability.earliest.AddHours(-1);
			return new Availability
			       {
			       	earliest = timeEarlierThanLoadsEarliest,
			       	earliestSpecified = true,
			       	latest = availability.latest,
			       	latestSpecified = true
			       };
		}

		private static Dimensions DeriveDimensions(PostAssetRequest assetRequest)
		{
			Dimensions assetDimensions = assetRequest.postAssetOperations[0].dimensions;
			return new Dimensions
			       {
			       	heightInches = assetDimensions.heightInches,
			       	heightInchesSpecified = true,
			       	lengthFeet = assetDimensions.lengthFeet + 10,
			       	lengthFeetSpecified = true,
			       	volumeCubicFeet = 0,
			       	volumeCubicFeetSpecified = false,
			       	weightPounds = assetDimensions.weightPounds,
			       	weightPoundsSpecified = true
			       };
		}

		private static DateTime GetLatestAvailability(DateTime when)
		{
			return when.Date.AddDays(5);
		}

		private static DateTime GetEarliestAvailability(DateTime when)
		{
			return when;
		}
	}
}