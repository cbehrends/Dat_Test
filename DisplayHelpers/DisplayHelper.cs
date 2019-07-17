using System;
using System.Collections.Generic;
using System.Linq;

namespace QuickStart.DisplayHelpers
{
	public abstract class DisplayHelper
	{
		protected abstract void Display(int indent);

		protected static void DisplayValue(string name, DateTime value, int indent)
		{
			if (value == new DateTime())
			{
				return;
			}
			var prefix = new string(' ', indent * 2);
			Console.WriteLine("{0}: {1}", prefix + name, value.ToString("yyyy-MM-dd HH:mm:ss"));
		}
		protected static void DisplayValue(string name, object value, int indent)
		{
			if (value == null)
			{
				return;
			}
			var prefix = new string(' ', indent * 2);
			Console.WriteLine("{0}: {1}", prefix + name, value);
		}

		protected static void DisplayLabel(string name, int indent)
		{
			if (name == null)
			{
				return;
			}
			var prefix = new string(' ', indent * 2);
			Console.WriteLine("{0}", prefix + name);
		}

		protected static void DisplayValueIfPresent(string name, string value, int indent)
		{
			if (string.IsNullOrEmpty(value) == false)
			{
				DisplayValue(name, value, indent);
			}
		}

		/// <summary>
		/// Displays conditionally.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="value"></param>
		/// <param name="specified"></param>
		/// <param name="indent"></param>
		/// <param name="format"> </param>
		protected static void DisplayValue(string name, object value, bool specified, int indent, string format = null)
		{
			if (specified)
			{
				string useFormat = "{0:" + format + "}";
				DisplayValue(name, string.Format(useFormat, value), indent);
			}
		}

		protected static void DisplayValues<TItem>(string name,
		                                           IEnumerable<TItem> values,
		                                           int indent,
		                                           string separator = ",")
		{
			string[] strings = values.Select(x => x.ToString()).ToArray();
			DisplayValue(name, string.Join(separator, strings), indent);
		}

		protected static void Display(OptionalPhoneNumber phoneNumber, int indent, string name = null)
		{
			if (phoneNumber == null)
			{
				return;
			}
			DisplayLabel(name ?? "Phone Number", indent);
			DisplayValue("Country Code", phoneNumber.countryCode, indent + 1);
			DisplayValue("Number", phoneNumber.number, indent + 1);
			DisplayValue("Extension", phoneNumber.extension, indent + 1);
		}

		protected static void Display(Asset data, int indent)
		{
			if (data == null)
			{
				return;
			}

			DisplayLabel("Asset", indent);
			int nextIndent = indent + 1;
			DisplayValue("Asset Id", data.assetId, nextIndent);
			Display(data.availability, nextIndent);
			if (data.comments != null && data.comments.Any())
			{
				DisplayLabel("Comments", nextIndent);
				for (int i = 0; i < data.comments.Length; i++)
				{
					int oneBasedIndexForDisplay = i + 1;
					DisplayValue("Comment " + oneBasedIndexForDisplay, data.comments[i], nextIndent + 1);
				}
			}
			DisplayValue("Count", data.count, nextIndent);
			Display(data.dimensions, nextIndent);
			DisplayValue("Ltl", data.ltl, nextIndent);
			DisplayValue("Posters Reference Id", data.postersReferenceId, nextIndent);
			//Display(data.status, nextIndent);
			DisplayValue("Stops", data.stops, nextIndent);
		}

		protected internal static void Display(Place data, int indent, string name = null)
		{
			if (data == null)
			{
				return;
			}

			DisplayLabel(name ?? "Place", indent);
			if (data.Item is CityAndState)
			{
				var d = data.Item as CityAndState;
				Display(d, indent + 1);
			}
			if (data.Item is FmPostalCode)
			{
				var d = data.Item as FmPostalCode;
				Display(d, indent + 1);
			}
			if (data.Item is LatLon)
			{
				var d = data.Item as LatLon;
				Display(d, indent + 1);
			}
			if (data.Item is NamedLatLon)
			{
				var d = data.Item as NamedLatLon;
				Display(d, indent + 1);
			}
			if (data.Item is NamedPostalCode)
			{
				var d = data.Item as NamedPostalCode;
				Display(d, indent + 1);
			}
		}


		private static void Display(CityAndState data, int indent)
		{
			if (data == null)
			{
				return;
			}

			DisplayLabel("CityAndState", indent);
			DisplayValue("City", data.city, indent + 1);
			DisplayValue("County", data.county, indent + 1);
			DisplayValue("State", data.stateProvince, indent + 1);
		}

		private static void Display(FmPostalCode data, int indent)
		{
			if (data == null)
			{
				return;
			}

			DisplayLabel("FM Postal Code", indent);
			DisplayValue("Code", data.code, indent + 1);
			DisplayValue("Country", data.country, indent + 1);
		}

