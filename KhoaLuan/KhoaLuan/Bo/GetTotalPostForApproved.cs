using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KhoaLuan.Models;

namespace KhoaLuan.Bo
{
	public class GetTotalPostForApproved
	{
		public static string getTotalPost()
		{
			var s = string.Empty;
			using(var db = new QLTroEntities())
			{
				s = db.Posts.Where(p => p.PostStatus == false).ToList().Count().ToString();
			}
			return s;
		}
	}
}