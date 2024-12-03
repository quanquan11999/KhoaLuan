using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KhoaLuan.Models;
namespace KhoaLuan.Bo
{
	public class GetImage
	{
		public static List<string> getListImage(int? motelID)
		{
			List<string> listResult = new List<string>();
			using (var db = new QLTroEntities())
			{
				var listImage = db.Images.Where(p => p.MotelID == motelID).ToList();
				foreach (var item in listImage)
				{
					var url = item.Url.ToString();
					listResult.Add(url);
				}
			}
			return listResult;
		}
	}
}