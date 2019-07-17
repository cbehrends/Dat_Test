namespace QuickStart.DisplayHelpers
{
	public class LookupRateDisplayHelper : DisplayHelper<LookupRateSuccessData>
	{
		private const string HEADER_LINE =
			"Type        | RateEst | RateLow | RateHigh | TotalEst | TotalLow | TotalHigh | Conf | Origin                    | Destination               | FuelSur | Contr | Moves | DaysBack ";

		public LookupRateDisplayHelper(LookupRateSuccessData data) : base(data) {}

		protected override void Display(int indent)
		{
			DisplayLabel("Lookup Rate Success Data", indent);
			DisplayLabel(HEADER_LINE, indent + 1);
			Display(Data.spotRate, indent + 1);
			Display(Data.contractRate, indent + 1);
			Display(Data.yourRate, indent + 1);
		}

		private static void Display(ContributorsSpotRateReport report, int indent)
		{
			if (report == null)
			{
				return;
			}
			DisplayLabel(
				SummaryLine("Contributor",
				            report.estimatedLinehaulRate,
				            report.lowLinehaulRate,
				            report.highLinehaulRate,
				            report.estimatedLinehaulTotal,
				            report.lowLinehaulTotal,
				            report.highLinehaulTotal,
				            report.confidenceLevel,
				            null,
				            null,
				            0,
				            0,
				            0,
				            0),
				indent);
		}

		private static void Display(CurrentContractRateReport report, int indent)
		{
			if (report == null)
			{
				return;
			}
			DisplayLabel(
				SummaryLine("Contract",
				            report.estimatedLinehaulRate,
				            report.lowLinehaulRate,
				            report.highLinehaulRate,
				            0,
				            0,
				            0,
				            0,
				            report.ratedLane.originGeography,
				            report.ratedLane.destinationGeography,
				            report.averageFuelSurchargeRate,
				            report.contributors,
				            report.moves,
				            0),
				indent);
		}

		private static void Display(CurrentSpotRateReport report, int indent)
		{
			if (report == null)
			{
				return;
			}
			DisplayLabel(
				SummaryLine("Spot",
				            report.estimatedLinehaulRate,
				            report.lowLinehaulRate,
				            report.highLinehaulRate,
				            report.estimatedLinehaulTotal,
				            report.lowLinehaulTotal,
				            report.highLinehaulTotal,
				            report.confidenceLevel,
				            report.ratedLane.originGeography,
				            report.ratedLane.destinationGeography,
				            report.averageFuelSurchargeRate,
				            report.contributors,
				            report.moves,
				            report.daysBack),
				indent);
		}

		private static string SummaryLine(string rateType,
		                                  float rateEst,
		                                  float rateLow,
		                                  float rateHigh,
		                                  float totalEst,
		                                  float totalLow,
		                                  float totalHigh,
		                                  int confidence,
		                                  string ratedOrigin,
		                                  string ratedDestination,
		                                  float fuelSurcharge,
		                                  int contributors,
		                                  int moves,
		                                  int daysBack)
		{
			const string intZeroAsDash = "#;;-";
			const string precisionTwoZeroAsDash = "0.00;;-";
			var items = new[]
			            {
			            	rateType.ClipLeft(11), rateEst.ToString(precisionTwoZeroAsDash).ClipLeft(7),
			            	rateLow.ToString(precisionTwoZeroAsDash).ClipLeft(7),
			            	rateHigh.ToString(precisionTwoZeroAsDash).ClipLeft(8), totalEst.ToString(intZeroAsDash).ClipLeft(8)
			            	, totalLow.ToString(intZeroAsDash).ClipLeft(8), totalHigh.ToString(intZeroAsDash).ClipLeft(9),
			            	confidence.ToString(intZeroAsDash).ClipLeft(4), ratedOrigin.ClipRight(25),
			            	ratedDestination.ClipRight(25), fuelSurcharge.ToString(precisionTwoZeroAsDash).ClipLeft(7),
			            	contributors.ToString(intZeroAsDash).ClipLeft(5), moves.ToString(intZeroAsDash).ClipLeft(5),
			            	daysBack.ToString(intZeroAsDash).ClipLeft(8)
			            };
			return string.Join(" | ", items);
		}
	}
}