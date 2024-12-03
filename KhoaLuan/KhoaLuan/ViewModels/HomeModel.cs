using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KhoaLuan.Models;

namespace KhoaLuan.ViewModels
{
	public class HomeModel
	{
		public List<Province> ListProvince { get; set; }
		public List<Criterion> ListCriteria { get; set; }
		public HomeModel CreateHomeModel()
		{
			List<Province> listProvince = new List<Province>();
			List<Criterion> listCriterion = new List<Criterion>();

			using (var db = new QLTroEntities())
			{
				listProvince = db.Provinces.ToList();
				listCriterion = db.Criteria.ToList();
			}
			HomeModel homeModel = new HomeModel()
			{
				ListCriteria = listCriterion,
				ListProvince = listProvince
			};
			return homeModel;
		}
	}
}