using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KhoaLuan.Helpers
{
	public static class CurrencyHelper
	{
		public static long CurrencyToNumber(string currencyString, char token)
		{
			string result = "";
			var arr = currencyString.Split(token);
			foreach (var item in arr)
			{
				result += item;
			}
			long number;
			if (Int64.TryParse(result, out number)) return number;
			else return 0;
		}
	}
}