		private static void Display(LatLon data, int indent)
		{
			if (data == null)
			{
				return;
			}

			DisplayLabel("LatLon", indent);
			DisplayValue("Latitude", data.latitude, indent + 1);
			DisplayValue("Longitude", data.longitude, indent + 1);
		}

		private static void Display(NamedLatLon data, int indent)
		{
			if (data == null)
			{
				return;
			}

			DisplayLabel("Named LatLon", indent);
			DisplayValue("City", data.city, indent + 1);
			DisplayValue("Latitude", data.latitude, indent + 1);
			DisplayValue("Longitude", data.longitude, indent + 1);
			DisplayValue("State/Province", data.stateProvince, indent + 1);
		}

		private static void Display(NamedPostalCode data, int indent)
		{
			if (data == null)
			{
				return;
			}

			DisplayLabel("Named Postal Code", indent);
			DisplayValue("City", data.city, indent + 1);
			DisplayValue("County", data.county, indent + 1);
			Display(data.postalCode, indent + 1);
			DisplayValue("State/Province", data.stateProvince, indent + 1);
		}

		private static void Display(PostalCode data, int indent)
		{
			if (data == null)
			{
				return;
			}

			DisplayLabel("Postal Code", indent);
			DisplayValue("Code", data.code, indent + 1);
			DisplayValue("Country", data.country, indent + 1);
		}

		protected static void Display(Availability data, int indent)
		{
			if (data == null)
			{
				return;
			}

			DisplayLabel("Availability", indent);
			if (data.earliestSpecified)
			{
				DisplayValue("Earliest", data.earliest, indent + 1);
			}
			if (data.latestSpecified)
			{
				DisplayValue("Latest", data.latest, indent + 1);
			}
		}

		protected static void Display(Dimensions data, int indent)
		{
			if (data == null)
			{
				return;
			}

			DisplayLabel("Dimensions", indent);
			if (data.heightInchesSpecified)
			{
				DisplayValue("Height Inches", data.heightInches, indent + 1);
			}
			if (data.lengthFeetSpecified)
			{
				DisplayValue("Length Feet", data.lengthFeet, indent + 1);
			}
			if (data.volumeCubicFeetSpecified)
			{
				DisplayValue("Volume Cubit Feet Specified", data.volumeCubicFeet, indent + 1);
			}
			if (data.weightPoundsSpecified)
			{
				DisplayValue("Weight Pounds", data.weightPounds, indent + 1);
			}
		}

		protected static void Display(FmeStatus data, int indent)
		{
			if (data == null)
			{
				return;
			}

			DisplayLabel("FmeStatus", indent);
			Display(data.created, indent + 1, "Created");
			DisplayValue("End Date", data.endDate, indent + 1);
			DisplayValue("Expired", data.expired, indent + 1);
			Display(data.lastModified, indent + 1, "Last Modified");
			DisplayValue("Start Date", data.startDate, indent + 1);
			Display(data.updated, indent + 1, "Updated");
			DisplayValue("User Id", data.userId, indent + 1);
		}

		private static void Display(UserTimeStamp data, int indent, string label)
		{
			DisplayLabel(label, indent);
			DisplayValue("Date", data.date, indent + 1);
			DisplayValue("User", data.user, indent + 1);
		}

		protected static void Display(Mileage data, int indent, string description = null)
		{
			if (data == null)
			{
				return;
			}

			DisplayLabel(description ?? "Mileage", indent);
			DisplayValue("Miles", data.miles, indent + 1);
			DisplayValue("Method", data.method, indent + 1);
		}

		protected static void Display(string name, Location location, int indent)
		{
			if (location == null)
			{
				return;
			}
			DisplayLabel(name, indent);
			DisplayValue("Street", location.street, indent + 1);
			DisplayValue("City", location.city, indent + 1);
			DisplayValue("State or Province", location.stateProvince, location.stateProvinceSpecified, indent + 1);
			DisplayValue("PostalCode", location.postalCode, indent + 1);
			DisplayValue("CountryCode", location.countryCode, indent + 1);
			Display(location.phone, indent + 1);
			Display(location.fax, indent + 1, "Fax");
			Display(location.tollFreePhone, indent + 1, "Toll Free");
		}

		protected static void ToDo(int indent)
		{
		    DisplayValue("NotImplementedYet", "NotImplementedYet", indent + 1);
		}
	}

	public abstract class DisplayHelper<TData> : DisplayHelper
		where TData : Data
	{
		private readonly TData _data;
		private readonly int _initialIndent;

		protected DisplayHelper(TData data, int initialIndent = 0)
		{
			_data = data;
			_initialIndent = initialIndent;
		}

		protected TData Data
		{
			get { return _data; }
		}

		public void Display()
		{
			if (Data == null)
			{
				return;
			}
			Display(_initialIndent);
		}
	}
}