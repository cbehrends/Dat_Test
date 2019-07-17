namespace QuickStart.DisplayHelpers
{
	public class LookupHistoricSpotDisplayHelper : DisplayHelper<LookupHistoricSpotRatesSuccessData>
	{
		private const string HEADER_LINE =
			"When    | RateEst | RateLow | RateHigh | TotalEst | TotalLow | TotalHigh | Conf ";

		public LookupHistoricSpotDisplayHelper(LookupHistoricSpotRatesSuccessData data) : base(data) {}

		protected override void Display(int indent)
		{
			DisplayLabel("Historic Spot Lookup", indent);
			DisplayLabel(HEADER_LINE, indent + 1);
			foreach (HistoricSpotRateReport report in Data.monthlyReport)
			{
				Display(report, indent + 1);
			}
		}

		private static void Display(HistoricSpotRateReport report, int indent)
		{
			if (report == null)
			{
				return;
			}
			DisplayLabel(
				SummaryLine(report.when.year,
				            report.when.month,
				            report.estimatedLinehaulRate,
				            report.lowLinehaulRate,
				            report.highLinehaulRate,
				            report.estimatedLinehaulTotal,
				            report.lowLinehaulTotal,
				            report.highLinehaulTotal,
				            report.confidenceLevel),
				indent);
		}

		private static string SummaryLine(int year,
		                                  int month,
		                                  float estimatedLinehaulRate,
		                                  float lowLinehaulRate,
		                                  float highLinehaulRate,
		                                  float estimatedLinehaulTotal,
		                                  float lowLinehaulTotal,
		                                  float highLinehaulTotal,
		                                  int confidenceLevel)
		{
			var formats = new[]
			              {
			              	year.ToString("0000") + "-" + month.ToString("00"),
			              	estimatedLinehaulRate.ToString("0.00").ClipLeft(7), lowLinehaulRate.ToString("0.00").ClipLeft(7),
			              	highLinehaulRate.ToString("0.00").ClipLeft(8),
			              	estimatedLinehaulTotal.ToString("0.00").ClipLeft(8),
			              	lowLinehaulTotal.ToString("0.00").ClipLeft(8), highLinehaulTotal.ToString("0.00").ClipLeft(9),
			              	confidenceLevel.ToString("####").ClipLeft(4)
			              };
			return string.Join(" | ", formats);
		}
	}
}