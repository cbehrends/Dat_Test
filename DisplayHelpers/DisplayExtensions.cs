
using QuickStart.Infrastructure;

namespace QuickStart.DisplayHelpers
{
	public static class DisplayExtensions
	{
		public static void Display(this PostAssetSuccessData data)
		{
			new PostDisplayHelper(data).Display();
		}

		public static void Display(this CreateSearchSuccessData data)
		{
			new SearchDisplayHelper(data).Display();
		}

		public static void Display(this LookupCarrierSuccessData data)
		{
			new LookupCarrierDisplayHelper(data).Display();
		}

		public static void Display(this LookupDobCarriersSuccessData data, int indent)
		{
			new LookupDobCarriersDisplayHelper(data, indent).Display();
		}

		public static void Display(this LookupDobEventsSuccessData data, SessionFacade session)
		{
			new LookupDobEventsDisplayHelper(data, session).Display();
		}

		public static void Display(this LookupDobSignedCarriersSuccessData data, SessionFacade session)
		{
			new LookupDobSignedCarriersDisplayHelper(data, session).Display();
		}

		public static void Display(this LookupRateSuccessData data)
		{
			new LookupRateDisplayHelper(data).Display();
		}

		public static void Display(this LookupHistoricContractRatesSuccessData data)
		{
			new LookupHistoricContractDisplayHelper(data).Display();
		}

		public static void Display(this LookupHistoricSpotRatesSuccessData data)
		{
			new LookupHistoricSpotDisplayHelper(data).Display();
		}

		public static void Display(this UpdateAlarmUrlSuccessData data, string alarmUrl)
		{
			new UpdateAlarmUrlDisplayHelper(data, alarmUrl).Display();
		}

		public static void Display(this ServiceError data, int indent = 0)
		{
			new ServiceErrorDisplayHelper(data).DisplayAt(indent);
		}

		/// <summary>
		/// Takes the first <param name="exactLength"></param> characters of a string and prepends 
		/// spaces if necessary to achieve a total length of <param name="exactLength"></param>.
		/// The string is trimmed first.
		/// </summary>
		/// <param name="s"></param>
		/// <param name="exactLength"></param>
		/// <returns></returns>
		public static string ClipLeft(this string s, int exactLength)
		{
			if (s == null)
			{
				return new string(' ', exactLength);
			}
			string trim = s.Trim();
			string padLeft = trim.PadLeft(exactLength);
			string clipLeft = padLeft.Substring(0, exactLength);
			return clipLeft;
		}

		/// <summary>
		/// Takes the last <param name="exactLength"></param> characters of a string and appends 
		/// spaces if necessary to achieve a total length of <param name="exactLength"></param>.
		/// The string is trimmed first.
		/// </summary>
		/// <param name="s"></param>
		/// <param name="exactLength"></param>
		/// <returns></returns>
		public static string ClipRight(this string s, int exactLength)
		{
			if (s == null)
			{
				return new string(' ', exactLength);
			}
			return s.Trim().PadRight(exactLength).SubstringFromEnd(exactLength);
		}

		/// <summary>
		/// Takes the last <param name="substringLength"></param> characters of a string, or less
		/// if the string is too short.
		/// </summary>
		/// <param name="s"></param>
		/// <param name="substringLength"> </param>
		/// <returns></returns>
		public static string SubstringFromEnd(this string s, int substringLength)
		{
			if (s == null || s.Length <= substringLength)
			{
				return s;
			}
			return s.Substring(s.Length - substringLength);
		}
	}
}