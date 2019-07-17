namespace QuickStart.DisplayHelpers
{
	public class LookupHistoricContractDisplayHelper : DisplayHelper<LookupHistoricContractRatesSuccessData>
	{
		private const string HEADER_LINE = "When    | RateEst | RateLow | RateHigh";
		public LookupHistoricContractDisplayHelper(LookupHistoricContractRatesSuccessData data) : base(data) {}

		protected override void Display(int indent)
		{
			DisplayLabel("Historic Contract Lookup", indent);
			DisplayLabel(HEADER_LINE, indent + 1);
			if (Data.monthlyReport != null)
			{
				foreach (HistoricContractRateReport report in Data.monthlyReport)
				{
					Display(report, indent + 1);
				}
			}
		}

		private static void Display(HistoricContractRateReport report, int indent)
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
				            report.highLinehaulRate),
				indent);
		}

		private static string SummaryLine(int year,
		                                  int month,
		                                  float estimatedLinehaulRate,
		                                  float lowLinehaulRate,
		                                  float highLinehaulRate)
		{
			var formats = new[]
			              {
			              	year.ToString("0000") + "-" + month.ToString("00"),
			              	estimatedLinehaulRate.ToString("0.00").ClipLeft(7), lowLinehaulRate.ToString("0.00").ClipLeft(7),
			              	highLinehaulRate.ToString("0.00").ClipLeft(8)
			              };
			return string.Join(" | ", formats);
		}
	}
}