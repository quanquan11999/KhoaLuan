using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KhoaLuan.Models;
namespace KhoaLuan.Bo
{
	public class GetCriteriaName
	{
		public static string GetName(int criteriaID)
		{
			var result = string.Empty;
			using (var db = new QLTroEntities())
			{
				result = db.Criteria.SingleOrDefault(p => p.CriteriaID == criteriaID).CriteriaName;
			}
			return result;
		}
	}
}